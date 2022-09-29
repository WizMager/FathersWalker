using UnityEngine;

public class Checkpoint : MonoBehaviour
{
        [SerializeField] private MoveDirection[] moveDirections;
        [SerializeField] private Checkpoint[] nextPoints;
        [SerializeField] private bool waypoint;
        [SerializeField] private bool deadEnd;

        public MoveDirection[] GetAvailableDirection => moveDirections;
        public Checkpoint[] GetNextCheckpoints => nextPoints;
        public bool WaypointCheck => waypoint;
        public bool DeadEndCheck => deadEnd;
}