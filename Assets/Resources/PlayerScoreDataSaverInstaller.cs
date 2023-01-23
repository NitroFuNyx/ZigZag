using UnityEngine;
using Zenject;

public class PlayerScoreDataSaverInstaller : MonoInstaller
{
    [SerializeField] private PlayerScoreDataSaver _playerScoreDataSaver;
    public override void InstallBindings()
    {
        Container.Bind<PlayerScoreDataSaver>().FromInstance(_playerScoreDataSaver).AsSingle();

    }
}