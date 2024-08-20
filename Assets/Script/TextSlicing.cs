using UnityEngine;
using System.Collections.Generic;

public class TextSlicing : MonoBehaviour
{
    public List<string> emailList = new List<string>();
    public List<string> contentList = new List<string>();
    public List<string> versionList = new List<string>();

    void Start()
    {
        string input = @"
        team.leader@company.com \ [신제품 출시 프로젝트] 8월 3주차 진행 상황 보고 및 다음 단계 안내 \ 프로젝트
        project.manager@company.com \ [마케팅 팀] 8월 25일 회의 일정 및 준비 자료 안내 \ 회의
        business.strategy@company.com \ [긴급] 서버 오류 관련 원인 분석 및 해결 방안 협의 요청 \ 서버 오류
        global.operations@company.com \ [긴급] 8월 인사 평가 관련 회신 요청 \ 인사 평가 
        marketing.expert@company.com \ [마감 임박] 8월 월간 실적 보고서 제출 요청 \ 월간 실적
        customer.success@company.com \ [신제품 출시 프로젝트] 9월 1주차 진행 상황 및 디자인 확정 요청 \ 프로젝트
        sales.director@company.com \ [임원 회의] 9월 4일 오후 2시, 주요 안건 및 준비사항 안내 \ 회의
        finance.controller@company.com \ [사내 교육] 데이터 분석 기초 과정 등록 안내 \ 사내 교육
        hr.recruiter@company.com \ [행사 안내] 2024년 연례 컨퍼런스 참가 신청 시작 (조기 등록 할인 안내) \ 컨퍼런스
        innovation.lead@company.com \ [8월 월간 보고서] 개발팀 성과 및 향후 계획 공유 \ 보고서
        digital.strategy@company.com \ [사내 교육] 데이터 분석 워크숍 참가 신청 마감 안내 \ 워크숍
        client.relations@company.com \ [긴급] 시스템 장애 관련 긴급 회의 요청  \ 긴급 회의";

        
        string[] lines = input.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] parts = line.Split(new[] { '\\' }, System.StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 3)
            {
                emailList.Add(parts[0].Trim());
                contentList.Add(parts[1].Trim());
                versionList.Add(parts[2].Trim());
            }
        }

        //Debug.Log("Emails:");
        //emailList.ForEach(Debug.Log);

        //Debug.Log("\nContents:");
        //contentList.ForEach(Debug.Log);

        //Debug.Log("\nVersions:");
        //versionList.ForEach(Debug.Log);
    }
}
