using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public int questId;
    public string fileName;

    float currentTime;
    float maxTime;
    TimeManager time;

    public QuestData() { }

    public QuestData(int id, string name, float maxTime)
    {
        time = GameObject.Find("GameManager").GetComponent<TimeManager>();
        this.questId = id;
        this.fileName = name;
        this.currentTime = time.time;
        this.maxTime = maxTime;
    }
}
