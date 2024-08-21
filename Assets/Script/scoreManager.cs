using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text successScore;
    [SerializeField] private TMP_Text FailedScore;

    private QuestManager questManager;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void setScores()
    {
        score.text = questManager.score.ToString();
        successScore.text = questManager.successCount.ToString();
        FailedScore.text = questManager.failCount.ToString();
    }
}
