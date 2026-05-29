using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;

    public int foodCount = 30;

    // map boundaries
    public float minX = -5f;
    public float maxX = 5f;

    public float minY = -5f;
    public float maxY = 5f;

    void Start()
    {
        SpawnFood();
    }

    void SpawnFood()
    {
        for (int i = 0; i < foodCount; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

            Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        }
    }
}