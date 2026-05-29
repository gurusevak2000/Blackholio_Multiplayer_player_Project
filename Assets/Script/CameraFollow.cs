using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10);

    private Transform target;

    void LateUpdate()
    {
        // if no target yet, try find local player
        if (target == null)
        {
            PlayerMovement[] players =
                FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

            foreach (PlayerMovement player in players)
            {
                if (player.isLocalPlayer)
                {
                    target = player.transform;
                    break;
                }
            }
        }

        // follow target
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}   