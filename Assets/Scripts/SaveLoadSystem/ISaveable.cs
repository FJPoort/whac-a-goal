/// <summary>
/// Use on scripts where you want to handle saving to and loading from your save file
/// </summary>
public interface ISaveable
{
	/// <summary>
	/// Loads the saved data that can be passed into <see cref="GameData"/>
	/// </summary>
	/// <param name="data">The saved data</param>
	void LoadData(GameData data);
	
	/// <summary>
	/// Saves your game data specified in<see cref="GameData"/>
	/// </summary>
	/// <param name="data">The data to save. Passed as a ref so all inheritors can write to it</param>
	void SaveData(ref GameData data);
}