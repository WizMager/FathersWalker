using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Checkpoint currentCheckpoint;
    private Transform _player;
    private Button _leftButton;
    private Button _rightButton;
    private Button _forwardButton;
    private Checkpoint _currentCheckpoint;
    private Checkpoint[] _nextCheckpoints;
    private int _chosenDirectionIndex;

    [Inject]
    private void Construct(PlayerComponents playerComponents, CanvasComponents canvasComponents)
    {
        _player = playerComponents.GetPlayerTransform;
        _leftButton = canvasComponents.GetLeftButton;
        _rightButton = canvasComponents.GetRightButton;
        _forwardButton = canvasComponents.GetForwardButton;
    }

    private void Start()
    {
        _currentCheckpoint = currentCheckpoint;
        _nextCheckpoints = _currentCheckpoint.GetNextCheckpoints;
        _leftButton.onClick.AddListener(OnLeftClick);
        _rightButton.onClick.AddListener(OnRightClick);
        _forwardButton.onClick.AddListener(OnForwardClick);
        ButtonActivation();
    }

    private void OnLeftClick()
    {
        if (_nextCheckpoints[0] == null) return;
        _chosenDirectionIndex = 0;
        MoveTween();
        CheckpointActivation();
    }
    
    private void OnRightClick()
    {
        if (_nextCheckpoints[1] == null) return; 
        _chosenDirectionIndex = 1;
        MoveTween();
        CheckpointActivation();
    }
    
    private void OnForwardClick()
    {
        if (_nextCheckpoints[2] == null) return; 
        _chosenDirectionIndex = 2;
        MoveTween();
        CheckpointActivation();
    }

    private void MoveTween()
    {
        _player.DOMove(_nextCheckpoints[_chosenDirectionIndex].transform.position, 5f).SetSpeedBased().OnComplete(OnMoveComplete);
    }

    private void OnMoveComplete()
    {
        if (_currentCheckpoint.DeadEndCheck)
        {
            _player.DORotate( _player.rotation.eulerAngles + new Vector3(0, 180f, 0), 2f);
        }
        
    }
    
    private void CheckpointActivation()
    {
        _currentCheckpoint = _nextCheckpoints[_chosenDirectionIndex];
        _nextCheckpoints = _currentCheckpoint.GetNextCheckpoints;
        ButtonActivation();
    }
    
    private void ButtonActivation()
    {
        DisableButtons();
        foreach (var direction in _currentCheckpoint.GetAvailableDirection)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    _leftButton.gameObject.SetActive(true);
                    break;
                case MoveDirection.Right:
                    _rightButton.gameObject.SetActive(true);
                    break;
                case MoveDirection.Forward:
                    _forwardButton.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void DisableButtons()
    {
        _leftButton.gameObject.SetActive(false);
        _rightButton.gameObject.SetActive(false);
        _forwardButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _leftButton.onClick.RemoveListener(OnLeftClick);
        _rightButton.onClick.RemoveListener(OnRightClick);
        _forwardButton.onClick.RemoveListener(OnForwardClick);
    }
}