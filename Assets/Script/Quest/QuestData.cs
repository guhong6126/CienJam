using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public int questId;
    public string fileName;

    float currentTime;
    public float maxTime;
    public int difficulty;

    TimeManager time;

    public QuestData() { }

    public QuestData(int id, string name, float maxTime, int difficulty)
    {
        time = GameObject.Find("GameManager").GetComponent<TimeManager>();
        this.questId = id;
        this.fileName = name;
        this.currentTime = time.minute;
        this.maxTime = maxTime == 0 ? 540 : maxTime + currentTime;
        this.difficulty = difficulty;
    }
}
