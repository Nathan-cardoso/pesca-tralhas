using UnityEngine;

public class Spawnavel : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    protected virtual void Update()
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
}