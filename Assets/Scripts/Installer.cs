using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private GameObject canvasPrefab;
        [SerializeField] private Checkpoint firstCheckpoint;

        public override void InstallBindings()
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerComponents>(playerPrefab, playerSpawnPosition.position,
                playerSpawnPosition.rotation, null);
            var canvasInstantiate = Container.InstantiatePrefabForComponent<CanvasComponents>(canvasPrefab);

            Container.Bind<PlayerComponents>().FromInstance(playerInstance).AsSingle().NonLazy();
            Container.Bind<CanvasComponents>().FromInstance(canvasInstantiate).AsSingle().NonLazy();
            Container.Bind<Checkpoint>().FromInstance(firstCheckpoint).AsSingle().NonLazy();
        }
}