using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action<float> TimerValueChanged;
    public event Action<bool> TimerModeChanged;
    public event Action TimerExpired;
    
    [SerializeField] private float _timerStartValue;
    private float _timeCount;
    private bool _isCounting = false;

    public float TimeCount => _timeCount;
    public bool IsCounting => _isCounting;  
    public float TimerStartValue => _timerStartValue;

    private void Awake()
    {
        _timeCount = _timerStartValue;
    }

    public void StartTimer()
    {
        _isCounting = true;
        TimerModeChanged?.Invoke(_isCounting);
    }

    public void ResetTimer()
    {
        _isCounting = false;
        _timeCount = _timerStartValue; 
        TimerModeChanged?.Invoke(_isCounting);
        TimerValueChanged?.Invoke(_timeCount);
    }

    public void PauseTimer()
    {
        _isCounting = false;
        TimerModeChanged?.Invoke(_isCounting);
    }

    private void Update()
    {
        if (_isCounting)
        {
            _timeCount -= Time.deltaTime;

            if (_timeCount <= 0)
                ExpireTimer();
            
            TimerValueChanged?.Invoke(_timeCount);
        }
    }

    private void ExpireTimer()
    {
        _isCounting = false;
        _timeCount = 0;
        TimerModeChanged?.Invoke(_isCounting);
        TimerExpired?.Invoke();
    }
}
