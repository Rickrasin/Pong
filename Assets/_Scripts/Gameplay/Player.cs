using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PongGame.Interfaces;
using PongGame.Managers;

namespace PongGame.Player
{
    public class Player : MonoBehaviour, IHeight
    {

        [SerializeField] private float speed;
        [SerializeField] private float valueSize;

        private bool ESC;
        private float moveInput;

        private bool isGrow;


        private Vector2 workSpace;

        private Rigidbody2D RB;

        void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
        }


        void Update()
        {
            Inputs();

            workSpace.Set(0, moveInput * speed);

            RB.velocity = workSpace;
        }




        private void Inputs()
        {

            if(ESC)
            {
                GameManager.Instance.UpdateGameState(GameState.Pause);
            }


            ESC = Input.GetKeyDown(KeyCode.Escape);

            moveInput = Input.GetAxisRaw("Vertical");


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
