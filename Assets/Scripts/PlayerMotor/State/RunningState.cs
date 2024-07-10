using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override Vector3 ProcessMotion()
    {
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // 이동해야 할 x 조정
        _moveVector.y = -1.0f;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }

    public override void Tick()
    {
        if (InputManager.Instance.IsSwipeLeft)
        {
            // 왼쪽 이동
            m_motor.ChangeLane(-1);
        }

        if (InputManager.Instance.IsSwipeRight)
        {
            // 오른쪽 이동
            m_motor.ChangeLane(1);
        }

        if (InputManager.Instance.IsSwipeUp && m_motor.m_isGrounded)
        {
            // 점프 상태로
            //m_motor.ChangeState()
        }
    }
}
