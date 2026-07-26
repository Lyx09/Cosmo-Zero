using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnCubeSize = 10;

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(Random.Range(-spawnCubeSize, spawnCubeSize), Random.Range(-spawnCubeSize, spawnCubeSize), Random.Range(-spawnCubeSize, spawnCubeSize));

            var spawnRotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
    }
}
