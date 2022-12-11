using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, ManagerInterface
{
    public ManagerStatus status { get; private set; }

    public int score;
    [SerializeField] TMP_Text scoreDisplay;

    public void Startup()
    {
        scoreDisplay.text = score.ToString();
        status = ManagerStatus.Started;
    }

    public void UpdateScore()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
