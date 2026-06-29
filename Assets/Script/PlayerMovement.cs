using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    public float baseMovementSpeed = 5f;

    [Header("Movement Feel")]
    public float accelerationTime = 0.12f;   // how fast you reach full speed
    public float decelerationTime = 0.08f;   // how fast you stop

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 smoothedDirection = Vector3.zero;

    // ... (rest of your fields remain the same)

    [Header("Growth")]
    public float foodGrowthAmount = 0.1f;

    [Header("Map")]
    public float mapBoundaryLimit = 25f;

    [Header("UI")]
    [SerializeField]
    private GameObject playerNameObject;

    // =========================
    // NETWORKED DATA
    // =========================

    [SyncVar]
    public float currentSize = 1f;

    [SyncVar]
    public MatchState currentMatchState = MatchState.Playing;

    [SyncVar]
    public uint playerId;

    [SyncVar]
    public string playerName = "Player";

    [SyncVar]
    public int score = 0;

    // =========================
    // STATIC PLAYER LIST FOR CACHING (fixes FindObjectsByType spam)
    // =========================
    public static List<PlayerMovement> allPlayers = new List<PlayerMovement>();

    // =========================
    // PLAYER SETUP
    // =========================

    public override void OnStartLocalPlayer()
    {
        string randomPlayerName =
            "Player" + Random.Range(100, 999);

        CmdSetPlayerName(randomPlayerName);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!allPlayers.Contains(this))
        {
            allPlayers.Add(this);
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        allPlayers.Remove(this);
    }

    [Command]
    private void CmdSetPlayerName(string newPlayerName)
    {
        playerName = newPlayerName;
    }

    // =========================
    // UPDATE
    // =========================

    private void Update()
    {
        UpdatePlayerScale();

        if (!CanControlPlayer())
            return;

        HandleMovement();

        ClampPlayerInsideMap();
    }

    // =========================
    // PLAYER CONTROL
    // =========================

    private bool CanControlPlayer()
    {
        return isLocalPlayer && currentMatchState == MatchState.Playing;
    }

    private void UpdatePlayerScale()
    {
        float targetSize = currentSize;
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            Vector3.one * targetSize,
            Time.deltaTime * 8f
        );
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 rawDirection = new Vector3(h, v, 0f).normalized;

        // Smoothly accelerate toward raw input, decelerate when no input
        float smoothTime = rawDirection.magnitude > 0.01f 
            ? accelerationTime 
            : decelerationTime;

        smoothedDirection = Vector3.SmoothDamp(
            smoothedDirection,
            rawDirection,
            ref currentVelocity,
            smoothTime
        );

        float speed = CalculateMovementSpeed();
        transform.position += smoothedDirection * speed * Time.deltaTime;
    }

    private float CalculateMovementSpeed()
    {
        return Mathf.Clamp(
            baseMovementSpeed /
            Mathf.Sqrt(currentSize),
            1.5f,
            baseMovementSpeed
        );
    }

    private void ClampPlayerInsideMap()
    {
        Vector3 currentPosition =
            transform.position;

        float playerRadius =
            transform.localScale.x * 0.5f;

        currentPosition.x = Mathf.Clamp(
            currentPosition.x,
            -mapBoundaryLimit + playerRadius,
            mapBoundaryLimit - playerRadius
        );

        currentPosition.y = Mathf.Clamp(
            currentPosition.y,
            -mapBoundaryLimit + playerRadius,
            mapBoundaryLimit - playerRadius
        );

        transform.position = currentPosition;
    }

    // =========================
    // COLLISIONS
    // =========================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanControlPlayer())
            return;

        HandleFoodCollision(collision);

        HandlePlayerCollision(collision);
    }

    private void HandleFoodCollision(Collider2D collision)
    {
        if (!collision.CompareTag("Food"))
            return;

        NetworkIdentity foodNetworkIdentity =
            collision.GetComponent<NetworkIdentity>();

        if (foodNetworkIdentity != null)
        {
            CmdEatFood(foodNetworkIdentity.netId);
        }
    }

    private void HandlePlayerCollision(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerMovement targetPlayer =
            collision.GetComponent<PlayerMovement>();

        if (targetPlayer == null)
            return;

        if (targetPlayer == this)
            return;

        if (currentSize > targetPlayer.currentSize + 0.2f)
        {
            CmdRequestPlayerConsumption(targetPlayer.netId);
        }
    }

    // =========================
    // FOOD SYSTEM
    // =========================

    [Command]
    private void CmdEatFood(uint foodNetId)
    {
        if (!NetworkServer.spawned.TryGetValue(
                foodNetId,
                out NetworkIdentity foodNetworkIdentity))
        {
            return;
        }

        currentSize += foodGrowthAmount;

        score += 10;

        NetworkServer.Destroy(
            foodNetworkIdentity.gameObject);

        FoodSpawner.Instance.SpawnOneFood();
    }

    // =========================
    // PLAYER EATING SYSTEM
    // =========================

    [Command]
    private void CmdRequestPlayerConsumption(uint eatenPlayerNetId)
    {
        if (!NetworkServer.spawned.TryGetValue(
                eatenPlayerNetId,
                out NetworkIdentity eatenPlayerIdentity))
        {
            return;
        }

        PlayerMovement eatenPlayer =
            eatenPlayerIdentity.GetComponent<PlayerMovement>();

        if (eatenPlayer == null)
            return;

        if (currentSize <= eatenPlayer.currentSize + 0.2f)
            return;

        currentSize +=
            eatenPlayer.currentSize * 0.5f;

        score += 100;

        eatenPlayer.HandlePlayerDeath();
    }

    // =========================
    // DEATH SYSTEM
    // =========================

    [Server]
    public void HandlePlayerDeath()
    {
        currentMatchState =
            MatchState.Dead;

        MatchManager.Instance.CheckForWinner();

        TargetShowDeathScreen(
            connectionToClient);

        RpcHandlePlayerDeath();
    }

    [ClientRpc]
    private void RpcHandlePlayerDeath()
    {
        SpriteRenderer playerRenderer =
            GetComponent<SpriteRenderer>();

        Collider2D playerCollider =
            GetComponent<Collider2D>();

        if (playerRenderer != null)
            playerRenderer.enabled = false;

        if (playerCollider != null)
            playerCollider.enabled = false;

        if (playerNameObject != null)
            playerNameObject.SetActive(false);
    }

    [TargetRpc]
    private void TargetShowDeathScreen(NetworkConnection target)
    {
        GameManager.Instance.ShowPlayerEliminatedScreen();
    }
    public void EnterSpectatorMode()
    {
        if (!isLocalPlayer)
            return;

        CmdEnterSpectatorMode();
    }
    [Command]
    private void CmdEnterSpectatorMode()
    {
        currentMatchState = MatchState.Spectating;
    }
}