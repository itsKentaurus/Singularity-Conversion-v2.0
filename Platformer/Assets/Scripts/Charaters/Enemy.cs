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

		if (m_IsOnWall || !CheckOnEdge() && m_IsGrounded)
		{
			m_Direction *= -1;
		}
		
		moveAmount.x *= m_Direction;
		base.Move(moveAmount);
	}
	#endregion

	#region Private Methods
	private bool CheckOnEdge()
	{
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		float x = p.x + m_ColliderCenter.x + m_ColliderSize.x/2 * m_Direction; // Left, centre and then rightmost point of collider
		float y = p.y + m_ColliderCenter.y - m_ColliderSize.y/2; // Bottom of collider
		Ray m_Ray = new Ray(new Vector2(x,y), new Vector2(0, -1f));
		return (Physics.Raycast(m_Ray,out m_Hit,Mathf.Abs(m_Direction),m_CollisionMask));
	}
	#endregion
}