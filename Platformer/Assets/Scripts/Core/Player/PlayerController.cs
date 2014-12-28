// File: PlayerController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : Subject {
	
	// const

	// enum
	public enum ePlayerDirection
	{
		LEFT,
		RIGHT
	}

	public enum eMovingState
	{
		MOVING,
		CLLIMBING,
		JUMPING
	}

	// public
	public float m_WalkingVelocity = 2f;
	public float m_RunningVelocity = 4f;
	public float m_JumpingVelocity = 400f;
	public float m_GravityForce = 0.05f;
	public LineRenderer m_LineDirection;
	// protected

	// private
	private bool m_HasJumped = false;
	private bool m_ApplyGravity = false;
	private bool m_TouchFloor = false;
	private bool m_TouchWall = false;
	private Vector3 m_MovementWalkVelocity = Vector3.zero;
	private Vector3 m_MovementJumpVelocity = Vector3.zero;

	#region Unity API
	protected virtual void FixedUpdate()
	{
		Movement();
		ApplyGravity();
		ApplyMovement();
		Reset();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void Reset()
	{
		m_MovementWalkVelocity = Vector3.zero;
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
		if (!m_HasJumped && Input.GetKeyDown("space"))
		{
			Jump();
		}
	}
	protected void WalkDirection(ePlayerDirection dir)
	{

		switch(dir)
		{
		case ePlayerDirection.LEFT:
			m_MovementWalkVelocity += -transform.right;
			break;
		case ePlayerDirection.RIGHT:
			m_MovementWalkVelocity += transform.right;
			break;
		}
	}
	protected virtual void Jump()
	{
		m_HasJumped = true;
//		transform.localPosition -= new Vector3(0f, 3f, 0f);
		m_JumpingVelocity = 5f;
	}

	protected virtual void ApplyMovement()
	{
		m_LineDirection.SetPosition(1, m_MovementWalkVelocity + m_MovementJumpVelocity);
		transform.position += (m_MovementWalkVelocity + m_MovementJumpVelocity) * Time.deltaTime;
	}
	protected virtual void ApplyGravity()
	{
		if (m_HasJumped)
		{
			m_JumpingVelocity -= 0.15f;
		}
		else
		{
			m_JumpingVelocity = 0f;
			m_HasJumped = false;
		}
		m_MovementJumpVelocity = new Vector3(0f, m_JumpingVelocity, 0f);
	}
	#endregion

	#region Private Methods
	public void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.GetComponent<Terrain>().m_Type == Terrain.eTerrainType.FLOOR)
		{
			m_HasJumped = false;
		}
		if(col.gameObject.GetComponent<Terrain>().m_Type == Terrain.eTerrainType.WALL)
		{
			m_TouchWall = true;
		}
	}
	#endregion
}