using System.Collections;
using System.Collections.Generic;
using PongGame.Managers;
using UnityEngine;

namespace PongGame.Managers.Sound
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _toggleMusic;
        [SerializeField] private AudioSource _toggleEffects;

        public void Toggle()
        {
            if (_toggleEffects) SoundManager.Instance.ToggleEffects();
            if (_toggleMusic) SoundManager.Instance.ToggleMusic();


        }


    }
}
