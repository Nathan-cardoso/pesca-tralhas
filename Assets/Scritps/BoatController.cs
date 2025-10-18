using UnityEngine;

public class BoatController : MonoBehaviour
{
    private float speed = 40f;
    private int xMinLimit = -175;
    private int xMaxLimit = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lê o eixo horizontal (-1 = esquerda, +1 = direita)
        float move = Input.GetAxis("Horizontal");

        // Cria o vetor de movimento
        Vector3 direction = new Vector3(move, 0f, 0f);

        // Move o objeto usando Transform
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
