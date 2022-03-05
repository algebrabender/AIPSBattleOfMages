using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    GameData gameData;
    List<Player> players;

    internal void SetGame(GameData gm, List<Player> players)
    {
        this.gameData = gm;
        this.players = players;
    }

    internal GameData GetGameData()
    {
        return this.gameData;
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
