using UnityEngine;
using Mirror;

public class FoodSpawner : NetworkBehaviour
{
    public static FoodSpawner Instance;

    public GameObject foodPrefab;

    public int foodCount = 30;

    public float minX = -25f;
    public float maxX = 25f;
    public float minY = -25f;
    public float maxY = 25f;

    private void Awake()
    {
        Instance = this;
    }

    // Fires on server only, AFTER Mirror server is fully active
    public override void OnStartServer()
    {
        SpawnInitialFood();
    }

    void SpawnInitialFood()
    {
        for (int i = 0; i < foodCount; i++)
        {
            SpawnOneFood();
        }
    }

    public void SpawnOneFood()
    {
        if (!NetworkServer.active) return;

        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        GameObject food = Instantiate(
            foodPrefab,
            randomPosition,
            Quaternion.identity
        );

        NetworkServer.Spawn(food);
    }
}