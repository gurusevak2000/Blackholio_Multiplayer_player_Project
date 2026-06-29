using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public TMP_Text leaderboardText;

    private void Start()
    {
        InvokeRepeating(
            nameof(UpdateLeaderboard),
            1f,
            1f
        );
    }

    void UpdateLeaderboard()
    {
        var sortedPlayers = PlayerMovement.allPlayers
            .Where(p => p != null && p.currentMatchState == MatchState.Playing)
            .OrderByDescending(p => p.score)
            .Take(5)
            .ToList();

        string board = "LEADERBOARD\n\n";

        int rank = 1;

        foreach (var player in sortedPlayers)
        {
            board +=
                rank +
                ". " +
                player.playerName +
                "   " +
                player.score.ToString() +
                "\n";

            rank++;
        }

        leaderboardText.text = board;
    }
}