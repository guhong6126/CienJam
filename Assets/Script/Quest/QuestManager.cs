using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, QuestData> questList;                   //현재 수락한 퀘스트 정리
    public Dictionary<int, string> mailReceiver;                   //메일 수신자 정리

    TimeManager time;

    [SerializeField] List<MakeText> texts = new List<MakeText>();
    [SerializeField] List<MessengerManager> messenge = new List<MessengerManager>();

    List<int> id = new List<int>() {21, 22, 23, 24, 25, 26, 27, 28, 29};                 //존재하는 id 리스트로 정리

    List<string> fileName = new List<string>() {"dd", "cc"};     //존재하는 파일이름 리스트로 정리

    //메일(20번대)에 사용
    List<string> receiverName = new List<string>() {"team.leader@company.com", "project.manager@company.com", "business.strategy@company.com", "global.operations@company.com", "marketing.expert@company.com", "customer.success@company.com", "sales.director@company.com", "finance.controller@company.com", "hr.recruiter@company.com", "innovation.lead@company.com", "digital.strategy@company.com", "client.relations@company.com" };
    List<string> titleName = new List<string>() {"a"};

    private void Start()
    {
        questList = new Dictionary<int, QuestData>();
        mailReceiver = new Dictionary<int, string>();

        time = GetComponent<TimeManager>();
        //StartCoroutine(MakeQuest());
    }

    void SendMessages(int id, string text)
    {
        texts[(id % 10) - 1].MakeT(text);
        messenge[(id % 10) - 1].OrderManage();
    }

    //테스트용 퀘스트생성기능
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(MakeQuest());
        }
    }

    //퀘스트 생성
    IEnumerator MakeQuest()
    {
        //yield return new WaitForSeconds(3.0f);
        yield return null;

        if (id.Count != 0)
        {
            int id = this.id[Random.Range(0, this.id.Count)];
            this.id.Remove(id);
            string fileName = null;

            if ((int)(id / 10) == 2)
            {
                mailReceiver.Add(id, receiverName[Random.Range(0, this.receiverName.Count)]);
                fileName = this.titleName[Random.Range(0, this.titleName.Count)];

                Debug.Log(mailReceiver[id]);
                Debug.Log(fileName);
            }
            else if ((int)(id / 10) == 1)
            {
                fileName = this.fileName[Random.Range(0, this.fileName.Count)];
            }

            questList.Add(id, new QuestData(id, fileName, 300));                    //MaxTime 스크립트 작성해야함
            SendMessages(id, "new quest");
        }
        //StartCoroutine(MakeQuest());
    }

    //퀘스트성공
    public void SuccessQuest(int id)
    {
        SendMessages(id, "quest clear");
        questList.Remove(id);
    }

    //퀘스트 실패처리
    private void FixedUpdate()
    {
        //foreach()
    }

    //퀘스트실패
    public void FailQuest(int id)
    {
        questList.Remove(id);
    }
}
