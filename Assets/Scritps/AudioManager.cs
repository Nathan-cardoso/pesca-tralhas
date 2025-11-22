using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMusica;
    [SerializeField] private AudioClip trilhaSonora;
    

    private bool isGameMuted = false;

    void Start()
    {
        PlayTrilhaSonora();
    }

    void PlayTrilhaSonora()
    {
        audioSourceMusica.clip = trilhaSonora;
        audioSourceMusica.loop = true;
        audioSourceMusica.Play();
    }

    public void MuteGame()
    {

        if (!isGameMuted)
        {
            // Mutar
            audioSourceMusica.Pause();
            isGameMuted = true;
            
        }
        else
        {
            // Desmutar
            audioSourceMusica.UnPause();
            isGameMuted = false;
        }
    }
}
