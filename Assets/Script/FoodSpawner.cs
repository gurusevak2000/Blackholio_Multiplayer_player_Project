using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner instance;

    public GameObject foodPrefab;

    public int foodCount = 30;

    public float minX = -25f;
    public float maxX = 25f;

    public float minY = -25f;
    public float maxY = 25f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
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
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        Instantiate(foodPrefab, randomPosition, Quaternion.identity);
    }
}