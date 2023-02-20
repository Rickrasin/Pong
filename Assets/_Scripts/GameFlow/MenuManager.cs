using UnityEngine.UI;
using UnityEngine;
using PongGame.Managers;
using TMPro;
using UnityEngine.SceneManagement;

namespace PongGame.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _Pause, _Question, _Score, _Options, _MainMenu;

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

        #region Score Methods
        public void UpdateScoresText(float playerScore, float enemyScore)
        {
            player.text = playerScore.ToString();
            enemy.text = enemyScore.ToString();
        }
        #endregion

        #region GameState Methods
        private void GameManagerOnOnGameStateChanged(GameState state)
        {
            if(_Pause != null) _Pause.SetActive(state == GameState.Pause);
        }
        #endregion

        #region Button Methods
        public void StartGame() => SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
        public void GoMainMenu() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        public void ResumeGame() => GameManager.Instance.UpdateGameState(GameState.Gameplay);

        public void ResetGame() => GameManager.Instance.UpdateGameState(GameState.Reset);

        public void RestartGame() => GameManager.Instance.UpdateGameState(GameState.Restart);




        public void MainMenu() => _MainMenu.SetActive(true);
        public void BackMain() => _MainMenu.SetActive(false);

        public void OptionsMenu() => _Options.SetActive(true);
        public void BackOptions() => _Options.SetActive(false);

        public void QuitQuestion() => _Question.SetActive(true);
        public void BackQuestion() => _Question.SetActive(false);
        
        public void QuitGame() => Application.Quit();
        #endregion



    }
}
