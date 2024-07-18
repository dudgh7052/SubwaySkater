using UnityEngine;

public class JumpingState : BaseState
{
    /// <summary>
    /// ������
    /// ���ν�����Ʈ���� ���°Ŷ� PlayerMoter�� �ȳ־��൵�ȴ�.
    /// </summary>
    [SerializeField] float m_jumpForce = 7.0f;

    public override void Enter()
    {
        m_motor.m_verticalVelocity = m_jumpForce;
        m_motor.m_animator?.SetTrigger("Jump");
    }

    public override void Tick()
    {
        if (m_motor.m_verticalVelocity < 0)
        {
            m_motor.ChangeState(GetComponent<FallingState>());
        }
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
