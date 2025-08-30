
using Unity.Cinemachine;
using UnityEngine;

public class C : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras; 
    private int currentCameraIndex = 0; 
    public float desiredFarClip = 3000f;
    void Start()
    {
        // �ʱ� ī�޶�
        SwitchCamera(0);
    }

    void Update()
    {
        // ī�޶� ��ȯ
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
        // ���� ī�޶�� ��Ȱ��ȭ
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // ���ο� ī�޶�� Ȱ��ȭ
        cameras[index].gameObject.SetActive(true);

        // ���� Ȱ��ȭ�� ī�޶� �ε��� ������Ʈ
        currentCameraIndex = index;
    }
}
