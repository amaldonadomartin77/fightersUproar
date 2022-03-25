using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterSystem : MonoBehaviour
{
    public int baseMeter;
    public float meterRegenRate;

    private float meterAmount;
    private int meterAmountMax;

    public void Start()
    {
        ResetMeter();
        InvokeRepeating("Regen", 0.0f, meterRegenRate);
    }

    public void Spend(int amount)
    {
        meterAmount -= amount;
        if (meterAmount < 0)
            meterAmount = 0;
    }

    void Regen()
    {
        if (meterAmount < meterAmountMax)
            meterAmount += 0.1f;
    }

    public float GetMeterNormalized()
    {
        return (float)meterAmount / meterAmountMax;
    }

    public float GetCurrentMeter()
    {
        return meterAmount;
    }

    public void ResetMeter()
    {
        meterAmountMax = baseMeter;
        meterAmount = baseMeter;
    }
}
