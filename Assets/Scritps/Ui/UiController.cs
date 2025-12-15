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
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;

    [Header("Game Hud Canvas")]
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private RawImage[] lifeDucks;
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private Button retryButton;

    [Header("Tutorial Screen")]
    [SerializeField] private RawImage ModalScreen;
    [SerializeField] private Button tutorialExitButton;
    [SerializeField] private Button tutorialNextButton;
    [SerializeField] private Button tutorialPreviousButton;
    [SerializeField] private GameObject[] tutorialPanels;

    private int currentTutorialIndex = 0;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        startButton.onClick.AddListener(OnStartPressed);
        helpButton.onClick.AddListener(OpenHelpModal);
        tutorialExitButton.onClick.AddListener(CloseHelpModal);
        exitButton.onClick.AddListener(OnExitPressed);
        retryButton.onClick.AddListener(OnRetryPressed);

        // Adiciona os listeners para os botões de navegação do tutorial
        tutorialNextButton.onClick.AddListener(OnNextTutorialPressed);
        tutorialPreviousButton.onClick.AddListener(OnPreviousTutorialPressed);

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

    private void OpenHelpModal()
    {
        ModalScreen.gameObject.SetActive(true);
        ShowTutorialPanel(0);  // Iniciar no primeiro painel do tutorial
    }

    private void CloseHelpModal()
    {
        ModalScreen.gameObject.SetActive(false);
    }

    // Função para exibir o painel atual do tutorial com base no índice
    private void ShowTutorialPanel(int index)
    {
        if (index < 0 || index >= tutorialPanels.Length) return;  // Verifica se o índice é válido

        // Desativa todos os painéis
        foreach (var panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        // Ativa o painel do índice atual
        tutorialPanels[index].SetActive(true);
    }

    // Função para navegar para o próximo painel do tutorial (circular)
    private void OnNextTutorialPressed()
    {
        currentTutorialIndex++;
        if (currentTutorialIndex >= tutorialPanels.Length)
        {
            currentTutorialIndex = 0; // volta para o primeiro
        }
        ShowTutorialPanel(currentTutorialIndex);
    }

    // Função para navegar para o painel anterior do tutorial (circular)
    private void OnPreviousTutorialPressed()
    {
        currentTutorialIndex--;
        if (currentTutorialIndex < 0)
        {
            currentTutorialIndex = tutorialPanels.Length - 1; // vai para o último
        }
        ShowTutorialPanel(currentTutorialIndex);
    }

}
