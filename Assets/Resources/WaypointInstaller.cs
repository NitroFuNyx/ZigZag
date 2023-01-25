using UnityEngine;
using Zenject;

public class WaypointInstaller : MonoInstaller
{
    [SerializeField] private WaypointMover _waypointMover;
    public override void InstallBindings()
    {
        Container.Bind<WaypointMover>().FromInstance(_waypointMover).AsSingle().NonLazy();
    }
}