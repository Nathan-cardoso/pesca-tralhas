using UnityEngine;

public class Spawnavel : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    // Update is called once per frame
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