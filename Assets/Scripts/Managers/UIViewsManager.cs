using UnityEngine;
using UnityEngine.UI;

public class UIViewsManager : MonoBehaviour
{
	[SerializeField]
	private ScoreUIView _scoreView;

	[SerializeField]
	private TimerUIView _timerView;

	public void UpdateScore(int score)
	{
		_scoreView.UpdateScore(score);
	}

	public void UpdateTimer(float remainingTime)
	{
		_timerView.UpdateTimerText(remainingTime);
	}
}