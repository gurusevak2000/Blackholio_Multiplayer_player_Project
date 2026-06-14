using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5f;
    public float growAmount = 0.1f;
    [SerializeField]
    private GameObject playerNameObject;

    [SyncVar]
    public float size = 1f;
    [SyncVar]
    public bool isDead = false;
    [SyncVar]
    public uint playerId;
    [SyncVar]
    public string playerName = "Player";
    
    public override void OnStartLocalPlayer()
    {
        string randomName =
            "Player" + Random.Range(100, 999);

        CmdSetPlayerName(randomName);
    }
    [Command]
    void CmdSetPlayerName(string newName)
    {
        playerName = newName;
    }

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
        if (!isLocalPlayer || isDead) return;

        // =========================
        // FOOD
        // =========================
        if (collision.CompareTag("Food"))
        {
            var netId = collision.GetComponent<NetworkIdentity>();
            if (netId != null)
            {
                CmdEatFood(netId.netId);
            }
            return;
        }

        // =========================
        // PLAYER
        // =========================
        if (collision.CompareTag("Player"))
        {
            PlayerMovement otherPlayer = collision.GetComponent<PlayerMovement>();
            if (otherPlayer == null || otherPlayer == this) return;

            // Client-side prediction for responsiveness
            if (size > otherPlayer.size + 0.2f)
            {
                CmdPlayerEaten(otherPlayer.netId);
            }
        }
    }

    [Command]
    void CmdEatFood(uint foodNetId)
    {
        if (!NetworkServer.spawned.TryGetValue(foodNetId, out NetworkIdentity foodIdentity))
            return;

        // Server validation (optional but good)
        if (foodIdentity == null) return;

        // Grow
        size += growAmount;

        // Destroy on server → syncs to all clients
        NetworkServer.Destroy(foodIdentity.gameObject);

        // Respawn food on server
        FoodSpawner.instance.SpawnOneFood();
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
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (playerNameObject != null)
            playerNameObject.SetActive(false);
    }
    [TargetRpc]
    void TargetShowDeathScreen(NetworkConnection target)
    {
        GameManager.instance.ShowDeathScreen();
    }
}