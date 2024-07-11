using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameNetworkManager _gameNetworkManager;
    [SerializeField] private Character _character;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameNetworkManager);
        builder.RegisterComponent(_character)
            .AsImplementedInterfaces()
            .AsSelf();
    }
}
