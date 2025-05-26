using System.Collections.Generic;
using UnityEngine;

public class HeartTimerView : MonoBehaviour
{
    [SerializeField] private Heart _heartPrefab;
    private Timer _timer;

    private int _maxNumberOfHearts;
    private int _numberOfHearts;

    private List<Heart> _heartList = new List<Heart>();
    
    public void Initialize(Timer timer)
    {
        _timer = timer;
        _maxNumberOfHearts = (int)_timer.TimerStartValue;
        _numberOfHearts = _maxNumberOfHearts;
        _timer.TimerValueChanged += OnTimerValueChanged;

        FillList();
    }

    public void OnTimerValueChanged(float count)
    {
        int intCount = (int)count;

        if (_numberOfHearts == intCount)
            return;

        _numberOfHearts = intCount;

        for (int i = 0; i < intCount; i++)
        {
            _heartList[i].gameObject.SetActive(true);
        }
        
        for (int i = intCount; i < _maxNumberOfHearts; i++)
        {
            _heartList[i].gameObject.SetActive(false);
        }
    }

    private void FillList()
    {
        for (int i = 0;  i < _maxNumberOfHearts; i++) 
        {
            Heart heart = Instantiate(_heartPrefab, transform);
            _heartList.Add(heart);
        }
    }

    private void OnDestroy()
    {
        _timer.TimerValueChanged -= OnTimerValueChanged;
    }
}
