using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class ppt디졸브 : MonoBehaviour
{
    public Material dissolveShaderMaterial;
    public float dissolveSpeed = 1.0f;
    private float targetDissolveAmount = 0f;
    private float currentDissolveAmount = 0f;
    private Coroutine dissolveCoroutine;
    private void Start()
    {
        dissolveShaderMaterial.SetFloat("_Dissolve", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartDissolveEffect(1f);
            Debug.Log("O");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartDissolveEffect(0f);
            Debug.Log("P");
        }
    }

    void StartDissolveEffect(float targetValue)
    {
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }
        dissolveCoroutine = StartCoroutine(DissolveEffectCoroutine(targetValue));
    }

    IEnumerator DissolveEffectCoroutine(float targetValue)
    {
        while (Mathf.Abs(currentDissolveAmount - targetValue) > 0.01f)
        {
            currentDissolveAmount = Mathf.Lerp(currentDissolveAmount, targetValue, Time.deltaTime * dissolveSpeed);
            dissolveShaderMaterial.SetFloat("_Dissolve", currentDissolveAmount);
            yield return null;
        }

        currentDissolveAmount = targetValue;
        dissolveShaderMaterial.SetFloat("_Dissolve", currentDissolveAmount);
    }
}
