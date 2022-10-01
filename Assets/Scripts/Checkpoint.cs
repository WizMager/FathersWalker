using UnityEngine;

public class Checkpoint : MonoBehaviour
{
        [SerializeField] private MoveDirection[] moveDirections;
        //If you use "deadEnd" character rotate to point from which it come
        //If you use "stopCheckpoint" you need rotate how player will stay on this point(forward will be forward on this point)
        //If you use point like waypoint(only for walk through) you need add next point and previous in "nextCheckpoint"
        //"nextCheckpoint" : 0 element is turn left, 1 is turn right, 2 is turn forward
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