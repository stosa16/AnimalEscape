﻿using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] Sounds;
        void Awake () {

            foreach (var sound in Sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.priority = sound.Priority;
                sound.Source.pitch = sound.Pitch;
            }
        }

        public void Play(string name)
        {
            var sound = Sounds.FirstOrDefault(s => s.Name == name);
            if (sound != null)
                sound.Source.Play();
        }

        public void Stop(string name){
            var sound = Sounds.FirstOrDefault(s => s.Name == name);
            if (sound != null)
                sound.Source.Stop();
        }
    }
}
