using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
	public List<HighscoreTable.HighscoreData> Highscores;

	public GameData()
	{
		Highscores = new List<HighscoreTable.HighscoreData>();
	}
}