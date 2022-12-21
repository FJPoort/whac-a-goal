using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestTouch : MonoBehaviour
{
	[SerializeField]
	private InputActionReference _inputReference;
	[SerializeField]
	private Text _textComponent;

	private int _tapCount = 0;
	
	private void OnEnable()
	{
		_inputReference.action.Enable();
	}

	private void OnDisable()
	{
		_inputReference.action.Disable();
	}

	public void OnTap()
	{
		Debug.Log("Whac!");
		_tapCount++;
		_textComponent.text = $"{_tapCount}";
	}
}