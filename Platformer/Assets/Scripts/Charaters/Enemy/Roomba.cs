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

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected override void Move(Vector2 moveAmount)
	{
		base.Move (moveAmount);
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		float y = p.y;
		float x = p.x + m_InitialScale.x + m_InitialScale.x/2 * m_LookingDirection;
		
		Ray ray = new Ray(new Vector2(x, y), new Vector2(m_LookingDirection, 0));

		if (Physics.SphereCast(ray, m_InitialScale.x, out m_Hit, Mathf.Abs(m_LookingDirection), m_Targets))
		{
			Debug.Log("Hit");
		}
	}
	#endregion

	#region Private Methods
	#endregion
}