using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _startPauseButton;
    [SerializeField] private Button _resetButton;

    private Timer _timer;
    private TMP_Text _startPauseButtonText;
    private TMP_Text _resetButtonText;

    private readonly string _startText = "Start";
    private readonly string _pauseText = "Pause";
    private readonly string _resetText = "Reset";

    public void Initialize(Timer timer)
    {
        _timer = timer;
        ButtonTextSetup();

        _timer.TimerExpired += OnTimerExpired;
    }

    public void OnStartPauseButtonClick()
    {
        if (_timer.IsCounting)
        {
            _timer.PauseTimer();
            _startPauseButtonText.text = _startText;
        }
        else
        {
            _startPauseButtonText.text = _pauseText;
            _timer.StartTimer();
        }
    }

    public void OnResetButtonClick()
    {
        _timer.ResetTimer();
        _startPauseButtonText.text = _startText;
        _startPauseButton.interactable = true;
    }

    private void ButtonTextSetup()
    {
        _startPauseButtonText = _startPauseButton.GetComponentInChildren<TMP_Text>();
        _resetButtonText = _resetButton.GetComponentInChildren<TMP_Text>();

        _startPauseButtonText.text = _startText;
        _resetButtonText.text = _resetText;
    }
    private void OnTimerExpired()
    {
        _startPauseButtonText.text = _startText;
        _startPauseButton.interactable = false;
    }

    private void OnDestroy()
    {
        _timer.TimerExpired -= OnTimerExpired;
    }
}
