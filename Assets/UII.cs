using UnityEngine;
using UnityEngine.UI;

public class ImageSlideshow : MonoBehaviour
{
    public Image displayImage;  
    public Sprite[] images; 
    private int currentIndex = 0; 

    void Update()
    {
        // 이전 이미지
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = images.Length - 1;  
            UpdateImage();
        }
        // 다음이미ㅣ지
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            if (currentIndex >= images.Length) currentIndex = 0;  
            UpdateImage();
        }
    }

    // 이미지 변경 함수
    void UpdateImage()
    {
        displayImage.sprite = images[currentIndex];
    }
}
