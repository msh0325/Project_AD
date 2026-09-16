using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private int enemeyCount = 0;
    void Start()
    {
        SpawnEnemies();
    }
    
    private void SpawnEnemies()
    {
        foreach(Transform point in spawnPoints)
        {
            GameObject obj = Instantiate(enemyPrefab, point.position, Quaternion.identity);
            Enemy e = obj.GetComponent<Enemy>();

            e.onDeath += HandleEnemyDeath;
            enemeyCount++;
        }
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemeyCount --;
        Debug.Log($"남은 적: {enemeyCount}");

        if(enemeyCount <= 0)
        {
            Debug.Log("방 클리어");
            
        }
    }
}
