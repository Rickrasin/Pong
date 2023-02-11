using PongGame.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongGame
{
    public class BallBounce : MonoBehaviour
    {
        [SerializeField]private float increaseSpeed;
        [SerializeField]private float maxSpeed;

        private Rigidbody2D RB;


        private Vector3 lastVelocity;
        private void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            lastVelocity = RB.velocity;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var speed = lastVelocity.magnitude;

           
            if(col.gameObject.layer == 12) //Aumenta velocidade se colidir com barra
            {
                if(speed < maxSpeed) { 
                speed += increaseSpeed;
                }

            }
           

            var direction = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);


            

            RB.velocity = direction * Mathf.Max(speed , 0f);


        }
    }
}
