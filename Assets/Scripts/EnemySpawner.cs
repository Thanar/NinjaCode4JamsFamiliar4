﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int round=1;
    public int wave = 1;
    public int wavesPerRound = 5;
    public float timeBetweenWaves = 30;
    public int enemiesCurrentRound = 10;
    public int difficultyVariable = 5;
    public float timeNextWave=0;

    public GameObject[] enemies;

    public Transform[] spawnPoints;

    public int enemiesOnScreen = 0;

    public UI ui;

	// Use this for initialization
	void Start () {
        timeNextWave = Time.time + 6f;
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
        ui.SetRound(this);
        enemiesCurrentRound = difficultyVariable * round;
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
            ui.SetRound(this);
        }
    }

    public void enemyDied()
    {
        enemiesOnScreen--;
        ui.SetRound(this);
        if (enemiesOnScreen == 0 && wave > wavesPerRound)
        {
            NextRound();
        }
    }
}
