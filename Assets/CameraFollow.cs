using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ���� ���
    public Vector3 offset = new Vector3(0, 5, -10); // ī�޶��� �ʱ� ��ġ ������
    public float smoothSpeed = 0.125f; // 0-1��

    void LateUpdate()
    {
        if (target != null)
        {
            // ��ǥ ��ġ ��� (����� ��ġ + ���������� �����)
            Vector3 desiredPosition = target.position + offset;
            // �ε巴�� �̵�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // ī�޶� �׻� ��� �ٶ󺸱� �������ֱ�
            transform.LookAt(target);
        }
    }
}
