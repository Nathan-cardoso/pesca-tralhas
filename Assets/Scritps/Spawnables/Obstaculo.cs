using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        MoveLeft();
        DestroyOutBounds();
    }

    void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.z += speed * Time.deltaTime;
        transform.position = pos;
    }

    void DestroyOutBounds()
    {
        if (transform.position.z >= 250f)
        {
            Destroy(gameObject);
        }
    }

    public int getDamage()
    {
        return damage;
    }
}