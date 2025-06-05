using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action TimerExpired;

    private readonly float _timerStartValue;
    private ReactiveVariable<float> _timeCount;
    private bool _isCounting = false;
    private readonly MonoBehaviour _coroutineRunner;
    private Coroutine _timerCoroutine;

    public Timer(float timerStartValue, MonoBehaviour coroutineRunner)
    {
        _timerStartValue = timerStartValue;
        _coroutineRunner = coroutineRunner;
        _timeCount = new ReactiveVariable<float>(_timerStartValue);
    }

    public IReadOnlyReactiveVariable<float> TimeCount => _timeCount;
    public float TimerStartValue => _timerStartValue;
    public bool IsCounting => _isCounting;

    public void StartTimer()
    {
        if (_isCounting)
            return;

        _isCounting = true;
        
        if (_timerCoroutine == null)
            _timerCoroutine = _coroutineRunner.StartCoroutine(TimerProcess());
    }

    public void ResetTimer()
    {
        StopTimerCoroutine();
        _isCounting = false;
        _timeCount.Value = _timerStartValue;
    }

    public void PauseTimer()
    {
        _isCounting = false;
    }

    private IEnumerator TimerProcess()
    {
        while (_timeCount.Value > 0)
        {
            if (_isCounting)
            {
                _timeCount.Value -= Time.deltaTime;
            }

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
        TimerExpired?.Invoke();
    }
}
