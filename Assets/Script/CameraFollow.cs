using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_FollowPlayer.position.x, transform.position.y, transform.position.z);
    }
}
