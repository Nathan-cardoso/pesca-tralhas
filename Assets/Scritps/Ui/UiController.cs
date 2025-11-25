using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;


    [Header("Main Menu Canvas")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    [Header("Game Hud Canvas")]
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private RawImage[] lifeDucks;
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private Button retryButton;
    
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        startButton.onClick.AddListener(OnStartPressed);
        exitButton.onClick.AddListener(OnExitPressed);
        retryButton.onClick.AddListener(OnRetryPressed);
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void ShowHUD()
    {
        mainMenuCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverScoreText.text = "Pontuação: " + playerController.GetScore();
        mainMenuCanvas.SetActive(false);
        hudCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    private void OnStartPressed()
    {
        gameManager.StartGame();
    }

    private void OnExitPressed()
    {
        gameManager.ExitGame();
    }
    
    private void OnRetryPressed()
    {
        gameManager.RestartGame();
    }
    
    void Update()
    {
        for (int i = 0; i < lifeDucks.Length; i++)
        {
            lifeDucks[i].enabled = i < playerController.GetLife();
        }
        scoreText.text = "Pontos: " + playerController.GetScore();
    }
}
