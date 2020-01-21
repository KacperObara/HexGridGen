using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public static int Score { get; set; }


    [SerializeField]
    private static TextMeshProUGUI scoreText;

    private void Start()
    {
        Score = 0;
    }

    private void OnEnable()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = Score.ToString();
    }

}
