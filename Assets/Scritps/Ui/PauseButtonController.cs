using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pauseButton.onClick.AddListener(OnPausePressed);
        AtualizeIcon();
    }

    public void OnPausePressed()
    {
        gameManager.PlayPauseGame();
        AtualizeIcon();
    }
    
    public void AtualizeIcon()
    {
        icon.sprite = gameManager.GetIsGamePaused() ? playIcon : pauseIcon ;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            OnPausePressed();
        }
    }

}
