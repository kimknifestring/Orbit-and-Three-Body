using UnityEngine;
using UnityEngine.UI; 
using System.Collections;


public class BF : MonoBehaviour
{
    public RectTransform uiElement;  // 애니메이션을 적용할 UI 요소 (예: Image, Text 등)
    float shrinkSpeed = 700f;   // 높이가 줄어드는 속도
    float originalHeight = 1080f; // 원래 높이

    private bool isFalling = false;  // UI가 축소 중인지 여부
    private bool isRecovering = false; // 복구 중인지 여부

    void Update()
    {
        // 0키 눌리면 UI가 쓰러짐
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!isFalling && !isRecovering)
            {
                isFalling = true;  // 쓰러짐 시작
            }
        }

        // P 눌리면 UI가 복구됨
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isFalling && !isRecovering)
            {
                isRecovering = true;  // 복구 시작
            }
        }

        // UI 요소가 쓰러지고 있으면
        if (isFalling)
        {
            // 높이를 줄여나가며 크기를 축소
            if (uiElement.sizeDelta.y > 0)
            {
                float newHeight = Mathf.Max(0, uiElement.sizeDelta.y - shrinkSpeed * Time.deltaTime);
                uiElement.sizeDelta = new Vector2(uiElement.sizeDelta.x, newHeight);
            }
            else
            {
                isFalling = false; // 쓰러짐 종료
            }
        }

        // UI 요소가 복구 중이면
        if (isRecovering)
        {
            // 높이를 원래 크기로 되돌림
            if (uiElement.sizeDelta.y < originalHeight)
            {
                float newHeight = Mathf.Min(originalHeight, uiElement.sizeDelta.y + shrinkSpeed * Time.deltaTime);
                uiElement.sizeDelta = new Vector2(uiElement.sizeDelta.x, newHeight);
            }
            else
            {
                isRecovering = false; // 복구 완료
            }
        }
    }
}
