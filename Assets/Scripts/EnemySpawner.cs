using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Your enemy prefab
    public int enemyCount = 5;           // How many to spawn
    public Vector2 areaMin;              // Bottom left corner
    public Vector2 areaMax;              // Top right corner

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float x = Random.Range(areaMin.x, areaMax.x);
            float y = Random.Range(areaMin.y, areaMax.y);
            Vector2 position = new Vector2(x, y);
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3((areaMin.x + areaMax.x) / 2, (areaMin.y + areaMax.y) / 2);
        Vector3 size = new Vector3(areaMax.x - areaMin.x, areaMax.y - areaMin.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
