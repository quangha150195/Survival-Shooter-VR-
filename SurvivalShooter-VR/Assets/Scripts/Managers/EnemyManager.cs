using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int m_maxEnemy;
    public float spawnTime = 3f;
    public GameObject[] enemy;
    public Transform[] spawnPoints;
    public static int m_numCurrentEnemy;

    void Start ()
    {
        instance = this;
        m_numCurrentEnemy = 0;
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    public IEnumerator SpawnEnemy()
    {
        while (PlayerHealth.instance.currentHealth > 0)
        {
            yield return new WaitForSeconds(spawnTime);
            if (m_numCurrentEnemy < m_maxEnemy)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                GameObject obj = enemy[Random.Range(0, enemy.Length)];
                m_numCurrentEnemy++;
                Instantiate(obj, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
        }
    }
}
