using UnityEngine;
using UnityEngine.UI;

public class BarTimerView : MonoBehaviour
{
    [SerializeField] private Image _filledImage;
    private Timer _timer;

    private float _maxTimer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
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
