using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static int Score { get; private set; }

	public void Reset()
	{
		Score = 0;
	}

	/// <summary>
	/// Adds an amount to the current score.
	/// </summary>
	/// <param name="amountToAdd">Amount to add to current score. Use negative value to subtract from score.</param>
	public void AddScore(int amountToAdd)
	{
		Score += amountToAdd;
	}
}