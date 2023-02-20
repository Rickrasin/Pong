using UnityEngine;
using UnityEngine.UI;

namespace PongGame.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectSource;
        [SerializeField] private Slider _slider;
        [Range(0, 1)] private float _volume = 0.7f;

        private void Awake()
        {
           
                Instance = this;
           


        }
        private void Start()
        {
            _slider.value = _volume;
        }

        private void Update()
        {
            _volume = _slider.value;
            ChangeMasterVolume(_volume);
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
