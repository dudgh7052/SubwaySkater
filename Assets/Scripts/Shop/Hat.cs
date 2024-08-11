using UnityEngine;

[CreateAssetMenu(fileName = "Hat")]
public class Hat : ScriptableObject
{
    public string m_itemName;
    public int m_itemPrice;
    public Sprite m_thumbnail;
    public GameObject m_model;
}
