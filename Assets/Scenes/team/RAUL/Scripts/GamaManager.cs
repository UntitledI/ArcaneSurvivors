using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public class SyncPlayerList : SyncList<Player> { }

    public SyncPlayerList players = new SyncPlayerList();

    // Function to add players to the list
    [Server]
    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    // Function to remove players from the list
    [Server]
    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }
}