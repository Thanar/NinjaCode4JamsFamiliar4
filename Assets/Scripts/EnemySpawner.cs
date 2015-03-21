using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int round=1;
    public int wave = 1;
    public int wavesPerRound = 5;
    public float timeBetweenWaves = 20;
    public int enemiesCurrentRound = 20;
    public float timeNextWave=0;

    public GameObject[] enemies;

    public Transform[] spawnPoints;

    public int enemiesOnScreen = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timeNextWave < Time.time)
        {
            wave++;
            if (wave > wavesPerRound)
            {
                //NextRound();
                timeNextWave = int.MaxValue;
            }

            SpawnWave();
            timeNextWave = Time.time + timeBetweenWaves;
            
        }
	}

    public void NextRound()
    {
        wave = 1;
        round++;
        enemiesCurrentRound = 10 + 10 * round;
        SpawnWave();
    }

    public void SpawnWave()
    {
        for (int i = 0; i < enemiesCurrentRound / wavesPerRound; i++)
        {
            int enemyID = Random.Range(0,enemies.Length);
            int spawnPointID = Random.Range(0,spawnPoints.Length);
            Instantiate(enemies[enemyID], spawnPoints[spawnPointID].position + (Vector3.right * Random.Range(-1f, 1f)) + (Vector3.forward * Random.Range(-1f, 1f)), spawnPoints[spawnPointID].rotation);
            enemiesOnScreen++;
        }
    }

    public void enemyDied()
    {
        enemiesOnScreen--;
        if (enemiesOnScreen == 0 && wave > wavesPerRound)
        {
            NextRound();
        }
    }
}
