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
    private Checkpoint _previousCheckpoint;
    private Checkpoint _chosenNextPoint;

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
        _chosenNextPoint = _nextCheckpoints[0];
        NewCheckpointDirection();
    }
    
    private void OnRightClick()
    {
        if (_nextCheckpoints[1] == null) return; 
        DisableButtons();
        _chosenNextPoint = _nextCheckpoints[1];
        NewCheckpointDirection();
    }
    
    private void OnForwardClick()
    {
        if (_nextCheckpoints[2] == null) return; 
        DisableButtons();
        _chosenNextPoint = _nextCheckpoints[2];
        NewCheckpointDirection();
    }

    private void NewCheckpointDirection()
    {
        var rotation =
            Quaternion.LookRotation(_chosenNextPoint.GetCheckpointTransform.position - _player.position,
                _player.up);
        _player.DORotateQuaternion(rotation, rotationDuration).OnComplete(MoveTween);
    }
    
    private void MoveTween()
    {
        _player.DOMove(_chosenNextPoint.GetCheckpointTransform.position, moveSpeed).SetSpeedBased().OnComplete(OnMoveComplete);
    }

    private void OnMoveComplete()
    {
        if (_chosenNextPoint.StopCheckpoint)
        {
            if (_chosenNextPoint.DeadEndCheck)
            {
                var rotation =
                    Quaternion.LookRotation(_currentCheckpoint.GetCheckpointTransform.position - _player.position, _player.up);
                _player.DORotateQuaternion(rotation, rotationDuration).OnComplete(CheckpointActivation);
            }
            else
            {
                _player.DORotateQuaternion(_chosenNextPoint.transform.localRotation, 2f).OnComplete(CheckpointActivation);
            }
        }
        else
        {
            CheckpointActivation();
            foreach (var checkpoint in _nextCheckpoints)
            {
                if (_previousCheckpoint == checkpoint) continue;
                _chosenNextPoint = checkpoint;
                break;
            }
            NewCheckpointDirection();
        }
    }
    
    private void CheckpointActivation()
    {
        _previousCheckpoint = _currentCheckpoint;
        _currentCheckpoint = _chosenNextPoint;
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