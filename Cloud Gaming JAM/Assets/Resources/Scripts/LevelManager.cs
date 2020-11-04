using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public RaftController[] rafts = new RaftController[2];
    private void Awake()
    {
        if (instance == null)
            Init();
    }

    private void Init()
    {
        instance = this;
    }
    
    public void AddNewPlayer(int playerId)
    {
        int teamToJoin = 1;
        if (rafts[0].GetNbrPlayersOnRaft() == 2)
            teamToJoin = 2;
        GameManager.instance.players[playerId].teamId = teamToJoin;
        Debug.Log(GameManager.instance.players[playerId].teamId);
        AddPlayerOnRaft(playerId, teamToJoin-1);
    }
    private void AddPlayerOnRaft(int playerId, int teamId)
    {
        if (rafts[teamId].playersOnRaft.Count < 2)
            rafts[teamId].playersOnRaft.Add(GameManager.instance.players[playerId]);
    }
}
