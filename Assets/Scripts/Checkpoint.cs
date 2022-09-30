using UnityEngine;

public class Checkpoint : MonoBehaviour
{
        [SerializeField] private MoveDirection[] moveDirections;
        [SerializeField] private Checkpoint[] nextPoints;
        [SerializeField] private Transform pointTransform;
        [SerializeField] private bool deadEnd;
        [SerializeField] private bool stopCheckpoint;

        public MoveDirection[] GetAvailableDirection => moveDirections;
        public Checkpoint[] GetNextCheckpoints => nextPoints;
        public Transform GetCheckpointTransform => pointTransform;
        public bool DeadEndCheck => deadEnd;
        public bool StopCheckpoint => stopCheckpoint;
}