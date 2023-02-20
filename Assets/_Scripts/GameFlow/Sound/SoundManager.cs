using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace PongGame.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            
        }

        public void PlaySound(AudioClip clip)
        {
            _effectSource.PlayOneShot(clip);
        }

        public void ChangeMasterVolume(float volume)
        {
            AudioListener.volume = volume;
        }

        public void ToggleEffects()
        {
            _effectSource.mute = !_effectSource.mute;
        }

        public void ToggleMusic()
        {
            _effectSource.mute = !_musicSource.mute;
        }
    }
}
