using UnityEngine;

public class MoleHole : MonoBehaviour
{
	#region Editor Variables
	
	[SerializeField]
	private Mole _molePrefab;
	
	#endregion

	#region Properties

	public Mole Mole { get; private set; }

	#endregion

	#region Public Methods

	public void CreateMole(int identifier)
	{
		Mole = Instantiate(_molePrefab, transform);
		Mole.Initialize(identifier);
	}

	public void HandleHitMole()
	{
		// Mole's gameobject is being destroyed after hit in Mole.cs
		// so we only need to null the reference to it here
		Mole = null;
	}

	public void HandleMissedMole()
	{
		if(Mole == null)
		{
			return;
		}
		
		Mole.Deactivate();
		Destroy(Mole.gameObject);
		Mole = null;
	}

	#endregion
}