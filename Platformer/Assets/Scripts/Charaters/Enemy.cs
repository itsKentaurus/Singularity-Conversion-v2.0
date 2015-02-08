// File: Enemy
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : CharacterBase {
	
	// const
	private const float CHANGE_DIRECTION = -1f;
	// enum

	// public

	// protected
	[SerializeField]
	protected bool m_IsPatroling = true;
	[SerializeField]
	protected LayerMask m_Targets;
	protected float m_LookingDirection = 1;

	// private

	#region Unity API
	protected void Start()
	{
		m_AmountToMove.x = m_LookingDirection * m_Speed;
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
			m_LookingDirection *= CHANGE_DIRECTION;
		}
		
		moveAmount.x *= m_LookingDirection;
		base.Move(moveAmount);
	}
	#endregion

	#region Private Methods
	private bool CheckOnEdge()
	{
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		float x = p.x + m_InitialScale.x/2 * m_LookingDirection;
		float y = p.y - m_InitialScale.y/2;
		Ray m_Ray = new Ray(new Vector2(x,y), new Vector2(0, -1f));
		return (Physics.Raycast(m_Ray,out m_Hit,Mathf.Abs(m_LookingDirection), m_CollisionMask));
	}
	#endregion
}