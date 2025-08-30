using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class fade : MonoBehaviour
{
    public Material tileMaterial;
    public float dissolveSpeed = 1.0f;

    void Start()
    {
        if (tileMaterial == null)
        {
            Debug.LogError("tileMaterial�� ������� �ʾҽ��ϴ�!");
            return;
        }

        tileMaterial.SetFloat("_Progress", 0);
        tileMaterial.SetFloat("Transition Direction", 1);
        StartCoroutine(DissolveEffect());
    }

    IEnumerator DissolveEffect()
    {
        float dissolve = 0; // 1���� ������ 0���� ���� (fade out)
        while (dissolve < 1)
        {
            dissolve += Time.deltaTime * dissolveSpeed;
            dissolve = Mathf.Min(dissolve, 1);
            tileMaterial.SetFloat("_Progress", dissolve); // ���̴� �Ӽ� �̸� ����
            yield return null;
        }
    }
}
