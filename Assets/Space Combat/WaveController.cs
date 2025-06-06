using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    private Transform[] spawnPoints;
    private readonly List<GameObject> enemies = new();
    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>().Skip(1).ToArray();
        Debug.Log($"spawn points: {string.Join(";", spawnPoints.Select(el => el.position).ToArray())}");
        EnemyData data = waves[0].Enemies.First();
        SpawnEnemy(data.Ship, data.Weapon);
    }

    private void SpawnEnemy(ShipData ship, WeaponData data)
    {
        GameObject obj = Instantiate(ship.ShipPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        obj.GetComponent<WeaponController>().Configure(data, Target.PLAYER);
        enemies.Add(obj);
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
