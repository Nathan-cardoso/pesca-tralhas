using UnityEngine;
using UnityEngine.UI;

public class MuteButtonController : MonoBehaviour
{

    [SerializeField] private Image icon;
    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Sprite UnMuteIcon;
    [SerializeField] private Button muteButton;
    [SerializeField] private AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        muteButton.onClick.AddListener(OnMutePressed);
    }

    private void OnMutePressed()
    {
        audioManager.MuteGame();
        icon.sprite = audioManager.GetIsGameMuted() ? muteIcon : UnMuteIcon ;
    }
}
