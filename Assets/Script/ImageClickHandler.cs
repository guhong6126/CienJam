using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    // 새로운 이미지를 생성할 부모 오브젝트 (예: Content의 부모)
    public Transform imageParent;
    // 생성할 이미지의 프리팹
    public GameObject newImagePrefab;
    // 새 이미지가 생길 위치
    public Vector3 newPosition;

    // 클릭된 이미지에 연결할 메서드
    public void OnImageClick()
    {
        // 새 이미지 생성
        GameObject newImage = Instantiate(newImagePrefab, imageParent);
        // 새 이미지의 위치 설정 (이 부분은 원하는 위치에 따라 조정 가능)
        newImage.transform.localPosition = newPosition;
    }
}
