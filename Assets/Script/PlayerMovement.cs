using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5f;
    public float growAmount = 0.1f;

    [SyncVar]
    public float size = 1f;
    [SyncVar]
    public bool isDead = false;

    void Update()
    {
        // sync scale
        transform.localScale = Vector3.one * size;

        // only local player moves
        if (!isLocalPlayer) return;
        // dead players cannot move
        if (isDead) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, v, 0);

        transform.position += movement * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLocalPlayer) return;

        // =========================
        // FOOD
        // =========================
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);

            size += growAmount;
        }

        // =========================
        // PLAYER
        // =========================
        // In OnTriggerEnter2D, replace the player collision block:
        if (collision.CompareTag("Player"))
        {
            PlayerMovement otherPlayer = collision.GetComponent<PlayerMovement>();

            if (otherPlayer == null) return;
            if (otherPlayer == this) return;

            if (size > otherPlayer.size + 0.2f)
            {
                // Tell the SERVER what happened — don't act directly
                CmdPlayerEaten(otherPlayer.netId);
            }
        }
    }

    // New Command: client tells server "I ate this player"
    [Command]
    void CmdPlayerEaten(uint eatenNetId)
    {
        // Server-side validation: re-check sizes to prevent cheating
        if (!NetworkServer.spawned.TryGetValue(eatenNetId, out NetworkIdentity eatenIdentity))
            return;

        PlayerMovement eaten = eatenIdentity.GetComponent<PlayerMovement>();
        if (eaten == null) return;

        // Re-validate on server (don't trust client blindly)
        if (size <= eaten.size + 0.2f) return;

        // Grow the eating player (SyncVar auto-syncs to all clients)
        size += eaten.size * 0.5f;

        // Tell the eaten player's client to show death screen
        // connectionToClient is valid here because we're on the server
        eaten.Die();
    }

    // [Server] tag is correct here — this only runs on server
    [Server]
    public void Die()
    {
        isDead = true;

        TargetShowDeathScreen(connectionToClient);

        RpcHandleDeath();
    }

    [ClientRpc]
    void RpcHandleDeath()
    {
        // hide visuals
        GetComponent<SpriteRenderer>().enabled = false;

        // disable collisions
        GetComponent<Collider2D>().enabled = false;
    }
        [TargetRpc]
    void TargetShowDeathScreen(NetworkConnection target)
    {
        GameManager.instance.ShowDeathScreen();
    }
}