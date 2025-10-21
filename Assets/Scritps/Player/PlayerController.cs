using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // [SerializeField] private int life = 5;
    [SerializeField] private float speed = 45f;
    private int xLimit = 90;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }
}
