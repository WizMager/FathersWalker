using UnityEngine;

public class Checkpoint : MonoBehaviour
{
        [SerializeField] private MoveDirection[] moveDirections;
        //If checkpoint use like a waypoint(don't stop on it) first point must be previous point which from start
        //because when chose index of next point used this 
        //_chosenDirectionIndex = _previousCheckpoint == _nextCheckpoints[0] ? 1 : 0;
        //where previous point is current point which now is previous when player complete move to this waypoint
        //P.S. for simple you need reverse previous point in playerController and on this waypoint
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