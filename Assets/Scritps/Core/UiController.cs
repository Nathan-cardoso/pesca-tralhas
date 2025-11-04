using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    // Scripts
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameManager gameManager;


    [Header("Canvas")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button startButton;


    [Header("HUD")]
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private Button retryButton;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        startButton.onClick.AddListener(OnPlayPressed);
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

    private void OnPlayPressed()
    {
        gameManager.StartGame();
    }

    private void OnRetryPressed()
    {
        gameManager.RestartGame();
    }

    void Update()
    {
        if (!gameManager.GetGameOver())
        {
            lifeText.text = "Vidas: " + playerController.GetLife();
            scoreText.text = "Pontos: " + playerController.GetScore();
        }     
    }
}
