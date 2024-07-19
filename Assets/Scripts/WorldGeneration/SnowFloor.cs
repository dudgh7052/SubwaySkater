using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] Material m_material;
    [SerializeField] float m_offsetSpeed = 0.25f;

    void Update()
    {
        transform.position = Vector3.forward * m_player.transform.position.z;
        m_material.SetVector("snowOffset", new Vector2(0, -transform.position.z * m_offsetSpeed));
    }
}
