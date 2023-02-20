using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PongGame.Interfaces;
using System.Threading;
using PongGame.Managers;
using UnityEditor;

namespace PongGame.Enemy
{
    public class Enemy : MonoBehaviour, IHeight
    {

        [SerializeField] private float speed = 6f;
        [SerializeField] private float positionThreshold = 0.2f;
        [Space(5)]



        private bool isGrow;
        private bool inRight;

        private Rigidbody2D RB;
        private Transform ball;

        void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
            ball = GameObject.FindGameObjectWithTag("Ball").transform;
            inRight = transform.position.x > 0.1f ? true : false;

        }

        private void FixedUpdate()
        {
            float diff = ball.position.y - transform.position.y;    
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();

            if (ballRB.velocity.x > 0 && inRight)
            {
                if(Mathf.Abs(diff) > positionThreshold)
                {
                    float direction = Mathf.Sign(diff);
                    RB.velocity = new Vector2(0, direction * speed);
                }
                else
                {
                    RB.velocity = Vector2.zero;
                }

            }else if(ballRB.velocity.x < 0 && !inRight)
            {
                if (Mathf.Abs(diff) > positionThreshold)
                {
                    float direction = Mathf.Sign(diff);
                    RB.velocity = new Vector2(0, direction * speed);
                }
                else
                {
                    RB.velocity = Vector2.zero;
                }
            }
            else
            {
                float direction = Mathf.Sign(transform.position.y);
                RB.velocity = new Vector2(0, direction * speed);
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
