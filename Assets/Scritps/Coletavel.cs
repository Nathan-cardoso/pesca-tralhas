using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int score = 10; 
    private GameManager gameManager;
    
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        MoveLeft();
        DestroyOutBounds();
    }

    void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.z += speed * Time.deltaTime * gameManager.GetGameSpeed();
        transform.position = pos;
    }

    void DestroyOutBounds()
    {
        if (transform.position.z >= 250f)
        {
            Destroy(gameObject);
        }
    }

    public float getScore()
    {
        return score;
    }
}