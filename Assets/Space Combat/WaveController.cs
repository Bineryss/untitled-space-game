using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    private Transform[] spawnPoints;
    private readonly List<GameObject> enemies = new();
    [SerializeField] private int ActiveWave = 0;
    void Awake()
    {
        StartMission();
    }

    public void StartMission()
    {
        spawnPoints = GetComponentsInChildren<Transform>().Skip(1).ToArray();
        SpawnWave(ActiveWave);
    }

    public void SpawnWave(int wave)
    {
        if (wave >= waves.Length)
        {
            Debug.Log("Wave doesn't exist!");
            return;
        }

        Wave activeWave = waves[wave];
        StartCoroutine(SpawnWaveCoroutine(activeWave));
    }

    private IEnumerator SpawnWaveCoroutine(Wave activeWave)
    {
        foreach (EnemyData data in activeWave.Enemies)
        {
            for (int i = 0; i < data.Ammount; i++)
            {
                // Pick random spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject obj = ShipFactory.SpawnShip(data.Ship, data.Weapon, Target.PLAYER, spawnPoint);
                enemies.Add(obj);

                // Wait before spawning next enemy
                yield return new WaitForSeconds(0.5f);
            }
        }
    }




    /* Pseudo Code
        load level config
        get spawn points

        read first wave
        spawn enemies
        if all ememies are dead
        => spawn next wave


        //Wave object
        -list: ship-data + weapon data + ammount
    */
}
