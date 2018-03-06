using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Sound
    {
        public string Name;

        public AudioClip Clip;

        [Range(0, 256)]
        public int Priority;

        [Range(.1f, 3f)]
        public float Pitch;

        [Range(.0f, 1.0f)]
        public float Volume;

        [HideInInspector]
        public AudioSource Source;

    }
}
