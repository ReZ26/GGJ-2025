using UnityEngine;

public class CaveMoving : MonoBehaviour
{
    [SerializeField] private Material m_Material;
    [SerializeField] private float m_speed=0.01f;
    private Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += Time.deltaTime * m_speed;
        m_Material.mainTextureOffset = offset;
    }
}
