using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Spawnables")]
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] collectibles;

    [Header("Scritps")]
    [SerializeField] PlayerController playerController;
    [SerializeField] private UiController uiController;
    
    [Header("Game Variables")]
    [SerializeField] private float spawnInterval = 2f; 
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private bool isGameRunning = false;
    private float xSpawnVariation = 93;
    private float zSpawnPos = -250f;

    void Start()
    {
        Time.timeScale = 0f;           
        uiController.ShowMainMenu();     
    }

    public void StartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        uiController.ShowHUD();   
        
        // Nome do metodo, tempo para começar e intervalo de spawn.
        InvokeRepeating(nameof(SpawnRandom), 2f, spawnInterval); 
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        uiController.ShowGameOver(); 
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawnRandom()
    {
        if (!isGameOver)
        {
            if (obstacles.Length == 0 || collectibles.Length == 0)
            {
                Debug.LogWarning("Nenhum obstáculo ou coletável foi configurado no Spawner!");
                return;
            }

            int index;

            // 40% de chance de spawnar um coletável
            // 60% de chance de spawnar um obstáculo
            if (UnityEngine.Random.Range(0, 10) <= 3)
            {
                // Seleciona um coletável aleatóriamente
                index = UnityEngine.Random.Range(0, collectibles.Length);

                // Define um posição de spawn aleatória
                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(xSpawnVariation, -(xSpawnVariation + 15)), collectibles[index].transform.position.y, zSpawnPos);

                // Cria a instancia
                Instantiate(collectibles[index], spawnPos, collectibles[index].transform.rotation);
            }
            else
            {
                // Seleciona um obstáculo aleatóriamente
                index = UnityEngine.Random.Range(0, obstacles.Length);

                // Define um posição de spawn aleatória
                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(xSpawnVariation, -(xSpawnVariation + 15)), obstacles[index].transform.position.y, zSpawnPos);

                // Cria a instancia
                Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
            }

        }
    }

    void Update()
    {
        if (playerController.GetLife() <= 0)
        {
            GameOver();
        }
    }
    
    public bool GetGameOver()
    {
        return isGameOver;
    }
}