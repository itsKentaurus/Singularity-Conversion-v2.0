// File: Roomba
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Roomba : Enemy {
	
	// const

	// enum

	// public

	// protected
	protected float m_Direction = 1;

	// private

	#region Unity API
	protected void Start()
	{
		m_AmountToMove = new Vector2(100f, 0f);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected override void Move (Vector2 moveAmount)
	{
		if (m_IsOnWall)
		{
 			m_Direction *= -1;
		}

		moveAmount.x *= m_Direction;
		base.Move (moveAmount);
	}
	#endregion

	#region Private Methods
	#endregion
}