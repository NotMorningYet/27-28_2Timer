using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textCount;
    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _textCount.text = _timer.TimerStartValue.ToString("0.0");
        _timer.TimeCount.Changed += OnTimerValueChanged;
    }

    public void OnTimerValueChanged(float count)
    {
        _textCount.text = count.ToString("0.0");
    }

    private void OnDestroy()
    {
        _timer.TimeCount.Changed -= OnTimerValueChanged;
    }
}
