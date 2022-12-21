using System;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
	[SerializeField]
	private Sprite _mole;
	[SerializeField]
	private Sprite _moleHit;
	[SerializeField]
	private SpriteRenderer _spriteRenderer;
	
	// The offset of the sprite to hide it
	private readonly Vector2 _startPosition = new Vector2(0f, -1.80f);
	private readonly Vector2 _endPosition = new Vector2(0f, 0.3f);
	private float _animDurationSecs = 0.5f;
	private float _fullyVisibleSecs = 1f;
	private float _quickAnimDurationSecs = 0.25f;

	private bool _canHit = true;

	private void Start()
	{
		StartCoroutine(ShowHide(_startPosition, _endPosition));
	}

	/// <summary>
	/// The mole's Show/Hide animation routine.
	/// It first lets the mole appear in the amount of seconds determined by <see cref="_animDurationSecs"/>,
	/// then keeps it visible for an other amount of seconds determined by <see cref="_fullyVisibleSecs"/>,
	/// to finally hide the mole again in the amount of seconds determined by <see cref="_animDurationSecs"/>.
	/// </summary>
	/// <param name="start"></param>
	/// <param name="end"></param>
	/// <returns></returns>
	private IEnumerator ShowHide(Vector2 start, Vector2 end)
	{
		// Snap to start position
		transform.localPosition = start;

		// Make mole visible in a timespan of 'animationDurationSecs'
		float elapsed = 0f;
		while(elapsed < _animDurationSecs)
		{
			transform.localPosition = Vector2.Lerp(start, end, elapsed / _animDurationSecs);
			elapsed += Time.deltaTime;
			yield return null;
		}

		// Snap to end position
		transform.localPosition = end;
		
		// Let mole be visible for '_fullyVisibleSecs' to give player a chance to hit
		yield return new WaitForSeconds(_fullyVisibleSecs);
		
		// Then hide mole again
		elapsed = 0f;
		while(elapsed < _animDurationSecs)
		{
			transform.localPosition = Vector2.Lerp(end, start, elapsed / _animDurationSecs);
			elapsed += Time.deltaTime;
			yield return null;
		}
		
		// Snap to start position
		transform.localPosition = start;
	}

	private IEnumerator QuickHide()
	{
		yield return new WaitForSeconds(_quickAnimDurationSecs);
		Hide();
	}

	public void Hide()
	{
		transform.localPosition = _startPosition;
	}

	private void OnMouseDown()
	{
		if(_canHit)
		{
			_spriteRenderer.sprite = _moleHit;
			StopAllCoroutines();
			StartCoroutine(QuickHide());
			_canHit = false;
		}
	}
}