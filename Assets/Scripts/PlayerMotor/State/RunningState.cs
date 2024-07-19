using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Enter()
    {
        m_motor.m_animator?.SetBool("Running", true);
        m_motor.m_verticalVelocity = 0.0f;
    }

    public override void Tick()
    {
        if (InputManager.Instance.IsSwipeLeft) m_motor.ChangeLane(-1); // 왼쪽 이동
        if (InputManager.Instance.IsSwipeRight) m_motor.ChangeLane(1); // 오른쪽 이동
        if (InputManager.Instance.IsSwipeUp && m_motor.m_isGrounded) m_motor.ChangeState(GetComponent<JumpingState>()); // 점프
        if (!m_motor.m_isGrounded) m_motor.ChangeState(GetComponent<FallingState>());
        if (InputManager.Instance.IsSwipeDown) m_motor.ChangeState(GetComponent<SlidingState>()); // 슬라이딩
    }

    public override void Exit()
    {
        m_motor.m_animator?.SetBool("Running", false);
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // 이동해야 할 x 조정
        _moveVector.y = -1.0f;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }
}
