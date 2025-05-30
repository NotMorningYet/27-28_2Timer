using System;
using System.Collections;
using UnityEngine;

public class Timer
{

    public event Action TimerStarted;
    public event Action TimerReseted;
    public event Action TimerPaused;
    public event Action TimerExpired;
    
    private ReactiveVariable<float> _timerStartValue;
    private ReactiveVariable<float> _timeCount;
    private bool _isCounting = false;
    private MonoBehaviour _coroutineRunner;
    private Coroutine _timerCoroutine;

    public Timer(ReactiveVariable<float> timerStartValue, MonoBehaviour coroutineRunner)
    {
        _timerStartValue = timerStartValue;
        _coroutineRunner = coroutineRunner;
        _timeCount = _timerStartValue;
    }

    public ReactiveVariable<float> TimeCount => _timeCount;
    public float TimerStartValue => _timerStartValue.Value;
    public bool IsCounting => _isCounting;  

    public void StartTimer()
    {
        if (!_isCounting)
        {
            _isCounting = true;
            _timerCoroutine = _coroutineRunner.StartCoroutine(TimerProcess());            
        }
    }

    public void ResetTimer()
    {
        StopTimerCoroutine();
        _isCounting = false;
        _timeCount = _timerStartValue;
        TimerReseted?.Invoke();        
    }

    public void PauseTimer()
    {
        if (_isCounting)
        {
            _isCounting = false;
            TimerPaused?.Invoke();
        }
    }

    private IEnumerator TimerProcess()
    {
        while (_timeCount.Value > 0)
        {
            _timeCount.Value -= Time.deltaTime;            
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
        _timeCount.Value = 0;
        TimerReseted?.Invoke();
        TimerExpired?.Invoke();
    }
}
