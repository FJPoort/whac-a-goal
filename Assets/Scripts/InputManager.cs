using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private TouchControls _touchControls;

	private Vector2 _touchPos;

	private void Awake()
	{
		_touchControls = new TouchControls();
	}

	private void OnEnable()
	{
		_touchControls.Enable();
	}

	private void OnDisable()
	{
		_touchControls.Disable();
	}

	private void Update()
	{
		HandleTouch();
	}

	private void HandleTouch()
	{
	}
	
	public void OnTouchPerformed(InputAction.CallbackContext context)
	{
		
	}
}