using UnityEngine;
using Zenject;

public class PlayerScoreDataLoaderInstaller : MonoInstaller
{
    [SerializeField] private PlayerScoreDataLoader _playerScoreDataLoader;
    public override void InstallBindings()
    {
        Container.Bind<PlayerScoreDataLoader>().FromInstance(_playerScoreDataLoader).AsSingle();

    }
}