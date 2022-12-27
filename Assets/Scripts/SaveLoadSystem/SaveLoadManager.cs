using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SaveLoadManager : MonoBehaviour
{
	#region Variables

	private GameData _gameData;
	private FileDataHandler _fileDataHandler;

	#endregion
	
	#region Properties
	
	public static SaveLoadManager instance { get; private set; }
	
	#endregion

	#region Unity Event Functions

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

	#endregion

	#region Public Methods

	/// <summary>
	/// Creates a clean storage object of <see cref="GameData"/>
	/// </summary>
	public void NewGame()
	{
		_gameData = new GameData();
	}

	/// <summary>
	/// Goes through all objects that inherit <see cref="ISaveable"/> and calls <see cref="ISaveable.LoadData"/>
	/// </summary>
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

	/// <summary>
	/// Goes through all objects that inherit <see cref="ISaveable"/> and calls <see cref="ISaveable.SaveData"/>
	/// </summary>
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

	#endregion
}