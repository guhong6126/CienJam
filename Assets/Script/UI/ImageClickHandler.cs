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

}
