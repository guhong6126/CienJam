using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mailSelect : MonoBehaviour
{
    public TMP_InputField inputField;  // Input Field 컴포넌트
    public ScrollRect scrollView;      // Scroll View 컴포넌트

    // Start 메서드에서 초기 설정
    private void Start()
    {
        // Scroll View 내의 모든 이미지를 가져옵니다.
        Image[] images = scrollView.content.GetComponentsInChildren<Image>();

        // 각 이미지에 클릭 이벤트를 연결합니다.
        foreach (Image image in images)
        {
            // Button 컴포넌트가 없으면 추가
            Button button = image.GetComponent<Button>();
            if (button == null)
            {
                button = image.gameObject.AddComponent<Button>();
            }

            // 클릭 이벤트 리스너 추가
            button.onClick.AddListener(() => OnImageClick(image));
        }
    }

    // 이미지 클릭 시 호출되는 메서드
    private void OnImageClick(Image clickedImage)
    {
        // 클릭된 이미지의 자식 오브젝트로 텍스트(TMP_Text)를 찾습니다.
        TMP_Text imageText = clickedImage.GetComponentInChildren<TMP_Text>();

        if (imageText != null)
        {
            // 텍스트를 Input Field에 설정
            inputField.text = imageText.text;

            // Scroll View를 비활성화
            scrollView.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("이미지에 텍스트가 연결되어 있지 않습니다.");
        }
    }
}
