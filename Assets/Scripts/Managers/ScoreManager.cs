using UnityEngine;

/// <summary>
/// Use to keep track of score
/// </summary>
public class ScoreManager : MonoBehaviour
{
	#region Properties
	
	public static int Score { get; private set; }
	
	public static bool HasScore => Score > 0;
	
	#endregion

	#region public Methods
	
	/// <summary>
	/// Reset score to 0
	/// </summary>
	public void ResetScore()
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
	
	#endregion
}