using PongGame.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongGame.Managers.Sound
{
    public class PlaySoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private void Start()
        {
            SoundManager.Instance.PlaySound(_clip);
        }
    }
}
