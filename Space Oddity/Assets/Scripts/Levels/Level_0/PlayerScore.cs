using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score;

    public int highScore;

    private TextMeshPro textMesh;

    public void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }

    public void Update()
    {
        Score();
    }

    public void Score()
    {
        textMesh.text = score.ToString("0");

        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScore = score;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("Player Score = " + score);
    }
}
