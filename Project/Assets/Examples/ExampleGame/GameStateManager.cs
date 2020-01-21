using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexGenExampleGame1
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> states;

        private BoardManager boardManager;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
        }

        public void PushState(GameObject gameObject)
        {
            states.Add(gameObject);
        }

        public void PopState()
        {
            if (boardManager.ExistingUnits.Count == 0 && states.Count <= 3)
                return;

            int lastIndex = states.Count - 1;
            states[lastIndex].SetActive(false);
            states.RemoveAt(lastIndex);
            states[lastIndex - 1].SetActive(true);
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("ExampleGame");
        }
    }
}
