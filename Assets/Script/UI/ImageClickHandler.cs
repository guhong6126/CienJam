using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    // �˾�â���� ����� �г� (������ �ִ� UI �г�)
    public GameObject popupPanel = null;

    public void OnImageClick()
    {
        // �˾� �г��� Ȱ��ȭ ���¸� ��� (���������� ����, ���������� �ѱ�)
        bool isActive = popupPanel.activeSelf;
        popupPanel.SetActive(!isActive);
    }

    public GameObject InputTextFalse = null;
    public GameObject InputTextTrue = null;

    // �ٸ� ��ư�� ������ �޼���
    public void ImageFalse()
    {
        InputTextFalse.SetActive(false);
    }

    public void ImageTrue()
    {
        InputTextTrue.SetActive(true);
    }

    // Scroll View�� Content ������Ʈ
    public Transform content = null;
    // �߰��� �̹����� ������
    public GameObject imagePrefab = null;

    public void AcceptQuest()
    {
        // Content �ȿ� �̹��� �������� ����
        GameObject newImage = Instantiate(imagePrefab, content);

        // �ʿ信 ���� ��ġ�� ũ�� ���� ����
        // newImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }
}
