using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class GameManager : MonoBehaviour
{
    [Header("Spawnables")]
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] collectibles;
    [SerializeField] private GameObject lifeDuck;

    [Header("Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UiController uiController;

    [Header("Game Variables")]
    [SerializeField] private float spawnInterval = 3f; 
    [SerializeField] private float gameSpeed = 1f;    
    [SerializeField] private float oldGameSpeed = 1f;    
    private float xSpawnVariation = 80f;
    private float zSpawnPos = -250f;
    private bool isGamePaused = false;
    private Coroutine spawnCoroutine;
    private Coroutine speedCoroutine;

    void Start()
    {
        Time.timeScale = 0f;
        uiController.ShowMainMenu();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        uiController.ShowHUD();

        // Inicia as corrotinas de spawn e aumento de velocidade
        spawnCoroutine = StartCoroutine(SpawnRoutine());
        speedCoroutine = StartCoroutine(UpdateSpeedRoutine());
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        uiController.ShowGameOver();

        // Para as corrotinas ao encerrar o jogo
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        if (speedCoroutine != null) StopCoroutine(speedCoroutine);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            SpawnRandom();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator UpdateSpeedRoutine()
    {
        yield return new WaitForSeconds(2f); 

        while (true)
        {
            yield return new WaitForSeconds(1f); // Intervalo de aumento da velocidade
            IncreaseGameDifficulty();
        }
    }

    void SpawnRandom()
    {
        if (obstacles.Length == 0 || collectibles.Length == 0)
        {
            Debug.LogWarning("Nenhum obstáculo ou coletável foi configurado no Spawner!");
            return;
        }
        if (isGamePaused)
        {
            return;
        }

        int index;
        bool spawnCollectible = UnityEngine.Random.Range(0, 10) <= 4;

        if (spawnCollectible)
        {

            bool spawnLifeDuck = UnityEngine.Random.Range(0, 10) <= 0;
            if (spawnLifeDuck)
            {     
                Vector3 spawnPos = new Vector3(
                    UnityEngine.Random.Range(-(xSpawnVariation + 20), xSpawnVariation),
                    lifeDuck.transform.position.y,
                    zSpawnPos
                );
                Instantiate(lifeDuck, spawnPos, lifeDuck.transform.rotation);
            }
            else {
                index = UnityEngine.Random.Range(0, collectibles.Length);
                Vector3 spawnPos = new Vector3(
                    UnityEngine.Random.Range(-(xSpawnVariation + 20), xSpawnVariation),
                    collectibles[index].transform.position.y,
                    zSpawnPos
                );
                Instantiate(collectibles[index], spawnPos, collectibles[index].transform.rotation);
            }
        }
        else
        {
            index = UnityEngine.Random.Range(0, obstacles.Length);
            Vector3 spawnPos = new Vector3(
                UnityEngine.Random.Range(-(xSpawnVariation + 20), xSpawnVariation),
                obstacles[index].transform.position.y,
                zSpawnPos
            );
            Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
        }
    }

    void Update()
    {
        if (playerController.GetLife() <= 0)
        {
            GameOver();
        }
    }

    public void IncreaseGameDifficulty()
    {
        if (!isGamePaused)
        {        
            if (gameSpeed < 5.0f)
            {
                if(spawnInterval > 0.4f)
                {
                    spawnInterval -= 0.01f;
                }
                gameSpeed += 0.01f;
            }
        }
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void PlayGame()
    {
        isGamePaused = false;
        gameSpeed = oldGameSpeed;
    }
    
    public void PauseGame()
    {
        isGamePaused = true;
        oldGameSpeed = gameSpeed;
        gameSpeed = 0;
    }
}
