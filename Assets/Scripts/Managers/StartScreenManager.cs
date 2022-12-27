using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
	#region Editor Variables
	
	[SerializeField]
	private HighscoreTable _highscoreTable;
	
	#endregion

	#region Unity Event Functions
	
	private void Start()
	{
		_highscoreTable.UpdateHighscoreList(5);
	}

	#endregion
	
	#region Public Methods
	
	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene(ScreenNames.GameScreenName);
	}
	
	#endregion
}