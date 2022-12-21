using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _menuContainer;
	[SerializeField]
	private GameObject _gameContainer;
	
	public void OnStartButtonClicked()
	{
		_menuContainer.SetActive(false);
		_gameContainer.SetActive(true);
	}
}