using UnityEngine;
using Zenject;

public class PlayerMovementInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement playerMovement;
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle().NonLazy();
    }
}