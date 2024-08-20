using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    TimeManager time;
    float cycle = 3.0f;

    //채팅 관련 클래스들
    [SerializeField] List<MakeText> texts = new List<MakeText>();
    [SerializeField] List<MessengerManager> messenge = new List<MessengerManager>();

    //공통
    List<int> id = new List<int>() {27, 28, 29};                                         //존재하는 id 리스트로 정리
    public Dictionary<int, QuestData> questList;                                         //현재 수락한 퀘스트 정리
    Dictionary<int, string> successText = new Dictionary<int, string>() { };
    Dictionary<int, string> failText = new Dictionary<int, string>() { };

    //파일(10번대)에 사용
    List<string> fileName = new List<string>() {"dd", "cc"};                             //존재하는 파일이름 리스트로 정리

    //메일(20번대)에 사용
    public Dictionary<int, string> mailReceiver;                                         //메일 수신자 정리
    List<string> receiverName = new List<string>() {"team.leader@company.com", "project.manager@company.com", "business.strategy@company.com", "global.operations@company.com", "marketing.expert@company.com", "customer.success@company.com", "sales.director@company.com", "finance.controller@company.com", "hr.recruiter@company.com", "innovation.lead@company.com", "digital.strategy@company.com", "client.relations@company.com" };
    List<string> titleName = new List<string>() {"a"};
    Dictionary<int, string> textData2 = new Dictionary<int, string>() { { 37, "{0}으로 메일좀 부탁할게. 제목은 {1}로 하는게 좋겠어. 필요한 정보 잘 정리해서, 신속하게 보내주길 바란다." }, { 38, "{0}한테 메일 작성해서 전달해줘야 할 것 같아. 제목은 {1}로 부탁해. 중요한 내용이니까 꼼꼼히 확인하고, 빨리 보내줘." }, { 39, "지금 {0}한테 메일 하나 보내줄 수 있겠어? 제목은 {1}로 하고. 가능한 한 빨리 보내주면 좋겠어. 부탁할게!" }, { 47, "{0}에게 메일 부탁한다. 제목은 {1}로 하면 될거야. 내용 잘 정리해서 빠르게 보내." }, { 48, "{0}으로 메일 작성해서 바로 보내줘야 할 것 같아. 제목은 {1}로 부탁해. 시간이 많지 않으니 서둘러서 진행해줘." }, { 49, "{0}에게 메일 좀 작성해서 보내줄래? 제목은 {1}이 좋을 것 같아. 최대한 빠르게 처리해줘." } };

    private void Start()
    {
        questList = new Dictionary<int, QuestData>();
        mailReceiver = new Dictionary<int, string>();

        time = GetComponent<TimeManager>();
        StartCoroutine(MakeQuest());
    }

    //메시지 보내기(string.Format 이용, 변수는 2개까지 가능함)
    void SendMessages(int id, string text, string var1, string var2)
    {
        texts[(id % 10) - 1].MakeT(text, var1, var2);            //1대1 채팅방에 메시지전송
        messenge[(id % 10) - 1].OrderManage();                   //채팅방 우선순위 변경
    }

    //테스트용 퀘스트생성기능(나중에 삭제할것.)
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
        yield return new WaitForSeconds(cycle);

        if (id.Count != 0)
        {
            int id = this.id[Random.Range(0, this.id.Count)];
            int maxTime = 0;
            int difficulty = 0;
            this.id.Remove(id);
            string fileName = null;

            if ((int)(id / 10) == 2)
            {
                mailReceiver.Add(id, receiverName[Random.Range(0, this.receiverName.Count)]);
                fileName = this.titleName[Random.Range(0, this.titleName.Count)];
                difficulty = Random.Range(3, 5);
                maxTime = difficulty == 3 ? 120 : 60;
                SendMessages(id, textData2[difficulty * 10 + id % 10], mailReceiver[id], fileName);

                //테스트용
                Debug.Log(mailReceiver[id]);
                Debug.Log(fileName);
                Debug.Log(maxTime);
            }
            else if ((int)(id / 10) == 1)
            {
                fileName = this.fileName[Random.Range(0, this.fileName.Count)];
                SendMessages(id, "new quest {0}", "aaaaaaa", null);
                //maxTime 값 변경 스크립트 추가해야함
            }

            questList.Add(id, new QuestData(id, fileName, maxTime));                    //MaxTime 스크립트 작성해야함
        }
        StartCoroutine(MakeQuest());
    }

    //퀘스트 생성 주기 변경
    void SetCycle()
    {
        if (time.minute < 60)
        {
            cycle = Random.Range(10f, 20f);
        }
        else if (time.minute < 120)
        {
            cycle = Random.Range(5f, 15f);
        }
        else
        {
            cycle = Random.Range(5f, 30f);
        }
    }

    //퀘스트 성공처리
    public void SuccessQuest(int id)
    {
        SendMessages(id, successText[50 + id % 10], null, null);
        questList.Remove(id);
        if ((int)(id / 10) == 2) mailReceiver.Remove(id);
        this.id.Add(id);
    }

    //퀘스트 실패판단
    private void FixedUpdate()
    {
        List<int> index = new List<int>();
        foreach (KeyValuePair<int, QuestData> kv in questList)
        {
            if (kv.Value.maxTime < time.minute)
            {
                index.Add(kv.Key);
            }
        }
        for (int i = 0; i < index.Count; i ++)
        {
            FailQuest(index[i]);
        }
    }

    //퀘스트 실패처리
    public void FailQuest(int id)
    {
        SendMessages(id, failText[id % 10], null, null);
        questList.Remove(id);
        if ((int)(id / 10) == 2) mailReceiver.Remove(id);
        Debug.Log(string.Format("remove {0}", id));
        this.id.Add(id);
    }
}
