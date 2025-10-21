using UnityEngine;

public class Spawnable : MonoBehaviour
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
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyOutBounds()
    {
        if (transform.position.z >= 250f)
        {
            Destroy(gameObject);
        }
    }
}
