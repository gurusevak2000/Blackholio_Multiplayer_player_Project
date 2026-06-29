using UnityEngine;
using Mirror;

public class MatchManager : NetworkBehaviour
{
    public static MatchManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckForWinner()
    {
        int alivePlayerCount = 0;
        PlayerMovement lastAlivePlayer = null;

        foreach (PlayerMovement player in PlayerMovement.allPlayers)
        {
            if (player != null && player.currentMatchState == MatchState.Playing)
            {
                alivePlayerCount++;
                lastAlivePlayer = player;
            }
        }

        if (alivePlayerCount == 1)
        {
            Debug.Log(lastAlivePlayer.playerName + " is the winner!");
        }
    }

    [ClientRpc]
    private void RpcAnnounceWinner(string winnerName)
    {
        GameManager.Instance.ShowWinnerScreen(winnerName);
    }
}
