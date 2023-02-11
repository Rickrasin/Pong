using UnityEngine.UI;
using UnityEngine;
using PongGame.Manager;
using TMPro;

namespace PongGame
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _Pause, _Question;


        private void Awake()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

        }

        private void GameManagerOnOnGameStateChanged(GameState state)
        {

            _Pause.SetActive(state == GameState.Pause);
 
        }


        
        public void ResumeGame() => GameManager.instance.UpdateGameState(GameState.Gameplay);

        public void ResetGame() => GameManager.instance.UpdateGameState(GameState.Reset);



        public void QuitQuestion() => _Question.SetActive(true);
        public void Back() => _Question.SetActive(false);

        public void QuitGame() => Application.Quit();


    }
}
