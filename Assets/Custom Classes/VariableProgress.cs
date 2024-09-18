using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public class VariableProgress : Progress<float>
{
    private int _currentlyCompleted;
    private int _totalAmount;
    [OdinSerialize]
    public int CurrentlyCompleted
    {
        get { return _currentlyCompleted; }
        private set
        {
            _currentlyCompleted = value;
            ReportProgress();
        }
    }

    [OdinSerialize]
    public int TotalAmount
    {
        get { return _totalAmount; }
        private set
        {
            _totalAmount = value;
            ReportProgress();
        }
    }

    [OdinSerialize]
    public float CurrentPercentage
    {
        get
        {
            if (TotalAmount <= 0) return 0f;
            return Mathf.Clamp01(((float)CurrentlyCompleted) / TotalAmount);
        }
    }

    // Constructors
    public VariableProgress(Action<float> handler) : base(handler) { }
    public VariableProgress() : base() { }


    public void AddToTotal(int amountToAdd = 1)
    {
        if (amountToAdd < 0)
            throw new ArgumentOutOfRangeException(nameof(amountToAdd), "Amount to add cannot be negative.");

        TotalAmount += amountToAdd;
    }

    public void AddToCompleted(int amountToAdd = 1)
    {
        if (amountToAdd < 0)
            throw new ArgumentOutOfRangeException(nameof(amountToAdd), "Amount to add cannot be negative.");

        CurrentlyCompleted += amountToAdd;
    }

    private void ReportProgress()
    {
        if (_totalAmount > 0)
        {
            OnReport(CurrentPercentage);
        }
    }

    public void Complete()
    {
        TotalAmount = CurrentlyCompleted = 1;
    }

    protected override void OnReport(float value)
    {
        base.OnReport(value);
    }
}
