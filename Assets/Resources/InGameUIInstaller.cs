using UnityEngine;
using Zenject;

public class InGameUIInstaller : MonoInstaller
{
    [SerializeField] private InGameUI inGameUI;
    public override void InstallBindings()
    {
        Container.Bind<InGameUI>().FromInstance(inGameUI).AsSingle();
    }
}