using UnityEngine;
using Zenject;

public class GameOverUiInstaller : MonoInstaller
{
    [SerializeField] private GameOverUI gameOverUI;

    public override void InstallBindings()
    {
        Container.Bind<GameOverUI>().FromInstance(gameOverUI).AsSingle();

    }
}