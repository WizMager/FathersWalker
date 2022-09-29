using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
      [SerializeField] private GameObject player;
      [SerializeField] private Transform playerSpawnPosition;

      private void Awake()
      {
            Instantiate(player, playerSpawnPosition.position, playerSpawnPosition.rotation);
      }
}