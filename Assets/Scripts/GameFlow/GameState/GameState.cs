using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager m_brain;

    private void Awake()
    {
        m_brain = GetComponent<GameManager>();
    }

    public virtual void Enter() 
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void Exit()
    {

    }
}
