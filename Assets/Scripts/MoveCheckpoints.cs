using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoveCheckpoints : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    private Transform _player;
    private Button _forward;

    [Inject]
    private void Construct(PlayerComponents playerComponents, CanvasComponents canvasComponents)
    {
        _player = playerComponents.GetPlayerTransform;
        _forward = canvasComponents.GetForwardButton;
    }

    private void Start()
    {
        _forward.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var positions = new Vector3[checkpoints.Length];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            positions[i] = checkpoints[i].position;
        }

        _player.DOPath(positions, 20f, PathType.CatmullRom);
    }
}