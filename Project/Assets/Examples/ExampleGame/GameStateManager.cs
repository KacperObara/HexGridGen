using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> states;

    public void PushState(GameObject gameObject)
    {
        states.Add(gameObject);
    }

    public void PopState()
    {
        int lastIndex = states.Count - 1;
        states[lastIndex].SetActive(false);
        states.RemoveAt(lastIndex);
        states[lastIndex - 1].SetActive(true);
    }
}
