using UnityEngine;
using System.Collections.Generic;

public class 공전2 : MonoBehaviour
{
    public float mass = 1.0f;  

    public float radius = 1.0f;                  
    public Vector3 rotationAxis = Vector3.up;   
    private float angularVelocity; 

    public Vector3 initialVelocity; 
    private Vector3 velocity; 

    public List<공전2> otherBodies; 

    private static readonly float G = 6.6743f * Mathf.Pow(10, -11) * 1000000;  

    void Start()
    {
        // 초기 속도 설정
        velocity = initialVelocity;
        행성추가();
        자전();
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

        // 각속도에 따른 자전 구현
        transform.Rotate(rotationAxis, angularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime);
    }
    public void 행성추가()
    {
        // 태그가 "행성"인 모든 오브젝트를 찾아서 otherBodies 리스트에 추가
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("행성");
        foreach (GameObject obj in taggedObjects)
        {
            공전2 body = obj.GetComponent<공전2>();
            if (body != null && body != this)
            {
                otherBodies.Add(body);
            }
        }
    }
    public void 자전()
    {
        float momentOfInertia = (2.0f / 5.0f) * mass * Mathf.Pow(radius, 2);

        // 초기 각운동량 설정 (L = I * ω)
        float initialAngularMomentum = momentOfInertia * 0.1f;  // 0.1은 초기 자전 속도 비율
        angularVelocity = initialAngularMomentum / momentOfInertia;
    }
}
