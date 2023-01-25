using UnityEngine;
using Zenject;

public class WaypointInstaller : MonoInstaller
{
    [SerializeField] private WaypointMover waypointMover;
    public override void InstallBindings()
    {
        Container.Bind<WaypointMover>().FromInstance(waypointMover).AsSingle().NonLazy();
    }
}