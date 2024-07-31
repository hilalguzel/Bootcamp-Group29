using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanYaratici : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsters;
    int randomSpawnPoint, randomMonster;
    public static bool spawnAllowed;
    public CharacterController karakter;
    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnAMonster", 0f, 1f);
    }
    void SpawnAMonster()
    {
        if (spawnAllowed && !karakter.oyunBittimi)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomMonster = Random.Range(0, monsters.Length);
            Instantiate(monsters[randomMonster], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
}
