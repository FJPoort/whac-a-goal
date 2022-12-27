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
	[SerializeField]
	private BoxCollider2D _boxCollider;

	// The offset of the sprite to hide it
	private readonly Vector2 _startPosition = new Vector2(0f, -1.80f);
	private readonly Vector2 _endPosition = new Vector2(0f, 0.3f);
	private float _animDurationSecs = 0.5f;
	private float _fullyVisibleSecs = 1.3f;
	private float _waitBeforeQuickHideSecs = 0.25f;
	
	// Collider vars
	private Vector2 _boxOffset;
	private Vector2 _boxSize;
	private Vector2 _boxOffsetHidden;
	private Vector2 _boxSizeHidden;

	private bool _canHit = true;

	private int _identifier;

	public delegate void MoleHitEventHandler(int identifier);
	public event MoleHitEventHandler MoleHitEvent;
	private void FireMoleHitEvent(int id) => MoleHitEvent?.Invoke(id);
	
	public delegate void MoleMisEventHandler(int identifier);
	public event MoleMisEventHandler MoleMisEvent;
	private void FireMoleMisEvent(int id) => MoleMisEvent?.Invoke(id);

	private void Awake()
	{
		_boxOffset = _boxCollider.offset;
		_boxSize = _boxCollider.size;
		_boxOffsetHidden = new Vector2(_boxOffset.x, -_startPosition.y / 2f);
		_boxSizeHidden = new Vector2(_boxSize.x, 0f);
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
	private IEnumerator ShowHide()
	{
		// Snap to start position
		transform.localPosition = _startPosition;
		_boxCollider.offset = _boxOffsetHidden;
		_boxCollider.size = _boxSizeHidden;
		
		// Make mole visible in a timespan of 'animationDurationSecs'
		float elapsed = 0f;
		while(elapsed < _animDurationSecs)
		{
			transform.localPosition = Vector2.Lerp(_startPosition, _endPosition, elapsed / _animDurationSecs);
			_boxCollider.offset = Vector2.Lerp(_boxOffsetHidden, _boxOffset, elapsed / _animDurationSecs);
			_boxCollider.size = Vector2.Lerp(_boxSizeHidden, _boxSize, elapsed / _animDurationSecs);
			elapsed += Time.deltaTime;
			yield return null;
		}

		// Snap to end position
		transform.localPosition = _endPosition;
		_boxCollider.offset = _boxOffset;
		_boxCollider.size = _boxSize;

		// Let mole be visible for '_fullyVisibleSecs' to give player a chance to hit
		yield return new WaitForSeconds(_fullyVisibleSecs);
		
		// Then hide mole again
		elapsed = 0f;
		while(elapsed < _animDurationSecs)
		{
			transform.localPosition = Vector2.Lerp(_endPosition, _startPosition, elapsed / _animDurationSecs);
			_boxCollider.offset = Vector2.Lerp(_boxOffset, _boxOffsetHidden, elapsed / _animDurationSecs);
			_boxCollider.size = Vector2.Lerp(_boxSize, _boxSizeHidden, elapsed / _animDurationSecs);
			elapsed += Time.deltaTime;
			yield return null;
		}
		
		// Snap to start position
		transform.localPosition = _startPosition;
		_boxCollider.offset = _boxOffsetHidden;
		_boxCollider.size = _boxSizeHidden;
		
		// If we are back at the startPosition, but the mole is still hittable; this means we missed it.
		if(_canHit)
		{
			_canHit = false;
			FireMoleMisEvent(_identifier);
		}
	}

	/// <summary>
	/// Let's the mole hide instantly after an amount of time determined by <see cref="_waitBeforeQuickHideSecs"/>
	/// </summary>
	/// <returns></returns>
	private IEnumerator QuickHide()
	{
		yield return new WaitForSeconds(_waitBeforeQuickHideSecs);
		Destroy(gameObject);
	}

	public void Initialize(int identifier)
	{
		_identifier = identifier;
		
		transform.localPosition = _startPosition;
		_boxCollider.offset = _boxOffsetHidden;
		_boxCollider.size = _boxSizeHidden;
	}
	
	/// <summary>
	/// Call this function to start mole's show/hide behaviour
	/// </summary>
	public void Activate()
	{
		_canHit = true;
		_spriteRenderer.sprite = _mole;
		StartCoroutine(ShowHide());
	}

	public void Deactivate()
	{
		_canHit = false;
		StopAllCoroutines();
	}

	private void OnMouseDown()
	{
		if(_canHit)
		{
			_canHit = false;

			_spriteRenderer.sprite = _moleHit;
			StopAllCoroutines();
			StartCoroutine(QuickHide());
			
			FireMoleHitEvent(_identifier);
		}
	}
}