using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mailSelect : MonoBehaviour
{
    public TMP_InputField inputField;  // Input Field ������Ʈ
    public ScrollRect scrollView;      // Scroll View ������Ʈ

    // Start �޼��忡�� �ʱ� ����
    private void Start()
    {
        // Scroll View ���� ��� �̹����� �����ɴϴ�.
        Image[] images = scrollView.content.GetComponentsInChildren<Image>();

        // �� �̹����� Ŭ�� �̺�Ʈ�� �����մϴ�.
        foreach (Image image in images)
        {
            // Button ������Ʈ�� ������ �߰�
            Button button = image.GetComponent<Button>();
            if (button == null)
            {
                button = image.gameObject.AddComponent<Button>();
            }

            // Ŭ�� �̺�Ʈ ������ �߰�
            button.onClick.AddListener(() => OnImageClick(image));
        }
    }

    // �̹��� Ŭ�� �� ȣ��Ǵ� �޼���
    private void OnImageClick(Image clickedImage)
    {
        // Ŭ���� �̹����� �ڽ� ������Ʈ�� �ؽ�Ʈ(TMP_Text)�� ã���ϴ�.
        TMP_Text imageText = clickedImage.GetComponentInChildren<TMP_Text>();

        if (imageText != null)
        {
            // �ؽ�Ʈ�� Input Field�� ����
            inputField.text = imageText.text;

            // Scroll View�� ��Ȱ��ȭ
            scrollView.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("�̹����� �ؽ�Ʈ�� ����Ǿ� ���� �ʽ��ϴ�.");
        }
    }
}
