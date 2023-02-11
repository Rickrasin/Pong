using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PongGame.Interfaces;
using System.Threading;
using PongGame.Manager;

namespace PongGame.Enemy
{
    public class Enemy : MonoBehaviour, IHeight
    {

        [SerializeField] private float timeToRandom;
        [Space(5)]
        [SerializeField] private float randomMax;
        [SerializeField] private float randomMin;
        [Space(5)]
        [SerializeField] private LayerMask layerMask;


        private float time;
        private float randomValue;

        private bool isGrow;

        private Vector2 workSpace;

        private Rigidbody2D RB;
        private Animator animator;
        private Transform ball;

        void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            ball = GameManager.ball.transform;
            randomValue = Random.Range(randomMin, randomMax);

            time = Time.time;
        }

        private void Update()
        {

            RandomizeValue();
            Moving();

        }


        private void FixedUpdate()
        {

            RB.velocity = workSpace;

        }

        private void Moving()
        {
            if (ball.position.y > transform.position.y)
            {
                workSpace.Set(transform.position.x, randomValue);
            }
            else if (ball.position.y < transform.position.y)
            {
                workSpace.Set(transform.position.x, -randomValue);

            }
        }
        private void RandomizeValue()
        {

            if (Time.time >= time + timeToRandom)
            {
                randomValue = Random.Range(randomMin, randomMax);
                time = Time.time;

            }

        }

        public void increaseHeight()
        {

            if (transform.localScale.y < 1.4f)
            {
                isGrow = true;
            }
            if (transform.localScale.y > 2.8f)
            {
                isGrow = false;
            }

            #region Action 
            if (transform.localScale.y <= 3 && !isGrow)
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.2f, transform.localScale.z);
            }

            if (transform.localScale.y >= 1.2f && isGrow)
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.2f, transform.localScale.z);

            }
            #endregion

        }


    }
}
