using UnityEngine;
using System.Collections.Generic;

public class 공전 : MonoBehaviour
{
    public float mass = 1.0f;  
    public Vector3 initialVelocity;  
    private Vector3 velocity;  

    public List<공전> otherBodies;

    private static readonly float G = 6.6743f * Mathf.Pow(10, -11)*1000000;  // 중력 상수 그냥 쓰면 계산이 너무 오래 걸리니까 극단적으로 크게 설정

    void Start()
    {
        velocity = initialVelocity;
    }

    void FixedUpdate()
    {
        Vector3 totalForce = Vector3.zero;

        // 각 다른 질량체에 대해 중력을 계산
        foreach (var otherBody in otherBodies)
        {
            if (otherBody == this) continue; // 자기 자신은 무시

            // 두 물체 사이의 거리와 방향 계산
            Vector3 direction = otherBody.transform.position - transform.position;
            float distance = direction.magnitude;

            // 만유인력 계산: F = G * (m1 * m2) / r^2
            float forceMagnitude = G * (mass * otherBody.mass) / Mathf.Pow(distance, 2);
            Vector3 force = direction.normalized * forceMagnitude;

            // 전체 힘에 추가
            totalForce += force;
        }

        // 가속도 계산 및 속도 업데이트
        Vector3 acceleration = totalForce / mass;
        velocity += acceleration * Time.fixedDeltaTime;

        // 위치 업데이트
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
