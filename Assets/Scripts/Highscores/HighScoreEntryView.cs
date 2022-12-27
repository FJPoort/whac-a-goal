using TMPro;
using UnityEngine;

public class HighScoreEntryView : MonoBehaviour
{
	#region Editor Variables
	
	[SerializeField]
	private TextMeshProUGUI _positionText;
	[SerializeField]
	private TextMeshProUGUI _nameText;
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	#endregion

	#region Public Methods

	public void Initialize(int index, string playerName, int score)
	{
		_positionText.text = index.ToString();
		_nameText.text = playerName;
		_scoreText.text = score.ToString();
	}

	#endregion
}