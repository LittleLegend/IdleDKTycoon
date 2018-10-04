using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundEffectService : MonoBehaviour
    {
        [SerializeField] private ClipsConfig _clips;
        private readonly List<AudioSource> _audioSources = new List<AudioSource>();

        private int _audioSourceIndex;
        private static SoundEffectService _sfxSingletonInstance;

        private Dictionary<ClipIdentifier, int> _indexOfLastPlayedClip;

        public static SoundEffectService Instance => _sfxSingletonInstance;

        private void Awake()
        {
            _sfxSingletonInstance = this;
            _indexOfLastPlayedClip = new Dictionary<ClipIdentifier, int>();

            _indexOfLastPlayedClip = InitializeDictionary();
           
        }

        public void PlayClip(ClipIdentifier clipIdentifier)
        {
            var clipsOfDesiredType = _clips.Clips.Where(clip => clip.Clip == clipIdentifier).ToList();
            
            var sfxClip = GetRandomClip(clipsOfDesiredType,clipIdentifier);
            Play(sfxClip.AudioClip);
        }

        private SfxClip GetRandomClip(List<SfxClip> clipsOfDesiredType, ClipIdentifier clipIdentifier)
        {
            if (clipsOfDesiredType.Count == 1)
                return clipsOfDesiredType[0];

            var indexOfLastOne = _indexOfLastPlayedClip[clipIdentifier];

            if (indexOfLastOne == -1)
            {
                var randomClipIndex = Random.Range(0, clipsOfDesiredType.Count);
                _indexOfLastPlayedClip[clipIdentifier] = randomClipIndex;
                
                return clipsOfDesiredType[randomClipIndex];
            }
            else
            {
                var lastPlayedClip = clipsOfDesiredType[indexOfLastOne];

                var otherClips = clipsOfDesiredType.Where(clip => clip != lastPlayedClip).ToArray();
                
                var randomClipIndex = Random.Range(0, otherClips.Length);
                var randomClip = otherClips[randomClipIndex];
                
                _indexOfLastPlayedClip[clipIdentifier] = clipsOfDesiredType.IndexOf(randomClip);

                return randomClip;
            }
            
        }

        private void Play(AudioClip sfxClipAudioClip)
        {
            var audioSource = GetAvailableAudioSource();
            audioSource.PlayOneShot(sfxClipAudioClip);
        }

        private AudioSource GetAvailableAudioSource()
        {
            if (_audioSources.Any(source => !source.isPlaying))
            {
                return _audioSources.First(source => !source.isPlaying);
            }
            else
            {
                return ExpandSources();
            }	
        }

        private AudioSource ExpandSources()
        {
            var source = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(source);

            return source;
        }

        void Update()
        {
			
            //testing area

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayClip((ClipIdentifier)1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayClip((ClipIdentifier)2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayClip((ClipIdentifier)3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayClip((ClipIdentifier)4);
            }
        }

        private static Dictionary<ClipIdentifier, int> InitializeDictionary()
        {
            var enumType = typeof(ClipIdentifier);
        
            Dictionary<ClipIdentifier, int> dictionary = new Dictionary<ClipIdentifier, int>(Enum.GetValues(enumType).Length);

            Array values = Enum.GetValues(enumType);
            foreach (ClipIdentifier value in values)
            {
                dictionary.Add(value, -1);
            }
            return dictionary;
        }
    }
}