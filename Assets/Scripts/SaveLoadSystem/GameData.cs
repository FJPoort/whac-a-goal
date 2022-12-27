using System;
using System.Collections.Generic;

/// <summary>
/// A container for all game data that needs to be saved
/// </summary>
[Serializable]
public class GameData
{
	#region Properties
	
	public List<HighscoreTable.HighscoreData> Highscores;

	#endregion

	public GameData()
	{
		Highscores = new List<HighscoreTable.HighscoreData>();
	}
}