using UnityEngine;
using Zenject;

public class PlayerScoreInstaller : MonoInstaller
{
    [SerializeField] private PlayerScore playerScore;
    public override void InstallBindings()
    {
        Container.Bind<PlayerScore>().FromInstance(playerScore).AsSingle();
    }
}