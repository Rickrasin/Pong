using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongGame.Manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;


        public static event Action<GameState> OnGameStateChanged;
        public static GameObject ball { get; private set; }
        public static GameObject[] bars { get; private set; }

        public GameState State;

        private void Awake()
        {
            instance = this;
            ball = GameObject.FindGameObjectWithTag("Ball");
            bars = GameObject.FindGameObjectsWithTag("Bars");

        }

        private void Start()
        {
            UpdateGameState(GameState.Gameplay);
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;

            switch (newState)
            {
                case GameState.Pause:
                    HandlePauseMenu();
                    break;
                case GameState.Gameplay:
                    HandleResumeGame();
                    break;
                case GameState.Reset:
                    HandleReset();
                    break;

            }

            OnGameStateChanged?.Invoke(newState);
        }

        

        private void HandleReset()
        {
            ball.transform.position = Vector3.zero;

            bars[0].transform.position = new Vector3(7, 0, 0);
            bars[0].transform.localScale = new Vector3(0.5f, 3, 1);

            bars[1].transform.localScale = new Vector3(0.5f, 3, 1);
            bars[1].transform.position = new Vector3(-7, 0, 0);


            instance.UpdateGameState(GameState.Gameplay);

        }


        private void HandleResumeGame()
        {
            Time.timeScale = 1;


        }

        private void HandlePauseMenu()
        {
            Time.timeScale = 0;
        }



    }




    public enum GameState
    {
        Pause,
        Gameplay,
        Reset
        
    }


}


