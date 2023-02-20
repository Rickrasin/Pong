using PongGame.GameBall;
using System;
using UnityEngine;

namespace PongGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float playerScore = 0f;
        public float enemyScore = 0f;


        public static GameManager Instance;



        public static event Action<GameState> OnGameStateChanged;
        public GameObject ball { get; private set; }
        public GameObject player { get; private set; }
        public GameObject enemy { get; private set; }

        public GameState State;

        

        private void Awake()
        {
            Instance = this;
            

            ball = GameObject.FindGameObjectWithTag("Ball");
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GameObject.FindGameObjectWithTag("Enemy");

        }

        private void Start()
        {   

            if (State == GameState.Gameplay)
            {
                UpdateGameState(GameState.Gameplay);
            }

            if (State == GameState.MainMenu)
            {
                HandleReset(true);

            }
        }

        public void increaseScores(float val, Score character)
        {

            if (character == Score.Player) playerScore += val;
            if (character == Score.Enemy) enemyScore += val;

        }

        #region GameState Functions

        public void UpdateGameState(GameState newState)
        {
            State = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                    HandleReset(true);
                    break;
                case GameState.Pause:
                    HandlePauseMenu();
                    break;
                case GameState.Options:
                    break;
                case GameState.Gameplay:
                    HandleResumeGame();
                    break;
                case GameState.Reset:
                    HandleReset(false);
                    break;
                case GameState.Restart:
                    HandleReset(true);
                    break;

            }

            OnGameStateChanged?.Invoke(newState);
        }

        private void HandleReset(bool resetScore)
        {
            if (resetScore)
            {
                playerScore = 0;
                enemyScore = 0;
                player.transform.localScale = new Vector3(0.5f, 3, 1);
                enemy.transform.localScale = new Vector3(0.5f, 3, 1);
                player.transform.position = new Vector3(-7, 0, 0);
                enemy.transform.position = new Vector3(7, 0, 0);

            }
            ball.GetComponent<Ball>().ResetBall();

            Instance.UpdateGameState(GameState.Gameplay);

        }

        private void HandleResumeGame()
        {
            Time.timeScale = 1;
        }

        private void HandlePauseMenu()
        {

            Time.timeScale = 0;
        }

        #endregion

    }

    public enum GameState
    {
        MainMenu,
        Options,
        Pause,
        Gameplay,
        Reset,
        Restart

    }
    public enum Score
    {
        Player,
        Enemy

    }


}


