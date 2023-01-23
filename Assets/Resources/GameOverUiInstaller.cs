using UnityEngine;
using Zenject;

public class StartGameUIInstaller : MonoInstaller
{
    [SerializeField] private StartGameUI startGameUi;
    public override void InstallBindings()
    {
        Container.Bind<StartGameUI>().FromInstance(startGameUi).AsSingle();
    }
}