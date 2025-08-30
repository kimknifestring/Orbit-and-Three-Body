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
            Debug.LogError("tileMaterial이 연결되지 않았습니다!");
            return;
        }

        tileMaterial.SetFloat("_Progress", 0);
        tileMaterial.SetFloat("Transition Direction", 1);
        StartCoroutine(DissolveEffect());
    }

    IEnumerator DissolveEffect()
    {
        float dissolve = 0; // 1에서 시작해 0으로 감소 (fade out)
        while (dissolve < 1)
        {
            dissolve += Time.deltaTime * dissolveSpeed;
            dissolve = Mathf.Min(dissolve, 1);
            tileMaterial.SetFloat("_Progress", dissolve); // 셰이더 속성 이름 수정
            yield return null;
        }
    }
}
