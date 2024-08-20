using System;
using UnityEngine;
using UnityEngine.UI;

public class ResizeParentOnTextChange : MonoBehaviour
{
    public RectTransform textRectTransform;  // 텍스트 박스의 RectTransform
    public RectTransform imageRectTransform; // 이미지의 RectTransform
    public RectTransform parentRectTransform; // 부모 오브젝트의 RectTransform
    public Vector2 padding; // 텍스트와 이미지 간의 패딩

    private Text uiText;

    void Start()
    {
        uiText = textRectTransform.GetComponent<Text>();
        AdjustSizes();
    }

    private void Update()
    {
        AdjustSizes();
    }

    void AdjustSizes()
    {
        // 텍스트 박스의 현재 높이
        float currentTextHeight = textRectTransform.rect.height;

        // 이미지의 새로운 크기 계산 (가로는 고정, 세로는 텍스트 높이에 패딩을 추가한 값)
        Vector2 newImageSize = new Vector2(
            imageRectTransform.sizeDelta.x, // 가로 크기 고정
            currentTextHeight + padding.y // 세로 크기: 텍스트 높이에 패딩 추가
        );

        // 이미지의 크기 조정
        imageRectTransform.sizeDelta = newImageSize;

        // 이미지의 pivot 설정 (이미지가 아래로만 확장되도록)
        imageRectTransform.pivot = new Vector2(0.5f, 1f);

        // 부모 오브젝트의 새로운 크기 계산
        float totalHeight = newImageSize.y + padding.y; // 텍스트와 이미지의 전체 높이 계산

        // 부모 오브젝트의 크기를 조정 (가로는 유지, 세로는 이미지와 텍스트에 맞게 조정)
        parentRectTransform.sizeDelta = new Vector2(
            parentRectTransform.sizeDelta.x, // 가로 크기는 유지
            totalHeight // 세로 크기를 이미지와 텍스트의 높이에 맞게 조정
        );

        // 부모 오브젝트의 pivot 설정 (부모 오브젝트도 아래로만 확장되도록)
        parentRectTransform.pivot = new Vector2(0.5f, 1f);

        // 부모 오브젝트의 anchoredPosition 조정 (부모 오브젝트가 아래로 확장되도록)
        parentRectTransform.anchoredPosition = new Vector2(
            parentRectTransform.anchoredPosition.x, // 가로 위치는 유지
            parentRectTransform.anchoredPosition.y - (totalHeight - currentTextHeight) / 2 // 세로 위치 조정
        );
    }
}