using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private GameData gameData;
    private PlayerStateData playerStateData;
    private List<Player> players;

    internal void SetGame(GameData gd, List<Player> players, PlayerStateData psd)
    {
        this.gameData = gd;
        this.players = players;
        this.playerStateData = psd;
    }

    internal GameData GetGameData()
    {
        return this.gameData;
    }

    internal PlayerStateData GetPlayerStateData()
    {
        return this.playerStateData;
    }

    internal List<Player> GetGamePlayers()
    {
        return this.players;
    }

    internal void UpdateGameData(GameData gd)
    {
        this.gameData = gd;
    }

    internal void AddPlayer(Player player)
    {
        this.players.Add(player);
    }
}
