// File: PlayerController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	// const

	// enum
	public enum ePlayerDirection
	{
		LEFT,
		RIGHT
	}
	// public
	public float m_WalkingVelocity = 2f;
	public float m_RunningVelocity = 4f;
	public float m_JumpingVelocity = 400f;
	// protected

	// private
	private bool m_HasJumped = false;
	private Vector3 m_MovementVelocity = Vector3.zero;

	#region Unity API
	protected virtual void FixedUpdate()
	{
		Movement();
		ApplyMovement();
		Reset();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void Reset()
	{
		m_MovementVelocity = Vector3.zero;
	}

	protected void Movement()
	{
		if (Input.GetKey("a"))
		{
			WalkDirection(ePlayerDirection.LEFT);
		}
		if (Input.GetKey("d"))
		{
			WalkDirection(ePlayerDirection.RIGHT);
		}
		if (!m_HasJumped&& Input.GetKeyDown("space"))
		{
			m_HasJumped = true;
			Jump();
		}
		if (m_HasJumped && Input.GetKeyUp("space"))
		{
			m_HasJumped = false;
		}
	}
	protected void WalkDirection(ePlayerDirection dir)
	{

		switch(dir)
		{
		case ePlayerDirection.LEFT:
			m_MovementVelocity.x = -m_WalkingVelocity;
			break;
		case ePlayerDirection.RIGHT:
			m_MovementVelocity.x = m_WalkingVelocity;
			break;
		}
	}
	protected virtual void Jump()
	{
		rigidbody.AddForce(transform.up * m_JumpingVelocity);
	}

	protected virtual void ApplyMovement()
	{
		transform.position += m_MovementVelocity * Time.deltaTime;
	}
	#endregion

	#region Private Methods
	private void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.GetComponent<Terrain>().m_Type == Terrain.eTerrainType.FLOOR)
		{
		}
		if(col.gameObject.GetComponent<Terrain>().m_Type == Terrain.eTerrainType.WALL)
		{
		}
	}
	#endregion
}