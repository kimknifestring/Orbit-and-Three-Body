using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class 궤도시각화 : MonoBehaviour
{
    float pointSpacing = 1f; 
    int maxPoints = 1000; 
    public Material lineMaterial; 

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private Vector3 lastPosition;

    void Start()
    {
            lineRenderer = GetComponent<LineRenderer>();
            lastPosition = transform.position;

            // LineRenderer설정
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
            lineRenderer.positionCount = 0;
            lineRenderer.numCapVertices = 10;
            lineRenderer.numCornerVertices = 10;

            lineRenderer.material = lineMaterial;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

    }

    void FixedUpdate()
    {
        // 일정 거리 이상 이동 시 새로운 점 추가
        if (Vector3.Distance(transform.position, lastPosition) >= pointSpacing)
        {
            if (points.Count >= maxPoints)
            {
                points.RemoveAt(0); // 최대 점 수를 초과하면 맨 앞 제거
            }

            points.Add(transform.position);
            lastPosition = transform.position;

            // LineRenderer에 점 적용
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
