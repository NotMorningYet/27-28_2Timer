using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timerStartValue;
    [SerializeField] private CounterView _counterView;
    [SerializeField] private HeartTimerView _heartTimerView;
    [SerializeField] private BarTimerView _barTimerView;
    [SerializeField] private ButtonHandler _buttonHandler;

    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_timerStartValue, this);
        _counterView.Initialize(_timer);
        _barTimerView.Initialize(_timer);
        _heartTimerView.Initialize(_timer);
        _buttonHandler.Initialize(_timer);
    }
}
