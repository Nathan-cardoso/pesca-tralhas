using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private int life;
    [SerializeField] private int score;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameOverText.enabled = false;
    }

    void Update()
    {
        if (!gameManager.GetGameOver())
        {
            lifeText.text = "Vidas: " + playerController.GetLife();
            scoreText.text = "Pontos: " + playerController.GetScore();
        }
        else
        {
            gameOverText.enabled = true;
        }
        
    }
}
