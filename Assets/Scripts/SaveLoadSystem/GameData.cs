using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
	public List<EndScreenManager.HighscoreData> Highscores;

	public GameData()
	{
		Highscores = new List<EndScreenManager.HighscoreData>();
	}
}