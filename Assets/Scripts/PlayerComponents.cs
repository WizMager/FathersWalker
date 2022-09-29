using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
        [SerializeField] private Transform playerTransform;

        public Transform GetPlayerTransform => playerTransform;
}