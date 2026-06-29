using System.Linq;
using UnityEngine;

public class SpectatorManager : MonoBehaviour
{
    public static SpectatorManager Instance;

    private PlayerMovement currentSpectateTarget;

    private void Awake()
    {
        Instance = this;
    }

    public PlayerMovement GetCurrentSpectateTarget()
    {
        return currentSpectateTarget;
    }

    public void FindBestPlayerToSpectate()
    {
        FindNewSpectateTarget();
    }

    public void FindNewSpectateTarget()
    {
        currentSpectateTarget =
            PlayerMovement.allPlayers
            .Where(player =>
                player != null && player.currentMatchState == MatchState.Playing)
            .OrderByDescending(player =>
                player.currentSize)
            .FirstOrDefault();
    }
}