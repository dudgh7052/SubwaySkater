using UnityEngine;

public class FallingState : BaseState
{
    public override void Enter()
    {
        m_motor.m_animator?.SetTrigger("Fall");
    }

    public override void Tick()
    {
        // 땅 밟을 시 러닝상태로
        if (m_motor.m_isGrounded)
        {
            m_motor.ChangeState(GetComponent<RunningState>());
        }

        if (InputManager.Instance.IsSwipeLeft) m_motor.ChangeLane(-1); // 왼쪽 이동
        if (InputManager.Instance.IsSwipeRight) m_motor.ChangeLane(1); // 오른쪽 이동
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
