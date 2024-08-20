using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, QuestData> questList;                //현재 수락한 퀘스트 정리
    TimeManager time;                                   

    List<int> id = new List<int>() {11, 12, 13};                 //존재하는 id 리스트로 정리
    List<string> fileName = new List<string>() {"dd", "cc"};     //존재하는 파일이름 리스트로 정리

    private void Start()
    {
        questList = new Dictionary<int, QuestData>();
        time = GetComponent<TimeManager>();
        StartCoroutine(MakeQuest());
    }

    //퀘스트 생성
    IEnumerator MakeQuest()
    {
        yield return new WaitForSeconds(3.0f);

        int id = this.id[Random.Range(0, this.id.Count)];
        string fileName = this.fileName[Random.Range(0, this.fileName.Count)];
        questList.Add(id, new QuestData(id, fileName, 300));                     //MaxTime 스크립트 작성해야함
    }

    //퀘스트성공
    public void SuccessQuest(int id)
    {
        questList.Remove(id);
    }

    //퀘스트실패
    public void FailQuest(int id)
    {
        questList.Remove(id);
    }
}
