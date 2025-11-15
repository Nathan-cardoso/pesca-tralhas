using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int life = 5;

    [SerializeField] private int score = 0;

    [SerializeField] private float speed = 45f;
    [SerializeField] GameManager gameManager;

    private int xLimit = 92;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }   

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalInput = moveInput.x;

        // Movimento do personagem
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Limita o X do personagem entre -(xLimit + 15) e xLimit
        float clampedX = Mathf.Clamp(transform.position.x, -(xLimit + 10), xLimit);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Obstaculo obstaculoScript = other.GetComponent<Obstaculo>();
            life -= obstaculoScript.getDamage();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Collectible"))
        {
            Coletavel coletavelScritp = other.GetComponent<Coletavel>();
            score += (int)coletavelScritp.getScore();
            Destroy(other.gameObject);
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
