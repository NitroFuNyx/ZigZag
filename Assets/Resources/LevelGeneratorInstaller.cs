using UnityEngine;
using Zenject;

public class LevelGeneratorInstaller : MonoInstaller
{
    [SerializeField] private LevelGenerator levelGenerator;
    public override void InstallBindings()
    {
        Container.Bind<LevelGenerator>().FromInstance(levelGenerator).AsSingle();
    }
}