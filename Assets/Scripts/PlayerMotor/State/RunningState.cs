using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override Vector3 ProcessMotion()
    {
        Vector3 _moveVector = Vector3.zero;

        _moveVector.x = m_motor.SnapToLane(); // �̵��ؾ� �� x ����
        _moveVector.y = -1.0f;
        _moveVector.z = m_motor.m_baseRunSpeed;

        return _moveVector;
    }

    public override void Tick()
    {
        if (InputManager.Instance.IsSwipeLeft)
        {
            // ���� �̵�
            m_motor.ChangeLane(-1);
        }

        if (InputManager.Instance.IsSwipeRight)
        {
            // ������ �̵�
            m_motor.ChangeLane(1);
        }

        if (InputManager.Instance.IsSwipeUp && m_motor.m_isGrounded)
        {
            // ���� ���·�
            //m_motor.ChangeState()
        }
    }
}
