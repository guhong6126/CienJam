using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TMP_Text tmp;
    [SerializeField] int maxTime;
    public int minute = 0;

    private void Start()
    {
        tmp.text = string.Format("{0:D2}:{1:D2} AM", 9, 0);
        StartCoroutine(CountTime());
    }

    //게임 표기시간 갱신
    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1.0f);
        minute += (int)(540 / maxTime);
        int hour = minute > 239 ? (int)(minute / 60) - 12 : (int)(minute / 60);
        string x = hour > 2 ? "PM" : "AM";
        tmp.text = string.Format("{0:D2}:{1:D2} {2}", hour + 9, minute % 60, x);

        if (minute >= 540)
        {
            SceneManager.LoadScene("calculate");
        }
        else
        {
            StartCoroutine(CountTime());
        }
    }
}
