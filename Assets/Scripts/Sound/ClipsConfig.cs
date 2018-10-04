using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(menuName = "Configs/Clips")]
    public class ClipsConfig : ScriptableObject
    {
        public List<SfxClip> Clips;
    }
}