using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    private float scrollSpeed = 0.1f;
    private Renderer rend;
    private Vector2 currentOffset = Vector2.zero;

    // A função Awake é executada antes da execução do projeto, funcionando inclusive antes da função start.
    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        RiverScroll();
    }

    void RiverScroll()
    {
        currentOffset.y += scrollSpeed * Time.deltaTime;

        if (rend.material.HasProperty("_BaseMap"))
            rend.material.SetTextureOffset("_BaseMap", currentOffset);
        else if (rend.material.HasProperty("_MainTex"))
            rend.material.SetTextureOffset("_MainTex", currentOffset);
    }
}
