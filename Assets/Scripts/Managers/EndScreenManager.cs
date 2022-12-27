using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
	[Header("Score UI Fields")]
	[SerializeField]
	private TextMeshProUGUI _scoreField;
	[SerializeField]
	private TMP_InputField _playerNameInputField;
	[SerializeField]
	private Button _submitButton;
	
	[Header("Highscore Fields")]
	[SerializeField]
	private HighscoreTable _highscoreTable;
	
	[Header("RetryQuestionPopup")]
	[SerializeField]
	private GameObject _retryQuestionPopup;

	private string _playerName;
	private bool _savedScore = false;

	private void Start()
	{
		_retryQuestionPopup.SetActive(false);
		
		_highscoreTable.UpdateHighscoreList();
		
		_playerNameInputField.text = string.Empty;
		_playerNameInputField.interactable = true;
		_submitButton.interactable = false;
		
		_scoreField.text = ScoreManager.Score.ToString();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus)
		{
			if(ScoreManager.HasScore && !_savedScore)
			{
				_highscoreTable.AddEntry(string.IsNullOrWhiteSpace(_playerNameInputField.text) ? "No Name" : _playerNameInputField.text, ScoreManager.Score);
			}
			
			SceneManager.LoadScene(ScreenNames.StartScreenName);
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

	public void OnRetryButtonClicked()
	{
		if(!_savedScore && ScoreManager.HasScore)
		{
			_retryQuestionPopup.SetActive(true);
		}
		else
		{
			LoadGameScene();			
		}
	}

	public void OnContinueRetryClicked()
	{
		LoadGameScene();
	}

	public void OnCancelRetryClicked()
	{
		_retryQuestionPopup.SetActive(false);
	}

	public void OnInputChanged()
	{
		_submitButton.interactable = ScoreManager.HasScore && !string.IsNullOrWhiteSpace(_playerNameInputField.text);
	}

	private void LoadGameScene()
	{
		SceneManager.LoadScene(ScreenNames.GameScreenName);
	}
}