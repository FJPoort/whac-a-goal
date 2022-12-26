using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SaveLoadManager : MonoBehaviour
{
	private GameData _gameData;
	private FileDataHandler _fileDataHandler;
	
	public static SaveLoadManager instance { get; private set; }

	private void Awake()
	{
		if(instance != null)
		{
			Debug.LogError($"Found another {nameof(SaveLoadManager)} in the scene while trying to create it.");
			return;
		}

		instance = this;
	}

	private void Start()
	{
		_fileDataHandler = new FileDataHandler();
		LoadGame();
	}

	public void NewGame()
	{
		_gameData = new GameData();
	}

	public void LoadGame()
	{
		// Load saved data from a file using the data handler
		_gameData = _fileDataHandler.Load();
		
		if(_gameData == null)
		{
			NewGame();
		}

		// Forward the loaded data to all other scripts that need it
		foreach(ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
		{
			saveable.LoadData(_gameData);
		}
	}

	public void SaveGame()
	{
		// Pass the data to other scripts so they can update it
		foreach(ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
		{
			saveable.SaveData(ref _gameData);
		}
		
		// Save the data to a file using the data handler
		_fileDataHandler.Save(_gameData);
	}
}