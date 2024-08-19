using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    // ���ο� �̹����� ������ �θ� ������Ʈ (��: Content�� �θ�)
    public Transform imageParent;
    // ������ �̹����� ������
    public GameObject newImagePrefab;
    // �� �̹����� ���� ��ġ
    public Vector3 newPosition;

    // Ŭ���� �̹����� ������ �޼���
    public void OnImageClick()
    {
        // �� �̹��� ����
        GameObject newImage = Instantiate(newImagePrefab, imageParent);
        // �� �̹����� ��ġ ���� (�� �κ��� ���ϴ� ��ġ�� ���� ���� ����)
        newImage.transform.localPosition = newPosition;
    }
}
