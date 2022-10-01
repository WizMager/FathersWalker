using UnityEngine;

public class Checkpoint : MonoBehaviour
{
        [SerializeField] private MoveDirection[] moveDirections;
        [SerializeField] private Checkpoint[] nextCheckpoints;
        [SerializeField] private Transform pointTransform;
        [SerializeField] private bool deadEnd;
        [SerializeField] private bool stopCheckpoint;

        public MoveDirection[] GetAvailableDirection => moveDirections;
        public Checkpoint[] GetNextCheckpoints => nextCheckpoints;
        public Transform GetCheckpointTransform => pointTransform;
        public bool DeadEndCheck => deadEnd;
        public bool StopCheckpoint => stopCheckpoint;

        private void OnDrawGizmos()
        {
                Gizmos.color = stopCheckpoint ? Color.red : Color.yellow;
                Gizmos.DrawWireSphere(pointTransform.position, 0.5f);
                Gizmos.color = Color.white;
        }
}