using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> TimerValueChanged;
    public event Action TimerStarted;
    public event Action TimerReseted;
    public event Action TimerPaused;
    public event Action TimerExpired;
    
    private float _timerStartValue;
    private float _timeCount;
    private bool _isCounting = false;
    private MonoBehaviour _coroutineRunner;
    private Coroutine _timerCoroutine;

    public Timer(float timerStartValue, MonoBehaviour coroutineRunner)
    {
        _timerStartValue = timerStartValue;
        _coroutineRunner = coroutineRunner;
        _timeCount = _timerStartValue;
    }

    public float TimeCount => _timeCount;
    public bool IsCounting => _isCounting;  
    public float TimerStartValue => _timerStartValue;

    public void StartTimer()
    {
        if (!_isCounting)
        {
            _isCounting = true;
            _timerCoroutine = _coroutineRunner.StartCoroutine(TimerProcess());
            TimerStarted?.Invoke();
        }
    }

    public void ResetTimer()
    {
        StopTimerCoroutine();
        _isCounting = false;
        _timeCount = _timerStartValue;
        TimerReseted?.Invoke();
        TimerValueChanged?.Invoke(_timeCount);
    }

    public void PauseTimer()
    {
        if (_isCounting)
        {
            StopTimerCoroutine();
            _isCounting = false;
            TimerPaused?.Invoke();
        }
    }

    private IEnumerator TimerProcess()
    {
        while (_timeCount > 0)
        {
            _timeCount -= Time.deltaTime;
            TimerValueChanged?.Invoke(_timeCount);
            yield return null;
        }

        ExpireTimer();
    }

    private void StopTimerCoroutine()
    {
        if (_timerCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    private void ExpireTimer()
    {
        _isCounting = false;
        _timeCount = 0;
        TimerReseted?.Invoke();
        TimerExpired?.Invoke();
    }
}
