using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Player Setings")]
    [SerializeField] private int life = 5;
    [SerializeField] private int score = 0;
    [SerializeField] GameManager gameManager;

    [Header("Movement Setings")]
    [SerializeField] private float maxSpeed = 20f; 
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 10f;
    private float currentSpeed = 0f;         
    
    [Header("Tilt Setings")]
    [SerializeField] private float tiltAmount = 15f;     
    [SerializeField] private float tiltSpeed = 30f;      

    private int xLimit = 80;
    private CharacterController characterController;
    private Vector2 moveInput;

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
        if (gameManager.GetIsGamePaused())
            return;

        MovePlayer();
        BoatTilt();
    }

    void MovePlayer()
    {
        /*
            Movimento
        */
        float horizontalInput = moveInput.x;
        if (horizontalInput != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, horizontalInput * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        transform.Translate(Vector3.right * Time.deltaTime * currentSpeed, Space.World); // Movimento do barco
        float newX = Mathf.Clamp(transform.position.x + currentSpeed * Time.deltaTime, -(xLimit + 20), xLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if ((newX >= xLimit && currentSpeed > 0) || (newX <= -(xLimit + 20) && currentSpeed < 0))
        {
            currentSpeed = 0f;
        }

        
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
        } else if (other.gameObject.CompareTag("LifeDuck"))
        {
            Destroy(other.gameObject);
            if(life == 5)
            {
                score += 100;
            }
            else if (life < 5)
            {
                life++;
            }
        }
    }

    private void BoatTilt()
    {
        float targetTilt = -(currentSpeed / maxSpeed) * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(0, targetTilt, targetTilt);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            tiltSpeed * Time.deltaTime
        );
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
