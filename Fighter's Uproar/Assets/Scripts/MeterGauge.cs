using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterGauge : MonoBehaviour
{
    public Sprite meterNormal, meterLow;
    public MeterSystem meterSystem;

    private Image gaugeImage;

    private void Awake()
    {
        gaugeImage = transform.Find("Gauge").GetComponent<Image>();
    }

    private void Start()
    {
        SetMeter(1.0f);
    }

    private void Update()
    {
        SetMeter(meterSystem.GetMeterNormalized());
        if (meterSystem.GetMeterNormalized() < 0.20f)
            gaugeImage.sprite = meterLow;
        else
            gaugeImage.sprite = meterNormal;
    }

    private void SetMeter(float meterNormalized)
    {
        gaugeImage.fillAmount = meterNormalized;
    }

    public void ResetGauge()
    {
        gaugeImage.fillAmount = 1.0f;
    }
}
