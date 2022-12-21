using TMPro;
using UnityEngine;

public class TimerUIView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _timerText;

	public void UpdateTimerText(float remainingTime)
	{
		int roundedTime = (int)remainingTime;
		_timerText.text = $"Time left:\n{roundedTime}s";
	}
}