using UnityEngine;

public class TestManager : MonoBehaviour
{
    [Header("=== MOVEMENT & CAMERA TESTS ===")]
    [Tooltip("Toggle to test smooth acceleration/deceleration")]
    public bool Test_SmoothMovement = true;

    [Tooltip("Toggle to test smooth camera follow")]
    public bool Test_CameraSmoothing = true;

    [Tooltip("Toggle to test smooth player scale growth")]
    public bool Test_SmoothScaleGrowth = true;

    [Header("=== CORE GAMEPLAY TESTS ===")]
    public bool Test_FoodEating = true;
    public bool Test_PlayerEating = true;
    public bool Test_DeathAndSpectate = true;
    public bool Test_Leaderboard = true;
    public bool Test_WinnerScreen = true;

    [Header("=== QUICK TEST BUTTONS ===")]
    [Tooltip("Right-click on this component in Inspector → Test options")]
    public bool ShowTestOptions = true;

    // ====================== TEST METHODS ======================

    [ContextMenu("Test Smooth Movement")]
    public void TestMovement()
    {
        Debug.Log("✅ <color=green>Smooth Movement Test:</color> Move your player using WASD. It should accelerate and decelerate smoothly.");
    }

    [ContextMenu("Test Camera Smoothing")]
    public void TestCamera()
    {
        Debug.Log("✅ <color=green>Camera Smoothing Test:</color> Move around. The camera should follow smoothly without snapping.");
    }

    [ContextMenu("Test Smooth Scale Growth")]
    public void TestScaleGrowth()
    {
        Debug.Log("✅ <color=green>Scale Growth Test:</color> Eat some food. Your player should grow smoothly instead of snapping.");
    }

    [ContextMenu("Simulate Winner Screen")]
    public void SimulateWinner()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowWinnerScreen("TestWinner123");
            Debug.Log("✅ <color=green>Winner Screen Triggered Successfully!</color>");
        }
        else
        {
            Debug.LogError("❌ GameManager.Instance not found!");
        }
    }

    [ContextMenu("Simulate Death + Spectate")]
    public void SimulateDeathAndSpectate()
    {
        PlayerMovement localPlayer = FindLocalPlayer();
        if (localPlayer != null)
        {
            localPlayer.HandlePlayerDeath();
            Debug.Log("✅ <color=green>Death + Spectate Flow Triggered!</color>");
        }
        else
        {
            Debug.LogError("❌ Local player not found!");
        }
    }

    private PlayerMovement FindLocalPlayer()
    {
        foreach (var p in PlayerMovement.allPlayers)
        {
            if (p != null && p.isLocalPlayer)
                return p;
        }
        return null;
    }
}