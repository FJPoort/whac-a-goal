using TMPro;
using UnityEngine;

public class ScoreUIView : MonoBehaviour
{
	#region Editor Variables

	[SerializeField]
	private TextMeshProUGUI _scoreText;

	#endregion

	#region Public Methods

	public void UpdateScore(int score)
	{
		_scoreText.text = score.ToString();
	}

	#endregion
}