using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    int questId;
    string fileName;
    float currentTime;
    float maxTime;
    TimeManager time;

    private void Start()
    {
        time = GameObject.Find("GameManager").GetComponent<TimeManager>();
    }

    public QuestData() { }

    public QuestData(int id, string name, float maxTime)
    {
        this.questId = id;
        this.fileName = name;
        this.currentTime = time.time;
        this.maxTime = maxTime;
    }

    private void Update()
    {
        if (maxTime < time.time)
        {
            //QuestManager 실패 호출
        }
    }
}
