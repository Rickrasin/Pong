using PongGame.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongGame.ball
{
    public class ball : MonoBehaviour
    {

        private Rigidbody2D RB;

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            RB.AddForce(new Vector2(9.8f * 25f, 9.6f * 25f));
        }

        

        private void OnCollisionEnter2D(Collision2D collision)
        {

            IHeight heightInterface = collision.gameObject.GetComponent<IHeight>();

            if (heightInterface != null)
            {
                heightInterface.increaseHeight();
            }
        }
    }
}
