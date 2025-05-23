using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour, ITimerUI
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _textCount;

    private void Awake()
    {
        _textCount.text = _timer.TimerStartValue.ToString("0.0");
        _timer.TimerValueChanged += OnTimerValueChanged;
    }

    public void OnTimerValueChanged(float count)
    {
        _textCount.text = count.ToString("0.0");
    }

    private void OnDestroy()
    {
        _timer.TimerValueChanged -= OnTimerValueChanged;
    }
}
