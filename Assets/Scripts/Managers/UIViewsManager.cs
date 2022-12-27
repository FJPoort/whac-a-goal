using UnityEngine;

public class UIViewsManager : MonoBehaviour
{
	#region Editor Variables

	[SerializeField]
	private ScoreUIView _scoreView;
	[SerializeField]
	private TimerUIView _timerView;

	#endregion

	#region Public Methods

	public void UpdateScore(int score)
	{
		_scoreView.UpdateScore(score);
	}

	public void UpdateTimer(float remainingTime)
	{
		_timerView.UpdateTimerText(remainingTime);
	}

	#endregion
}