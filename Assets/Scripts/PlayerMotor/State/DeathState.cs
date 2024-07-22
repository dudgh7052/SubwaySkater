using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] Vector3 m_knockbackForce = new Vector3 (0, 4.0f, -3.0f);
    Vector3 m_currentKnockback = Vector3.zero;

    public override void Enter()
    {
        m_motor.m_animator?.SetTrigger("Death");
        m_currentKnockback = m_knockbackForce;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 _moveVector = m_currentKnockback;

        m_currentKnockback = new Vector3(
            0,
            m_currentKnockback.y -= m_motor.m_gravity * Time.deltaTime,
            m_currentKnockback.z += 2.0f * Time.deltaTime);

        if (m_currentKnockback.z > 0.0f)
        {
            m_currentKnockback.z = 0.0f;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }

        return m_currentKnockback;
    }
}