using System;
using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variaveis de configuração dos Spawnaveis
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float xSpawnVariation = 85;
    [SerializeField] private bool isGameOver = false;
    [SerializeField] PlayerController playerController;
    private float zSpawnPos = -250f;


    void Start()
    {
        // Nome do metodo, tempo para começar e intervalo de spawn.
        InvokeRepeating(nameof(SpawnRandom), 2f, spawnInterval);
    }

    void SpawnRandom()
    {
        if (!isGameOver)
        {
            if (spawnPrefabs.Length == 0)
            {
                Debug.LogWarning("Nenhum prefab configurado no Spawner!");
                return;
            }

            // Escolhe um prefab aleatoriamente
            int index = UnityEngine.Random.Range(0, spawnPrefabs.Length);

            // Define posição aleatória
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(xSpawnVariation, -xSpawnVariation), spawnPrefabs[index].transform.position.y, zSpawnPos);

            // Instancia
            Instantiate(spawnPrefabs[index], spawnPos, spawnPrefabs[index].transform.rotation);
        }
    }

    void Update()
    {
        if (playerController.GetLife() <= 0)
        {
            isGameOver = true;
        }
    }
    
    public bool GetGameOver()
    {
        return isGameOver;
    }
}