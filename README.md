# 행성의 운동과 삼체 문제 시뮬레이션 🪐
**Unity & C# a N-Body Gravity Simulation Exploring Deterministic Chaos**

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-11-blue?style=for-the-badge&logo=c-sharp&logoColor=white)

---

## ✨ 주요 기능

* **실시간 N-Body 중력 시뮬레이션**: 뉴턴의 만유인력 법칙($$F = G \frac{m_1 m_2}{r^2}$$)을 C# 스크립트로 직접 구현하여 모든 천체 간의 상호작용을 계산합니다.
* **궤적 시각화**: Unity의 `LineRenderer`를 이용해 천체의 이동 경로를 실시간으로 렌더링하여 궤적을 확인할 수 있습니다.
* **두 가지 시뮬레이션 모드**:
    * **태양계 (Solar System)**
    * **삼체 문제 (Three-Body Problem)**
* **인터랙티브 컨트롤**:
    * `N` 키 / `M` 키: **항성계**와 **삼체 문제** 모드를 즉시 전환합니다.
    * `숫자 키 (1, 2, ...)`: `Cinemachine`으로 구현된 다양한 카메라 뷰로 전환하며 역동적인 관찰이 가능합니다.

---

## 🚀 시뮬레이션 결과

* **항성계**:
    * <img src="./항성계.gif" width="500">
* **삼체문제**:
    * <img src="./삼체문제.gif" width="500">

---

## 🛠️ 기술 구현 핵심

### 수치 해석: 오일러 방법 (Euler Method)

연속적인 시간의 물리 현상을 컴퓨터의 불연속적인 프레임으로 옮기기 위해 **오일러 방법**을 적용했습니다. Unity의 고정된 시간 간격(`FixedUpdate`)마다 모든 천체에 작용하는 힘(가속도)을 계산하고, 이를 바탕으로 각 천체의 속도와 위치를 업데이트합니다.

```
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

```

