using UnityEngine;
using System.Collections;

public class PlayerController : CharacterBase {
	
	// const
	
	// enum
	
	// public
	
	// protected

	// private

	protected override void ApplyGravity()
	{
		base.ApplyGravity();

		// TODO Rigil: Slow down when attached to wall
	}

	protected override void Update () 
	{
		base.Update();

		m_CurrentSpeed = Input.GetAxisRaw("Horizontal") * m_Speed;

		if (m_IsGrounded) 
		{
			m_AmountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) 
			{
				m_AmountToMove.y = m_JumpHeight;
			}
		}

		m_AmountToMove.x = m_CurrentSpeed;
	}
}
