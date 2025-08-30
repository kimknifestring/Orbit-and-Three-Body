using UnityEngine;
using UnityEngine.UI; 
using System.Collections;


public class BF : MonoBehaviour
{
    public RectTransform uiElement;  // �ִϸ��̼��� ������ UI ��� (��: Image, Text ��)
    float shrinkSpeed = 700f;   // ���̰� �پ��� �ӵ�
    float originalHeight = 1080f; // ���� ����

    private bool isFalling = false;  // UI�� ��� ������ ����
    private bool isRecovering = false; // ���� ������ ����

    void Update()
    {
        // 0Ű ������ UI�� ������
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!isFalling && !isRecovering)
            {
                isFalling = true;  // ������ ����
            }
        }

        // P ������ UI�� ������
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isFalling && !isRecovering)
            {
                isRecovering = true;  // ���� ����
            }
        }

        // UI ��Ұ� �������� ������
        if (isFalling)
        {
            // ���̸� �ٿ������� ũ�⸦ ���
            if (uiElement.sizeDelta.y > 0)
            {
                float newHeight = Mathf.Max(0, uiElement.sizeDelta.y - shrinkSpeed * Time.deltaTime);
                uiElement.sizeDelta = new Vector2(uiElement.sizeDelta.x, newHeight);
            }
            else
            {
                isFalling = false; // ������ ����
            }
        }

        // UI ��Ұ� ���� ���̸�
        if (isRecovering)
        {
            // ���̸� ���� ũ��� �ǵ���
            if (uiElement.sizeDelta.y < originalHeight)
            {
                float newHeight = Mathf.Min(originalHeight, uiElement.sizeDelta.y + shrinkSpeed * Time.deltaTime);
                uiElement.sizeDelta = new Vector2(uiElement.sizeDelta.x, newHeight);
            }
            else
            {
                isRecovering = false; // ���� �Ϸ�
            }
        }
    }
}
