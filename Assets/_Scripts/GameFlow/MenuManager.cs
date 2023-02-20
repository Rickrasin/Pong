using UnityEngine.UI;
using UnityEngine;
using PongGame.Managers;
using TMPro;

namespace PongGame.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _Pause, _Question, _Score;

        private TMP_Text player;
        private TMP_Text enemy;


        private void Awake()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

            foreach (TMP_Text item in _Score.GetComponentsInChildren<TMP_Text>())
            {
                _ = item.name == "Player 1" ? player = item : enemy = item;
            }

        }

        private void Update()
        {
            UpdateScoresText(GameManager.Instance.playerScore, GameManager.Instance.enemyScore);
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

        }

        public void UpdateScoresText(float playerScore, float enemyScore)
        {
            player.text = playerScore.ToString();
            enemy.text = enemyScore.ToString();
        }

        #region GameState Functions
        private void GameManagerOnOnGameStateChanged(GameState state)
        {

            _Pause.SetActive(state == GameState.Pause);

        }
        #endregion

        #region Button Functions
        public void ResumeGame() => GameManager.Instance.UpdateGameState(GameState.Gameplay);

        public void ResetGame() => GameManager.Instance.UpdateGameState(GameState.Reset);

        public void RestartGame() => GameManager.Instance.UpdateGameState(GameState.Restart);

        public void QuitQuestion() => _Question.SetActive(true);
        public void Back() => _Question.SetActive(false);

        public void QuitGame() => Application.Quit();
        #endregion



    }
}
