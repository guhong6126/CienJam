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
        team.leader@company.com \ [����ǰ ��� ������Ʈ] 8�� 3���� ���� ��Ȳ ���� �� ���� �ܰ� �ȳ� \ ������Ʈ
        project.manager@company.com \ [������ ��] 8�� 25�� ȸ�� ���� �� �غ� �ڷ� �ȳ� \ ȸ��
        business.strategy@company.com \ [���] ���� ���� ���� ���� �м� �� �ذ� ��� ���� ��û \ ���� ����
        global.operations@company.com \ [���] 8�� �λ� �� ���� ȸ�� ��û \ �λ� �� 
        marketing.expert@company.com \ [���� �ӹ�] 8�� ���� ���� ���� ���� ��û \ ���� ����
        customer.success@company.com \ [����ǰ ��� ������Ʈ] 9�� 1���� ���� ��Ȳ �� ������ Ȯ�� ��û \ ������Ʈ
        sales.director@company.com \ [�ӿ� ȸ��] 9�� 4�� ���� 2��, �ֿ� �Ȱ� �� �غ���� �ȳ� \ ȸ��
        finance.controller@company.com \ [�系 ����] ������ �м� ���� ���� ��� �ȳ� \ �系 ����
        hr.recruiter@company.com \ [��� �ȳ�] 2024�� ���� ���۷��� ���� ��û ���� (���� ��� ���� �ȳ�) \ ���۷���
        innovation.lead@company.com \ [8�� ���� ����] ������ ���� �� ���� ��ȹ ���� \ ����
        digital.strategy@company.com \ [�系 ����] ������ �м� ��ũ�� ���� ��û ���� �ȳ� \ ��ũ��
        client.relations@company.com \ [���] �ý��� ��� ���� ��� ȸ�� ��û  \ ��� ȸ��";

        
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
