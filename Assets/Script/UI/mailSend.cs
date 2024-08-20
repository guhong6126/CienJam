using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mailSend : MonoBehaviour
{
    public GameObject targetImage;  // Ȱ��ȭ�� �̹��� ������Ʈ
    public Button activateButton;   // �̹����� Ȱ��ȭ�� ��ư

    [SerializeField] mailSelect reciverName;

    private void Start()
    {
        activateButton.onClick.AddListener(ActivateImage);
    }

    //메일보내기 
    private void ActivateImage()
    {
        QuestManager quest = GameObject.Find("GameManager").GetComponent<QuestManager>();
        string text = reciverName.inputField.text;
        List<int> questList = new List<int>();

        foreach(KeyValuePair<int, string> kv in quest.mailReceiver)
        {
            if (kv.Value == text)
            {
                questList.Add(kv.Key);
            }
        }

        foreach(int index in questList)
        {
            if (quest.questList[index].fileName == )
            {

            }
        }

        targetImage.SetActive(true);
    }
}
