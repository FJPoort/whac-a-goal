using System;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTable : MonoBehaviour, ISaveable
{
	#region Editor Variables
	
	[Header("Highscore Fields")]
	[SerializeField]
	private HighScoreEntryView _highScoreEntryPrefab;
	[SerializeField]
	private Transform _entriesContainer;
	
	#endregion
	
	#region Variables

	private List<HighscoreData> _highscoreList = new List<HighscoreData>();
	private readonly List<HighScoreEntryView> _visibleEntries = new List<HighScoreEntryView>();
	
	#endregion
	
	#region Public Methods

	/// <summary>
	/// Add en entry to the highscore list
	/// </summary>
	/// <param name="playerName">The name to be displayed</param>
	/// <param name="score">The score to be displayed</param>
	public void AddEntry(string playerName, int score)
	{
		_highscoreList.Add(new HighscoreData
		{
			PlayerName = playerName,
			Score = score
		});
		
		SaveLoadManager.instance.SaveGame();
	}

	/// <summary>
	/// Updates the highscore table.
	/// </summary>
	/// <param name="maxEntries">The amount of entries to show in the table. Default value of '0' shows all existing entries.</param>
	public void UpdateHighscoreList(int maxEntries = 0)
	{
		_visibleEntries.ForEach(x => Destroy(x.gameObject));
		_visibleEntries.Clear();
		_highscoreList.Sort((a,b) => b.Score.CompareTo(a.Score));

		for(int i = 0, c = _highscoreList.Count; i < c; i++)
		{
			HighscoreData entry = _highscoreList[i];
			HighScoreEntryView newEntryView = Instantiate(_highScoreEntryPrefab, _entriesContainer);
			newEntryView.Initialize(i+1, entry.PlayerName, entry.Score);
			
			_visibleEntries.Add(newEntryView);

			if(maxEntries > 0 && i == maxEntries - 1)
			{
				break;
			}
		}
	}
	
	#endregion

	#region ISaveable Implementation

	public void LoadData(GameData data)
	{
		_highscoreList = data.Highscores;
	}

	public void SaveData(ref GameData data)
	{
		data.Highscores = _highscoreList;
	}

	#endregion
	
	#region Inner Structs

	[Serializable]
	public struct HighscoreData
	{
		public string PlayerName;
		public int Score;
	}
	
	#endregion
}