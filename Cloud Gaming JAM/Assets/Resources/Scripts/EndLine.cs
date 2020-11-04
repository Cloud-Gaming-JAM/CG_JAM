using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    List<PlayerController> playersFinished = new List<PlayerController>();

    void OnTrigger2DEnter(Collider2D player)
    {
        if (player.GetComponent<PlayerController>() != null && !playersFinished.Contains(player.GetComponent<PlayerController>()))
        {
            playersFinished.Add(player.GetComponent<PlayerController>());
        }

        if (playersFinished.Count == GameManager.instance.player.Length)
        {
            EndGame(/*WinnerID*/);
        }
    }

    void EndGame(/*int WinnerID*/)
    {

    }
}
