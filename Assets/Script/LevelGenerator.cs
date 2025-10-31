using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public int numberOfPlatforms = 5;
    public Transform startPoint;

    private Transform lastSpawnPoint;

    void Start()
    {
        lastSpawnPoint = startPoint;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnNextPlatform();
        }
    }

    void SpawnNextPlatform()
    {
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject newPlatform = Instantiate(platformPrefabs[index], lastSpawnPoint.position, Quaternion.identity);

        // Encuentra el SpawnPoint del bloque recién creado
        Transform nextSpawn = newPlatform.transform.Find("SpawnPoint");

        if (nextSpawn != null)
        {
            lastSpawnPoint = nextSpawn;
        }
    }
}
