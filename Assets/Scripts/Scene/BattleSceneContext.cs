using System.Collections.Generic;
using Buildings;
using UnityEngine;

public class BattleSceneContext : Singletone<BattleSceneContext>
{
    [field: SerializeField] public BuildingsPlacer Placer { get; private set; }
    [field: SerializeField] public BuildedObjectsCollection ObjectsCollection { get; private set; }
    
    private readonly List<Player.Player> _players = new();

    public List<Player.Player> PlayersCopy => new(_players);
    public Player.Player OwnedPlayer => GetOwnedPlayer();

    protected override void OnAwake()
    {
        
    }

    public void RegisterPlayer(Player.Player player)
    {
        _players.Add(player);
        player.Health.Died += OnPlayerDied;
    }

    private void OnPlayerDied(Player.Player player)
    {
        _players.Remove(player);
    }

    public Player.Player GetOwnedPlayer()
    {
        foreach(var player in _players)
        {
            if(player.IsOwner)
            {
                return player;
            }
        }
        return null;
    }
}
