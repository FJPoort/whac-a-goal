using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
	private readonly string _saveFilePath;

	public FileDataHandler()
	{
		_saveFilePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
	}

	public GameData Load()
	{
		GameData result = null;
		if(File.Exists(_saveFilePath))
		{
			try
			{

				// Load json data into a string
				string data = string.Empty;
				using(FileStream stream = new FileStream(_saveFilePath, FileMode.Open))
				{
					using(StreamReader reader = new StreamReader(stream))
					{
						data = reader.ReadToEnd();
					}
				}

				// Deserialize json string to game data object
				result = JsonUtility.FromJson<GameData>(data);
			}
			catch(Exception e)
			{
				Debug.LogError($"Loading file went wrong for: {_saveFilePath}. Error:\n{e.Message}\nStacktrace: {e.StackTrace}");
			}
		}

		return result;
	}

	public void Save(GameData data)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(_saveFilePath));
			
			// Serialize game data object into Json
			string dataJson = JsonUtility.ToJson(data, true);

			// Write the serialized data to file
			using(FileStream stream = new FileStream(_saveFilePath, FileMode.Create))
			{
				using(StreamWriter writer = new StreamWriter(stream))
				{
					writer.Write(dataJson);
				}
			}
		}
		catch(Exception e)
		{
			Debug.LogError($"Saving file went wrong for: {_saveFilePath}. Error:\n{e.Message}\nStacktrace: {e.StackTrace}");
		}
	}
}