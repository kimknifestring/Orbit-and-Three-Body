using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    public Vector3 offset = new Vector3(0, 5, -10); // 카메라의 초기 위치 오프셋
    public float smoothSpeed = 0.125f; // 0-1로

    void LateUpdate()
    {
        if (target != null)
        {
            // 목표 위치 계산 (대상의 위치 + 오프셋으로 계산함)
            Vector3 desiredPosition = target.position + offset;
            // 부드럽게 이동
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 카메라가 항상 대상 바라보기 설정해주기
            transform.LookAt(target);
        }
    }
}
