using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int life = 5;

    [SerializeField] private int score = 0;

    [SerializeField] private float speed = 45f;
    [SerializeField] GameManager gameManager;
    private int xLimit = 90;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetGameOver())
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (transform.position.x <= -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }

        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameManager.GetGameOver())
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                life--;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Collectible"))
            {
                score += 10;
                Destroy(other.gameObject);
            }
        }
    }

    public int GetLife()
    {
        return life;
    }
    
    public int GetScore()
    {
        return score;
    }

}
