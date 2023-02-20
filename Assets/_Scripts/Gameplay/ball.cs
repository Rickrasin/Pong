using PongGame.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PongGame.Managers;

namespace PongGame.ball
{
    public class ball : MonoBehaviour
    {
        [Header("Velocities")]
        [SerializeField] private float startVelocity = 25f;
        [SerializeField] private float increaseSpeed;
        [SerializeField] private float maxSpeed;
        [Space(5)]

        [Header("Sounds")]
        [SerializeField] private AudioClip wallSound;
        [SerializeField] private AudioClip playerClip;
        [SerializeField] private AudioClip enemyClip;
        [SerializeField] private AudioClip lostWinClip;

        private Vector3 lastVelocity;
        private float velocity;


        private Rigidbody2D RB;

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            RB.AddForce(new Vector2(9.8f * startVelocity, 9.6f * startVelocity));
        }

        private void Update()
        {
            lastVelocity = RB.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {


            #region LostWin
            if (collision.gameObject.layer == 14)
            {
                SoundManager.Instance.PlaySound(lostWinClip); //SOUND
                if (collision.transform.localPosition.x >= 0.1f)
                {
                    GameManager.Instance.increaseScores(1, Score.Player);

                }
                if (collision.transform.localPosition.x <= -0.1f)
                {
                    GameManager.Instance.increaseScores(1, Score.Enemy);
                }

                GameManager.Instance.UpdateGameState(GameState.Reset);

            }


            #endregion


            #region Paddles
            // Aumenta e Diminui a barra
            IHeight heightInterface = collision.gameObject.GetComponent<IHeight>();

            if (heightInterface != null)
            {
                heightInterface.increaseHeight();
            }

            #endregion


            #region Sounds 

            if (collision.gameObject.layer == 8) SoundManager.Instance.PlaySound(wallSound);

            if (collision.gameObject.tag == "Player") SoundManager.Instance.PlaySound(playerClip);

            if (collision.gameObject.tag == "Enemy") SoundManager.Instance.PlaySound(enemyClip);
           

            #endregion


            #region ReflectBall 

            var speed = lastVelocity.magnitude;

            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            RB.velocity = direction * Mathf.Max(speed, 0f);
            #endregion






        }
    }
}
