using UnityEngine;

namespace Sound
{
    public class MusicService : MonoBehaviour
    {
        private static MusicService _instance;

        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;
        
        [SerializeField] private AudioSource _source;
        
        public static MusicService Instance => _instance;

        private void Awake()
        {
            _instance = this;

         
            _source.clip = _menuMusic;
            _source.Play(1);
            
            _source.loop = true;
        }

        public void PlayGameMusic()
        {
            if(_source.clip == _gameMusic)
                return;
            
            _source.Stop();
            _source.clip = _gameMusic;
            _source.Play(1);
        }

        public void PlayMenuMusic()
        {
            if(_source.clip == _menuMusic)
                return;
            
            _source.Stop();
            _source.clip = _menuMusic;
            _source.Play(1);
        }
    }
}