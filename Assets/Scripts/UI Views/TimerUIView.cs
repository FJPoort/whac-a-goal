using TMPro;
using UnityEngine;

public class TimerUIView : MonoBehaviour
{
	#region Editor variables

	[SerializeField]
	private TextMeshProUGUI _timerText;

	#endregion

	#region Public Methods
	
	public void UpdateTimerText(float remainingTime)
	{
		int roundedTime = (int)remainingTime;
		_timerText.text = $"Time left:\n{roundedTime}s";
	}
	
	#endregion
}