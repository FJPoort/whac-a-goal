using TMPro;
using UnityEngine;

public class HighScoreEntryView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _positionText;
	[SerializeField]
	private TextMeshProUGUI _nameText;
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	public void Initialize(int index, string playerName, int score)
	{
		_positionText.text = index.ToString();
		_nameText.text = playerName;
		_scoreText.text = score.ToString();
	}
}