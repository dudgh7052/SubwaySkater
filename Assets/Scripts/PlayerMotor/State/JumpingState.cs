using UnityEngine;

public class JumpingState : BaseState
{
    /// <summary>
    /// 점프력
    /// 점핑스테이트에서 쓰는거라 PlayerMoter에 안넣어줘도된다.
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
        // 중력 적용
        m_motor.ApplyGravity();

        // 되돌아오는 벡터 리턴
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // 이동해야 할 x 조정
        _moveVector.y = m_motor.m_verticalVelocity;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    } 
}
