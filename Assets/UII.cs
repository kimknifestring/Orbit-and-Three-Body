using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    public Image displayImage;  
    public Sprite[] images; 
    private int currentIndex = 0; 

    void Update()
    {
        // ���� �̹���
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = images.Length - 1;  
            UpdateImage();
        }
        // �����̹̤���
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            if (currentIndex >= images.Length) currentIndex = 0;  
            UpdateImage();
        }
    }

    // �̹��� ���� �Լ�
    void UpdateImage()
    {
        displayImage.sprite = images[currentIndex];
    }
}
