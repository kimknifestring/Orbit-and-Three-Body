using UnityEngine;

public class ppt전환 : MonoBehaviour
{
    public Material targetMaterial; 
    public Texture2D[] textures;    
    private int currentIndex = 0;   

    private void Start()
    {
        targetMaterial.SetTexture("_BaseMap", textures[0]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = textures.Length - 1;
            targetMaterial.SetTexture("_BaseMap", textures[currentIndex]);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            if (currentIndex >= textures.Length) currentIndex = 0;
            targetMaterial.SetTexture("_BaseMap", textures[currentIndex]);
        }
    }
}
