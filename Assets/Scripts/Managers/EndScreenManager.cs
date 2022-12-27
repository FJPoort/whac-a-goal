using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
	[Header("UI Fields")]
	[SerializeField]
	private TextMeshProUGUI _scoreField;
	[SerializeField]
	private TMP_InputField _playerNameInputField;

	[Header("Highscore Fields")]
	[SerializeField]
	private HighscoreTable _highscoreTable;
	
	[Space(5f)]
	[SerializeField]
	private Button _submitButton;

	private string _playerName;
	private bool _savedScore = false;

	private void Start()
	{
		_highscoreTable.UpdateHighscoreList();
		
		_playerNameInputField.text = string.Empty;
		_playerNameInputField.interactable = true;
		_submitButton.interactable = false;
		
		_scoreField.text = ScoreManager.Score.ToString();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if(ScoreManager.HasScore && pauseStatus && !_savedScore)
		{
			_highscoreTable.AddEntry(string.IsNullOrWhiteSpace(_playerNameInputField.text) ? "No Name" : _playerNameInputField.text, ScoreManager.Score);
			SceneManager.LoadScene(ScreenNames.StartScreenName);
		}
	}
	
	private void OnApplicationQuit()
	{
		if(ScoreManager.HasScore && !_savedScore)
		{
			_highscoreTable.AddEntry(string.IsNullOrWhiteSpace(_playerNameInputField.text) ? "No Name" : _playerNameInputField.text, ScoreManager.Score);
		}
	}

	public void OnSubmitButtonClicked()
	{
		_submitButton.interactable = false;
		_playerNameInputField.interactable = false;
		_playerName = _playerNameInputField.text;
		
		_highscoreTable.AddEntry(_playerName, ScoreManager.Score);
		_highscoreTable.UpdateHighscoreList();

		_savedScore = true;
	}

	public void OnRetryClicked()
	{
		SceneManager.LoadScene(ScreenNames.GameScreenName);
	}

	public void OnInputChanged()
	{
		_submitButton.interactable = ScoreManager.HasScore && !string.IsNullOrWhiteSpace(_playerNameInputField.text);
	}
}