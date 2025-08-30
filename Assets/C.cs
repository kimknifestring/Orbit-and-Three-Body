
using Unity.Cinemachine;
using UnityEngine;

public class C : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; 
    private int currentCameraIndex = 0; 
    public float desiredFarClip = 3000f;
    void Start()
    {
        // 초기 카메라
        SwitchCamera(0);
    }

    void Update()
    {
        // 카메라 전환
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCamera(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCamera(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCamera(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchCamera(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SwitchCamera(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SwitchCamera(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SwitchCamera(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SwitchCamera(7);
    }


    void SwitchCamera(int index)
    {
        // 이전 카메라는 비활성화
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // 새로운 카메라는 활성화
        cameras[index].gameObject.SetActive(true);

        // 현재 활성화된 카메라 인덱스 업데이트
        currentCameraIndex = index;
    }
}
