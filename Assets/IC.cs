using System.Collections.Generic;
using UnityEngine;

public class IC : MonoBehaviour
{
    public List<Texture> textures; 
    public GameObject cube;
    private int currentIndex = 0;

    void Start()
    {
        UpdateCubeTexture();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNext();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePrevious();
        }
    }

    void MoveNext()
    {
        if (currentIndex < textures.Count - 1)
        {
            currentIndex++;
            UpdateCubeTexture();
        }
    }

    void MovePrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateCubeTexture();
        }
    }

    void UpdateCubeTexture()
    {
        // ť���� MeshRenderer�� ã�ư��� ������Ʈ �ϴ� ������ ����
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.mainTexture = textures[currentIndex];
    }
}
