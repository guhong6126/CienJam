using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    TimeManager time;
    float cycle = 3.0f;
    int maxQuest = 5;
    int successCount = 0;
    int failCount = 0;

    //채팅 관련 클래스들
    [SerializeField] List<MakeText> texts = new List<MakeText>();
    [SerializeField] List<MessengerManager> messenge = new List<MessengerManager>();

    //공통
    List<int> id = new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 27, 28, 29 };                                         //존재하는 id 리스트로 정리
    public Dictionary<int, QuestData> questList;                                         //현재 수락한 퀘스트 정리
    Dictionary<int, string> successText = new Dictionary<int, string>() { { 1, "확인했어요, 수고 많으셨습니다!" }, { 2, "잘 받았습니다. 감사합니다." }, { 3, "고마워!!!!!! =^.^=" }, { 4, "확인했어. 수고했다" }, { 5, "확인." }, { 6, "확인했습니다. 다음부턴 빨리 좀 주세요." }, { 7, "확인했다." }, { 8, "확인완료, 수고 많았다." }, { 9, "잘 확인했어, 고마워!" } };
    Dictionary<int, string> failText = new Dictionary<int, string>() { { 1, "바쁘시다면 하는 수 없죠... 다음에는 더 빨리 부탁드릴게요!" }, { 2, "안보내셔도 괜찮을 것 같습니다." }, { 3, "안돼!!!너무 늦어버렸잖아!! ( ,_,)" }, { 4, "너무 늦었군. 앞으론 더 빨리 해줘." }, { 5, "다른 사람에게 부탁했습니다." }, { 6, "늦어도 너무 늦어요. 됐습니다, 다른 사람에게 부탁할게요." }, { 7, "이미 늦었다. 괜찮아." }, { 8, "늦어버렸네. 그런 김에 내 개그 하나만 더 듣고가겠나? 지하철이 믿는 사상은? \"발빠짐주의\"! 하하하!" }, { 9, "늦었네. 괜찮아, 다음번엔 더 빨리 보내줘." } };

    //파일(10번대)에 사용
    List<string> fileName = new List<string>() { "2024_분기별_매출_보고서.xlsx", "8월_프로젝트_진행_현황_보고서.pptx", "9월_신규_고객_리스트_업데이트.xlsx", "주간_업무_계획서_7월_둘째주.docx", "마케팅_캠페인_결과_분석.pptx", "제품_개발_타임라인.xlsx", "회의_자료_2024_08_20.docx", "팀_내_업무_분장표.xlsx", "월별_예산_배분_계획.xlsx", "계약서_초안_검토_의견.docx", "2024_11월_프로젝트_예산_계획.xlsx", "고객_만족도_조사_결과_제품0025.pptx", "고객_만족도_조사_결과_제품0028.pptx", "고객_만족도_조사_결과_제품0029.pptx", "고객_만족도_조사_결과_제품0033.pptx", "연간_마케팅_플랜_보고서.docx", "3월_부서별_성과_분석_2024.xlsx", "신제품_출시_일정_관리.xlsx", "내부_교육_자료_정리.pptx", "협력사_계약_현황_리스트.xlsx", "팀_목표_설정_및_성과_평가.docx", "프로모션_기획서_초안.docx", "프로젝트_예산_계획_2024_1월.xlsx", "경쟁사_동향_분석_보고서.pptx", "경쟁사_동향_분석_보고서_최종.pptx", "경쟁사_동향_분석_보고서_최최종.pptx", "경쟁사_동향_분석_보고서_리얼최최최종.pptx" };                             //존재하는 파일이름 리스트로 정리
    List<List<int>> difficulty1 = new List<List<int>>() { new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 2, 4 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 2, 3 }, new List<int> { 2, 3 }, new List<int> { 2, 3, 4 }, new List<int> { 4 } }; 
    Dictionary<int, string> textData1 = new Dictionary<int, string>() { { 11, "안녕하세요! 혹시 {0} 파일 좀 보내주실 수 있을까요? 번거로우시겠지만 부탁드려요. 감사합니다!" }, { 21, "혹시 가능하시면 {0} 파일 좀 보내주실 수 있을까요? 필요해서 그런데, 늦지 않게 보내주시면 정말 감사하겠습니다. 오늘 안에 부탁드릴게요." }, { 31, "바쁘신 와중에 죄송한데요, 혹시 가능하시면 {0} 파일을 보내주실 수 있을까요? 제가 좀 급하게 필요하게 된 상황이라서요. 시간이 되실 때 편한 시간에 보내주시면 정말 감사하겠습니다. 늦어지지 않게 챙겨주시면 정말 큰 도움이 될 것 같아요." }, { 41, "지금 바로 {0} 파일이 필요합니다. 정말 급한 상황이라 최대한 빨리 보내주세요. 부탁드립니다, 지금 바로요!" }, { 12, "혹시 괜찮으시면 {0} 파일 좀 보내주실 수 있을까요? 지금 필요한 상황이라서요. 오늘 안으로 받을 수 있으면 정말 좋겠습니다." }, { 22, "바쁘시겠지만 {0} 파일 좀 보내주실 수 있을까요? 지금 필요해서요. 시간이 되시면 최대한 빨리 부탁드려요." }, { 32, "죄송한데 {0} 파일 보내주실 수 있으실까요? 상황이 좀 급해져서요. 최대한 빨리 부탁드립니다." }, { 42, "지금 당장 {0} 파일 보내주셔야 할 것 같습니다. 시간이 너무 촉박해서요, 최대한 빨리 부탁드립니다. 정말 급한 상황입니다, 바로 보내주시면 감사하겠습니다." }, { 13, "{0}!!!!!!! 오늘 안으로 부탁할게 >.ㅇ" }, { 23, "파일 좀 부탁할게. 내가 문서 정리를 좀 더럽게 했어, 양해해줘!" }, { 43, "큰일이야!!!! 회의가 당장 코앞인데 자료를 깜빡했어! 당장 {0} 파일 좀 부탁할게! 지금 당장!!!" }, { 14, "{0} 파일 좀 빨리 보내줘. 오늘 중으로 부탁할게." }, { 24, "{0} 파일 좀 보내줘. 지금 필요해서 그러니까 최대한 빨리 부탁해." }, { 34, "급해서 그러는데 {0} 파일 지금 당장 보내. 서둘러서 보내줘야 할 것 같아." }, { 44, "{0} 파일 좀 서둘러서 보내줘야 할 것 같아. 상황이 급하니까 바로 처리해줘." }, { 25, "{0} 파일 보내주셔야 할 것 같은데요. 제가 기다리고 있으니까 늦지 않게 보내주시길 바랍니다." }, { 35, "{0} 파일 말인데, 언제쯤 보내실 건가요? 최대한 빨리 보내주셨으면 합니다." }, { 26, "{0} 파일 아직 못 받았습니다. 빨리 보내주시면 좋겠습니다. 시간 너무 끌지 마시고요." }, { 36, "{0} 파일 좀 보내주시죠? 지금 당장 필요해서 그러는데, 빨리 보내주시면 좋겠네요." }, { 27, "{0} 파일 아직도 안 왔네. 빨리 보내라, 더 기다리기 싫다." }, { 37, "{0} 파일 보내라니까. 늦지 말고 빨리 보내." }, { 47, "지금 당장 {0} 파일 보내라. 시간 없으니까 최대한 빨리 부탁한다. 지금 바로 필요하다." }, { 48, "내가 MZ 조크 하나 배워왔어. 달 뒤엔 무엇이 있는지 아나? 달뒤 달뒤 단 밤양갱~ 하하, 나도 엠제트에 좀 더 가까워졌을까? 아무튼, 용건은 {0} 파일좀 보내줘. 되도록 빨리 부탁할게." } };


    //메일(20번대)에 사용
    public Dictionary<int, string> mailReceiver;                                         //메일 수신자 정리
    List<string> receiverName = new List<string>() {"team.leader@company.com", "project.manager@company.com", "business.strategy@company.com", "global.operations@company.com", "marketing.expert@company.com", "customer.success@company.com", "sales.director@company.com", "finance.controller@company.com", "hr.recruiter@company.com", "innovation.lead@company.com", "digital.strategy@company.com", "client.relations@company.com" };
    List<string> titleName = new List<string>() { "[신제품 출시 프로젝트] 8월 3주차 진행 상황 보고 및 다음 단계 안내", "[마케팅 팀] 8월 25일 회의 일정 및 준비 자료 안내", "[긴급] 서버 오류 관련 원인 분석 및 해결 방안 협의 요청", "[긴급] 8월 인사 평가 관련 회신 요청", "[마감 임박] 8월 월간 실적 보고서 제출 요청", "[신제품 출시 프로젝트] 9월 1주차 진행 상황 및 디자인 확정 요청", "[임원 회의] 9월 4일 오후 2시, 주요 안건 및 준비사항 안내", "[사내 교육] 데이터 분석 기초 과정 등록 안내", "[행사 안내] 2024년 연례 컨퍼런스 참가 신청 시작 (조기 등록 할인 안내)", "[8월 월간 보고서] 개발팀 성과 및 향후 계획 공유", "[사내 교육] 데이터 분석 워크숍 참가 신청 마감 안내", "[긴급] 시스템 장애 관련 긴급 회의 요청" };
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

        if (id.Count != 0 && (questList.Count < maxQuest))
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
            }
            else if ((int)(id / 10) == 1)
            {
                fileName = this.fileName[Random.Range(0, this.fileName.Count)];
                difficulty = difficulty1[id%10 - 1][Random.Range(0, difficulty1[id%10 - 1].Count)];
                if (difficulty == 4) maxTime = 30;
                else if (difficulty == 3) maxTime = 60;
                else if (difficulty == 2) maxTime = 180;
                else maxTime = 0;
                SendMessages(id, textData1[difficulty * 10 + id % 10], fileName, null);
            }

            questList.Add(id, new QuestData(id, fileName, 0));
        }
        SetCycle();
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
            cycle = Random.Range(10f, 15f);
        }
        else
        {
            cycle = Random.Range(5f, 30f);
        }
    }

    //퀘스트 성공처리
    public void SuccessQuest(int id)
    {
        SendMessages(id, successText[id % 10], null, null);
        questList.Remove(id);
        if ((int)(id / 10) == 2) mailReceiver.Remove(id);
        this.id.Add(id);
        successCount += 1;
        PrintPoint();
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
        this.id.Add(id);
        failCount += 1;
        PrintPoint();
    }

    public void PrintPoint()
    {
        Debug.Log(string.Format("성공{0} / 실패{1}", successCount, failCount));
    }
}
