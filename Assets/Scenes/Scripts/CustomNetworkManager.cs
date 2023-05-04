using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient  conn)
    {
        // Instantiate the player prefab
        GameObject player = Instantiate(playerPrefab);

        // Set the player's position to a spawn point
        player.transform.position = GetStartPosition().position;

        // Spawn the player on the network
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}


