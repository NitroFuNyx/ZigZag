using UnityEngine;
using Zenject;

public class PauseGameHandlerInstaller : MonoInstaller
{
    [SerializeField] private PauseGameHandler pauseGameHandler;

    public override void InstallBindings()
    {
        Container.Bind<PauseGameHandler>().FromInstance(pauseGameHandler).AsSingle();
    }
}