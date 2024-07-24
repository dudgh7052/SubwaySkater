using UnityEngine;

public class FallingState : BaseState
{
    public override void Enter()
    {
        m_motor.m_animator?.SetTrigger("Fall");
    }

    public override void Tick()
    {
        // �� ���� �� ���׻��·�
        if (m_motor.m_isGrounded)
        {
            m_motor.ChangeState(GetComponent<RunningState>());
        }

        if (InputManager.Instance.IsSwipeLeft) m_motor.ChangeLane(-1); // ���� �̵�
        if (InputManager.Instance.IsSwipeRight) m_motor.ChangeLane(1); // ������ �̵�
    }

    public override Vector3 ProcessMotion()
    {
        // �߷� ����
        m_motor.ApplyGravity();

        // �ǵ��ƿ��� ���� ����
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // �̵��ؾ� �� x ����
        _moveVector.y = m_motor.m_verticalVelocity;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }
}
