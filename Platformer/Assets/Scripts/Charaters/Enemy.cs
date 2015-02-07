// File: Enemy
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : CharacterBase {
	
	// const

	// enum

	// public
	public bool m_IsPatroling = true;

	// protected
	protected float m_Direction = 1;

	// private

	#region Unity API
	protected void Start()
	{
		m_AmountToMove.x = m_Direction * m_Speed;
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected override void Move(Vector2 moveAmount)
	{
		if (!m_IsPatroling)
		{
			return;
		}

		if (m_IsOnWall)
		{
			m_Direction *= -1;
		}
		
		moveAmount.x *= m_Direction;
		base.Move(moveAmount);
	}
	#endregion

	#region Private Methods
	#endregion
}