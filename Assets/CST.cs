using UnityEngine;

public class CST : MonoBehaviour
{
    public CST[] cameras;  
    private int currentCameraIndex = 0; 

    void Start()
    {
        SwitchCamera(currentCameraIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            SwitchCamera((currentCameraIndex + 1) % cameras.Length); 
        }
    }

    void SwitchCamera(int index)
    {
        if (index < 0 || index >= cameras.Length) return;


        cameras[currentCameraIndex].gameObject.SetActive(false);


        cameras[index].gameObject.SetActive(true);

        currentCameraIndex = index;
    }
}
