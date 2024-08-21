using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class txtPrinter : MonoBehaviour
{
    public TMP_Text textComponent;       // TMP_Text 컴포넌트를 드래그하여 할당합니다.
    [TextArea(10, 20)]
    public string inputText;             // 입력 텍스트 (여러 줄 입력 가능)
    public float totalDuration = 10f;    // 전체 출력 시간
    public float minCharacterDelay = 0.02f; // 글자당 최소 출력 시간 (최소 0.02초)

    private string[] lines;              // 줄 단위로 분리된 텍스트 저장
    private float characterDelay;        // 계산된 글자당 출력 시간
    public ScrollRect scrollRect;

    public GameObject mainScreen;
    public AudioSource bootingmusic;
    

    [SerializeField] private AudioSource bottingAudio;
    
    private void Start()
    {
        bottingAudio.Play();
        // 입력된 텍스트를 줄 단위로 분리하여 배열에 저장
        lines = inputText.Split(new[] { '\n' }, System.StringSplitOptions.None);

        // 텍스트 전체 길이 계산
        int totalCharacters = inputText.Length;

        // 총 출력 시간을 고려한 글자당 출력 시간 계산
        characterDelay = totalDuration / totalCharacters;

        // 최소 출력 시간보다 작으면 최소 출력 시간으로 설정
        if (characterDelay < minCharacterDelay)
        {
            characterDelay = minCharacterDelay;
        }

        StartCoroutine(ShowTextSequentially());
    }

    private void Update()
    {
        scrollRect.verticalNormalizedPosition = 0f;
    }

    IEnumerator ShowTextSequentially()
    {
        textComponent.text = ""; // 초기 텍스트 비우기

        // 각 줄을 순차적으로 출력
        foreach (string line in lines)
        {
            yield return StartCoroutine(ShowLineWithCharacters(line));
            textComponent.text += "\n"; // 줄바꿈 추가
        }

        // 모든 텍스트 출력이 완료된 후 디버그 로그 출력
        
        
        mainScreen.SetActive(true);
        bootingmusic.Play();
        this.gameObject.SetActive(false);

    }

    IEnumerator ShowLineWithCharacters(string line)
    {
        // 현재 줄에서 글자를 한 글자씩 출력
        for (int i = 0; i < line.Length; i++)
        {
            textComponent.text += line[i]; // 한 글자 추가
            yield return new WaitForSeconds(characterDelay); // 글자 출력 간 대기
        }
    }
}