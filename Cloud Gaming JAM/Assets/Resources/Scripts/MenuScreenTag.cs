using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenTag : MonoBehaviour
{
    public MenuController.MenuScreen menuScreenType = 0;

    void OnDisable()
    {
        if (menuScreenType == MenuController.MenuScreen.CharacterSelection)
        {
            foreach (PlayerController player in GameManager.instance.players)
            {
                player.playerId = 0;
                player.teamId = 0;
            }
            foreach (RaftController raft in LevelManager.instance.rafts)
            {
                raft.playersOnRaft.Clear();
            }
            MenuController.instance.SetActiveLeavePlayerImage(1);
            MenuController.instance.SetActiveLeavePlayerImage(2);
            MenuController.instance.SetActiveLeavePlayerImage(3);
            MenuController.instance.SetActiveLeavePlayerImage(4);
        }
    }
}
