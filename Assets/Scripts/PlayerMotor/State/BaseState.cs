using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor m_motor;

    void Awake()
    {
        m_motor = GetComponent<PlayerMotor>();
    }

    public virtual void Enter() { } // Enter
    public virtual void Exit() { } // Exit
    public virtual void Tick() { } // Update

    public virtual Vector3 ProcessMotion()
    {
        Debug.Log("Process motion is not implemented in " + this.ToString());
        return Vector3.zero;
    }
}