using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[Header("Mole properties")]
	[SerializeField]
	private MoleHole _moleHolePrefab;
	[SerializeField]
	private List<Transform> _possibleMolePositions;
	
	[Header("Gameplay values")]
	[SerializeField, Tooltip("The number of hits needed to activate an extra mole.")]
	private int _activateExtraMoleThreshold = 10;
	[SerializeField]
	private int _defaultScoreValue = 1;
	[SerializeField]
	private int _timePenaltyMissedMole = 2;
	[SerializeField]
	private int _molePositionsAmount = 10;
	[SerializeField]
	private float _playTimeSecs = 30f;

	[Header("Other References")]
	[SerializeField]
	private UIViewsManager _uiManager;
	[SerializeField]
	private ScoreManager _scoreManager;

	private float _timeRemainingSecs;
	private bool _playing = false;

	private Transform[] _selectedMolePositions;
	private List<Transform> _activeMolePositions = new List<Transform>();
	
	private List<MoleHole> _moleHoles = new List<MoleHole>();
	private HashSet<Mole> _activeMoles = new HashSet<Mole>();

	private void Start()
	{
		// If this threshold is set to 0 it will result in an error 
		if(_activateExtraMoleThreshold <= 0)
		{
			Debug.LogWarning($"You have set the {nameof(_activateExtraMoleThreshold)} to '0' or less, resulting in it automatically being set to 1.");
			_activateExtraMoleThreshold = 1;
		}

		// Can't have more moles than spawnpoints
		if(_molePositionsAmount > _possibleMolePositions.Count)
		{
			Debug.LogWarning($"You have exceeded the maximum amount of {nameof(_molePositionsAmount)}, resulting in it automatically being set to the max amount of {_possibleMolePositions.Count}");
			_molePositionsAmount = _possibleMolePositions.Count;
		}
		
		// Copy the possible positions list to use the copy for creating a list of selected positions
		List<Transform> copyOfPossiblePositions = _possibleMolePositions;
		
		// Randomly select positions for the moles to appear
		//_selectedMolePositions = new Transform[_molePositionsAmount];
		for(int i = 0; i < _molePositionsAmount; i++)
		{
			int index = Random.Range(0, copyOfPossiblePositions.Count);

			Transform curPos = copyOfPossiblePositions[index];
			MoleHole hole = Instantiate(_moleHolePrefab, curPos);
			_moleHoles.Add(hole);
			
			copyOfPossiblePositions.RemoveAt(index);
		}
		
		// Reset the game's state
		_activeMolePositions.Clear();
		_timeRemainingSecs = _playTimeSecs;
		_scoreManager.Reset();
		_uiManager.UpdateScore(ScoreManager.Score);
		_uiManager.UpdateTimer(_timeRemainingSecs);
		
		_playing = true;
	}

	private void Update()
	{
		if(!_playing)
		{
			return;
		}

		_timeRemainingSecs -= Time.deltaTime;
		if(_timeRemainingSecs < 0)
		{
			_timeRemainingSecs = 0;
			TimerEnded();
			
			// It is pointless to continue trying to activate new moles, since we have stopped playing
			return;
		}
		_uiManager.UpdateTimer(_timeRemainingSecs);

		// Activate more moles every X hits
		if(_activeMoles.Count <= ScoreManager.Score / _activateExtraMoleThreshold)
		{
			int index = Random.Range(0, _moleHoles.Count);
			// For now doesn't matter if that mole is already active.
			// In that case, a new position will be tried in the next frame
			if(!_activeMoles.Contains(_moleHoles[index].Mole))
			{
				MoleHole curHole = _moleHoles[index];
				curHole.CreateMole(index);
				Mole mole = curHole.Mole;
				mole.Activate();
				mole.MoleHitEvent += OnMoleHit;
				mole.MoleMisEvent += OnMoleMis;

				_activeMoles.Add(mole);
			}
		}
	}

	private void TimerEnded()
	{
		for(int i = 0; i < _moleHoles.Count; i++)
		{
			_moleHoles[i].RemoveMole();
		}
		
		_playing = false;
		SceneManager.LoadScene(ScreenNames.EndScreenName);
	}

	private void OnMoleHit(int identifier)
	{
		_moleHoles[identifier].Mole.MoleHitEvent -= OnMoleHit;
		
		_scoreManager.AddScore(_defaultScoreValue);
		_uiManager.UpdateScore(ScoreManager.Score);
        
		_activeMoles.Remove(_moleHoles[identifier].Mole);
		_moleHoles[identifier].RemoveMole();
	}
	
	private void OnMoleMis(int identifier)
	{
		_moleHoles[identifier].Mole.MoleMisEvent -= OnMoleMis;
		
		_timeRemainingSecs -= _timePenaltyMissedMole;
		
		_activeMoles.Remove(_moleHoles[identifier].Mole);
		_moleHoles[identifier].RemoveMole();
		// For later: reset streaks
	}
}