using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variaveis de configuração dos Spawnaveis
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float xSpawnVariation = 85;
    private float zSpawnPos = -250f;
    private float ySpawnPos = 2.5f;


    void Start()
    {
        // Nome do metodo, tempo para começar e intervalo de spawn.
        InvokeRepeating(nameof(SpawnRandom), 2f, spawnInterval);
    }
    
    void SpawnRandom()
    {
        if (spawnPrefabs.Length == 0)
        {
            Debug.LogWarning("Nenhum prefab configurado no Spawner!");
            return;
        }

        // Escolhe um prefab aleatoriamente
        int index = Random.Range(0, spawnPrefabs.Length);

        // Define posição aleatória
        Vector3 spawnPos = new Vector3(Random.Range(xSpawnVariation, -xSpawnVariation), ySpawnPos , zSpawnPos);

        // Instancia
        Instantiate(spawnPrefabs[index], spawnPos, Quaternion.identity);
    }
}
