using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.5f;  // ���콺�� �÷��� ���� ũ�� ����
    private Vector3 originalScale;

    void Start()
    {
        // ó���� �̹����� ���� ũ�⸦ ����
        originalScale = transform.localScale;
    }

    // ���콺 �ø���
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �̹��� Ȯ��
        transform.localScale = originalScale * hoverScale;
    }

    // ���콺 �� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        // �ǵ�����
        transform.localScale = originalScale;
    }
}
