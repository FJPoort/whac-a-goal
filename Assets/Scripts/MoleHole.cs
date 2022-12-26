using UnityEngine;

public class MoleHole : MonoBehaviour
{
	[SerializeField]
	private Mole _molePrefab;

	public Mole Mole { get; private set; }

	public void CreateMole(int identifier)
	{
		Mole = Instantiate(_molePrefab, transform);
		Mole.Initialize(identifier);
	}

	public void RemoveMole()
	{
		DestroyMole();
	}

	private void DestroyMole()
	{
		if(Mole == null)
		{
			return;
		}
		
		Mole.Deactivate();
		Destroy(Mole.gameObject);
		Mole = null;
	}
}