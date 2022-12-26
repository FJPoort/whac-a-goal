using System;
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

	[Header("Highscore Fiels")]
	[SerializeField]
	private HighscoreTable _highscoreTable;
	
	[Space(5f)]
	[SerializeField]
	private Button _submitButton;

	private string _playerName;

	private void Start()
	{
		_highscoreTable.UpdateHighscoreList();
		
		_playerNameInputField.text = string.Empty;
		_playerNameInputField.interactable = true;
		_submitButton.interactable = true;
		
		_scoreField.text = ScoreManager.Score.ToString();
	}

	public void OnSubmitButtonClicked()
	{
		_submitButton.interactable = false;
		_playerNameInputField.interactable = false;
		_playerName = _playerNameInputField.text;
		
		_highscoreTable.AddEntry(_playerName, ScoreManager.Score);
	}

	public void OnRetryClicked()
	{
		SceneManager.LoadScene(ScreenNames.GameScreenName);
	}
}