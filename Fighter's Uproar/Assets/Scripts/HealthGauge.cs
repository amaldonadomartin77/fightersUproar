using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{
    public Sprite healthFull, healthNormal, healthLow;
    public HealthSystem healthSystem;

    private const float DAMAGE_SHRINK_TIMER_MAX = 1.0f;

    private Image gaugeImage;
    private Image damageGaugeImage;
    private float damageShrinkTimer;

    private void Awake()
    {
        gaugeImage = transform.Find("Gauge").GetComponent<Image>();
        damageGaugeImage = transform.Find("DamageGauge").GetComponent<Image>();
    }

    private void Start()
    {
        SetHealth(1.0f);
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }

    private void Update()
    {
        if (healthSystem.GetHealthNormalized() == 1.0f)
            gaugeImage.sprite = healthFull;
        else if (healthSystem.GetHealthNormalized() <= 0.25f)
            gaugeImage.sprite = healthLow;
        else
            gaugeImage.sprite = healthNormal;

        damageShrinkTimer -= Time.deltaTime;
        if (damageShrinkTimer < 0)
        {
            if (gaugeImage.fillAmount < damageGaugeImage.fillAmount)
                damageGaugeImage.fillAmount -= 1f * Time.deltaTime;
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        damageShrinkTimer = DAMAGE_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void SetHealth(float healthNormalized)
    {
        gaugeImage.fillAmount = healthNormalized;
    }
}
