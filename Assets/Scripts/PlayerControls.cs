using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Checkpoint currentCheckpoint;
    [SerializeField] private float rotationDuration;
    [SerializeField] private float moveSpeed;
    private Transform _player;
    private Button _leftButton;
    private Button _rightButton;
    private Button _forwardButton;
    private Image _leftArrow;
    private Image _rightArrow;
    private Image _forwardArrow;
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
        _leftArrow = canvasComponents.GetLeftArrow;
        _rightArrow = canvasComponents.GetRightArrow;
        _forwardArrow = canvasComponents.GetForwardArrow;
    }

    private void Start()
    {
        DisableButtons();
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
        DisableButtons();
        _chosenDirectionIndex = 0;
        NewCheckpointDirection();
    }
    
    private void OnRightClick()
    {
        if (_nextCheckpoints[1] == null) return; 
        DisableButtons();
        _chosenDirectionIndex = 1;
        NewCheckpointDirection();
    }
    
    private void OnForwardClick()
    {
        if (_nextCheckpoints[2] == null) return; 
        DisableButtons();
        _chosenDirectionIndex = 2;
        NewCheckpointDirection();
    }

    private void NewCheckpointDirection()
    {
        var rotation =
            Quaternion.LookRotation(_nextCheckpoints[_chosenDirectionIndex].GetCheckpointTransform.position - _player.position,
                _player.up);
        _player.DORotateQuaternion(rotation, rotationDuration).OnComplete(MoveTween);
    }
    
    private void MoveTween()
    {
        _player.DOMove(_nextCheckpoints[_chosenDirectionIndex].GetCheckpointTransform.position, moveSpeed).SetSpeedBased().OnComplete(OnMoveComplete);
    }

    private void OnMoveComplete()
    {
        var nextCheckpoint = _nextCheckpoints[_chosenDirectionIndex];
        if (nextCheckpoint.StopCheckpoint)
        {
            if (_nextCheckpoints[_chosenDirectionIndex].DeadEndCheck)
            {
                var rotation =
                    Quaternion.LookRotation(_currentCheckpoint.GetCheckpointTransform.position - _player.position, _player.up);
                _player.DORotateQuaternion(rotation, rotationDuration).OnComplete(CheckpointActivation);
            }
            else
            {
                _player.DORotateQuaternion(nextCheckpoint.transform.localRotation, 2f).OnComplete(CheckpointActivation);
            }
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
        foreach (var direction in _currentCheckpoint.GetAvailableDirection)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    _leftButton.enabled = true;
                    _leftArrow.enabled = true;
                    break;
                case MoveDirection.Right:
                    _rightButton.enabled = true;
                    _rightArrow.enabled = true;
                    break;
                case MoveDirection.Forward:
                    _forwardButton.enabled = true;
                    _forwardArrow.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void DisableButtons()
    {
        _leftButton.enabled = false;
        _rightButton.enabled = false;
        _forwardButton.enabled = false;
        _leftArrow.enabled = false;
        _rightArrow.enabled = false;
        _forwardArrow.enabled = false;
    }

    private void OnDestroy()
    {
        _leftButton.onClick.RemoveListener(OnLeftClick);
        _rightButton.onClick.RemoveListener(OnRightClick);
        _forwardButton.onClick.RemoveListener(OnForwardClick);
    }
}