using PongGame.Interfaces;
using UnityEngine;
using PongGame.Managers;

namespace PongGame.GameBall
{
    public class Ball : MonoBehaviour
    {
        [Header("Velocities")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float maxSpeed = 12f;
       

        
        [SerializeField] private float initialAngle = 45f;

        private Vector2 startPos;
        private Vector2 lastVelocity;



        [Space(5)]
        [Header("Sounds")]
        [SerializeField] private AudioClip wallSound;
        [SerializeField] private AudioClip playerClip;
        [SerializeField] private AudioClip enemyClip;
        [SerializeField] private AudioClip lostWinClip;



        private Rigidbody2D RB;

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            lastVelocity = transform.forward;
            float angle = Random.Range(-initialAngle, initialAngle);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            RB.velocity = direction * speed;

        }

        private void Update()
        {
            lastVelocity = RB.velocity;

        }
        public void ResetBall()
        {
            
            RB.velocity = Vector2.zero;
            transform.position = startPos;
            float angle = Random.Range(-initialAngle, initialAngle);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            RB.velocity = direction * speed;
        }




        private void OnCollisionEnter2D(Collision2D collision)
        {


            

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



            //reflete a bola na direção oposta ao eixo atingido
            var velocity = lastVelocity.magnitude;
            Vector2 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);


            RB.velocity = direction * (Mathf.Min(velocity * 1.125f, maxSpeed));


            #endregion


        }

        private void OnTriggerEnter2D(Collider2D collision)
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
        }
    }
}
