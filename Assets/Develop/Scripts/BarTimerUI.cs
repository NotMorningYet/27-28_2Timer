using UnityEngine;
using UnityEngine.UI;

public class BarTimerUI : MonoBehaviour, ITimerUI
{
    [SerializeField] private Image _filledImage;
    [SerializeField] private Timer _timer;

    private float _maxTimer;

    private void Awake()
    {
        _maxTimer = _timer.TimerStartValue;
        _timer.TimerValueChanged += OnTimerValueChanged;        
    }

    public void OnTimerValueChanged(float count)
    {
        _filledImage.fillAmount = count / _maxTimer;
    }

    private void OnDestroy()
    {
        _timer.TimerValueChanged += OnTimerValueChanged;
    }
}
