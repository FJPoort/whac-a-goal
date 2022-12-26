using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
	[SerializeField]
	private HighscoreTable _highscoreTable;

	private void Start()
	{
		_highscoreTable.UpdateHighscoreList(5);
	}

	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene(ScreenNames.GameScreenName);
	}
}