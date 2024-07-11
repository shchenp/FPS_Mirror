using Mirror;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameNetworkManager : NetworkManager
{
    private IObjectResolver _container;

    [Inject]
    private void Construct(IObjectResolver container)
    {
        _container = container;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? _container.Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : _container.Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
