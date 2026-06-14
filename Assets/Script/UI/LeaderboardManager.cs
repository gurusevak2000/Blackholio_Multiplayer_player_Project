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
        PlayerMovement[] players =
            FindObjectsByType<PlayerMovement>(
                FindObjectsSortMode.None
            );

        var sortedPlayers = players
            .Where(p => p != null && !p.isDead)// filter out dead players and null references
            .OrderByDescending(p => p.size)
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
                player.size.ToString("F1") +
                "\n";

            rank++;
        }

        leaderboardText.text = board;
    }
}