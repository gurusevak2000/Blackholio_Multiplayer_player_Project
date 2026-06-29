using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Camera Feel")]
    public float cameraSmoothing = 6f;
    private Transform target;

    private bool isSpectating = false;

    private void LateUpdate()
    {
        UpdateCameraTarget();
        FollowCurrentTarget();
    }

    // =========================
    // CAMERA MODE
    // =========================
    public void EnableSpectateMode()
    {
        isSpectating = true;
        target = null;
    }
    
    public void DisableSpectateMode()
    {
        isSpectating = false;
        target = null;
    }

    // =========================
    // TARGET SELECTION
    // =========================

    private void UpdateCameraTarget()
    {
        if (isSpectating)
        {
            UpdateSpectateTarget();
        }
        else
        {
            UpdateLocalPlayerTarget();
        }
    }

    private void UpdateLocalPlayerTarget()
    {
        // Already following someone
        if (target != null)
            return;

        foreach (PlayerMovement player in PlayerMovement.allPlayers)
        {
            if (player != null && player.isLocalPlayer)
            {
                target = player.transform;
                return;
            }
        }
    }

    private void UpdateSpectateTarget()
    {
        PlayerMovement spectateTarget = 
            SpectatorManager.Instance.GetCurrentSpectateTarget();

        if (spectateTarget == null || 
            spectateTarget.currentMatchState != MatchState.Playing)
        {
            SpectatorManager.Instance.FindNewSpectateTarget();
            spectateTarget = 
                SpectatorManager.Instance.GetCurrentSpectateTarget();
        }

        if (spectateTarget != null)
        {
            target = spectateTarget.transform;
        }
    }

    // =========================
    // CAMERA MOVEMENT
    // =========================

    private void FollowCurrentTarget()
    {   
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * cameraSmoothing
        );
    }
}