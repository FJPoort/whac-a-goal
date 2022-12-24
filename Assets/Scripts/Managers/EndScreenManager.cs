using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour, ISaveable
{
	[Header("UI Fields")]
	[SerializeField]
	private TextMeshProUGUI _scoreField;
	[SerializeField]
	private TMP_InputField _playerNameInputField;

	[Header("Highscore Fiels")]
	[SerializeField]
	private Transform _highScoreEntryContainer;
	[SerializeField]
	private HighScoreEntryView _highScoreEntryPrefab;
	
	[Space(5f)]
	[SerializeField]
	private Button _submitButton;

	private string _playerName;
	private int _score;

	private List<HighscoreData> _highscoreList = new List<HighscoreData>();
	private List<HighScoreEntryView> _visibleEntries = new List<HighScoreEntryView>();
	
	public void Initialize(int score)
	{
		_playerNameInputField.text = string.Empty;
		_playerNameInputField.interactable = true;
		_submitButton.interactable = true;
		
		_score = score;
		_scoreField.text = score.ToString();
		
		UpdateHighscoreList();
	}

	private void UpdateHighscoreList()
	{
		_visibleEntries.ForEach(x => Destroy(x.gameObject));
		_visibleEntries.Clear();
		_highscoreList.Sort((a,b) => b.Score.CompareTo(a.Score));
		
		for(int i = 0, c = _highscoreList.Count; i < c; i++)
		{
			HighscoreData entry = _highscoreList[i];
			HighScoreEntryView newEntryView = Instantiate(_highScoreEntryPrefab, _highScoreEntryContainer);
			newEntryView.Initialize(i+1, entry.PlayerName, entry.Score);
			
			_visibleEntries.Add(newEntryView);
		}
	}

	public void OnSubmitButtonClicked()
	{
		_submitButton.interactable = false;
		_playerNameInputField.interactable = false;
		_playerName = _playerNameInputField.text;
		
		_highscoreList.Add(new HighscoreData
		{
			PlayerName = _playerName,
			Score = _score
		});
		
		UpdateHighscoreList();
		
		SaveLoadManager.instance.SaveGame();
	}
	
	public void LoadData(GameData data)
	{
		_highscoreList = data.Highscores;
	}

	public void SaveData(ref GameData data)
	{
		data.Highscores = _highscoreList;
	}

	[Serializable]
	public struct HighscoreData
	{
		public string PlayerName;
		public int Score;
	}
}