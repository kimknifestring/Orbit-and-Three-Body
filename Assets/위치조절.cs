using UnityEngine;

public class 위치조절 : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;

    public float heightMultiplier = 1.5f; 
    public float minHeight = 500f;          // 최소 높이 (너무 가까이 가지 않게 하려면 필요)

    void LateUpdate()
    {
        // 1. x, z 평균값 계산
        Vector3 avgPosition = (target1.position + target2.position + target3.position) / 3f;

        // 2. x, z 범위 계산
        float minX = Mathf.Min(target1.position.x, target2.position.x, target3.position.x);
        float maxX = Mathf.Max(target1.position.x, target2.position.x, target3.position.x);

        float minZ = Mathf.Min(target1.position.z, target2.position.z, target3.position.z);
        float maxZ = Mathf.Max(target1.position.z, target2.position.z, target3.position.z);

        float xRange = maxX - minX;
        float zRange = maxZ - minZ;

        float maxRange = Mathf.Max(xRange, zRange);

        // 3. y값 계산
        float desiredY = Mathf.Max(minHeight, maxRange * heightMultiplier);

        // 4. 카메라 위치 설정 (y값은 계산한 거로 사용)
        transform.position = new Vector3(avgPosition.x, desiredY, avgPosition.z);
    }
}
