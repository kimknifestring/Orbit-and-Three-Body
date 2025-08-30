using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class 화면전환 : MonoBehaviour
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

        tileMaterial.SetFloat("_Progress", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartDissolveEffect();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartDissolveEffect2();
        }
    }


    void StartDissolveEffect()
    {
        StartCoroutine(DissolveEffect());
    }

    void StartDissolveEffect2()
    {
        StartCoroutine(DissolveEffect2());
    }

    IEnumerator DissolveEffect()
    {
        float dissolve = 1;
        while (dissolve > 0)
        {
            dissolve -= Time.deltaTime * dissolveSpeed;
            dissolve = Mathf.Max(dissolve, 0); // 0 이하로 내려가지 않도록
            tileMaterial.SetFloat("_Progress", dissolve); // 셰이더 속성 이름 수정
            yield return null;
        }
        SceneManager.LoadScene("3학년");
    }

    IEnumerator DissolveEffect2()
    {
        float dissolve = 1;
        while (dissolve > 0)
        {
            dissolve -= Time.deltaTime * dissolveSpeed;
            dissolve = Mathf.Max(dissolve, 0); // 0 이하로 내려가지 않도록
            tileMaterial.SetFloat("_Progress", dissolve); // 셰이더 속성 이름 수정
            yield return null;
        }
        SceneManager.LoadScene("샘플");
    }
}
