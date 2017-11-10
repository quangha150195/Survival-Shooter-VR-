using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;
    public static EnemyManager instance;
    public int maxEnemy;
    public GameObject[] enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public static List<GameObject> m_ListEnemy;

    void Start ()
    {
        instance = this;
        m_ListEnemy = new List<GameObject>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (m_ListEnemy.Count < maxEnemy)
        {
            yield return new WaitForSeconds(spawnTime);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject obj = enemy[Random.Range(0, enemy.Length)];
            m_ListEnemy.Add(obj);
            Instantiate(obj, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
