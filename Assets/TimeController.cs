using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Slider timeSlider; 
    public Text timeScaleText;

    void Start()
    {
        timeSlider.onValueChanged.AddListener(UpdateTimeScale);
        UpdateTimeScale(timeSlider.value);
    }

    public void UpdateTimeScale(float value)
    {
        Time.timeScale = value;

        if (timeScaleText != null)
        {
            timeScaleText.text = "Time Scale: " + value.ToString("F1");
        }
    }
}
