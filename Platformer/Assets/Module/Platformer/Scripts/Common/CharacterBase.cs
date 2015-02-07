// File: CharacterBase
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : Subject {
	
	// const
	private const int NUMBER_OF_RAYCASE = 3;
	private const int STOP_VELOCITY = 0;

	// enum

	// public
	public LayerMask m_CollisionMask;
	public float m_Gravity = 1000f;
	public float m_Speed = 1000f;
	public float m_JumpHeight = 1000f;

	// protected
	protected bool m_IsGrounded = true;
	protected bool m_IsOnWall = false;
	protected Vector3 m_FinalTransform = Vector3.zero;
	protected Vector3 m_ExternalModifiers = Vector3.zero;
	protected Vector2 m_AmountToMove = Vector2.zero;
	protected float m_CurrentSpeed;
	protected Vector3 m_ColliderSize;
	protected Vector3 m_ColliderCenter;

	// private
	[SerializeField]
	private float m_Skin = .005f;

	// properties
	public Vector3 ForcedMovement
	{
		set	{	m_ExternalModifiers = value;	}
	}
	#region Unity API
	protected virtual void Awake() 
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		m_ColliderSize = collider.size;
		m_ColliderCenter = collider.center;
	}

	protected virtual void Update()
	{
		if (!PauseController.IsInstanceNull)
		{
			return;
		}

		ApplyGravity();
		Move(m_AmountToMove * Time.deltaTime);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void ApplyGravity()
	{
		m_AmountToMove.y -= m_Gravity * Time.deltaTime;
	}
	protected virtual void Move(Vector2 moveAmount) 
	{
		m_IsGrounded = false;
		
		float deltaY = GetDeltaY(moveAmount.y);
		float deltaX = GetDeltaX(moveAmount.x);
		
		m_IsOnWall = (deltaX == STOP_VELOCITY);

		m_FinalTransform = new Vector2(deltaX,deltaY);
		
		transform.Translate(m_FinalTransform + m_ExternalModifiers);
	}
	#endregion

	#region Private Methods
	private float GetDeltaY(float deltaY)
	{
		Ray m_Ray;
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE && deltaY != STOP_VELOCITY; ++i)
		{
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + m_ColliderCenter.x - m_ColliderSize.x/2) + m_ColliderSize.x/2 * i; // Left, centre and then rightmost point of collider
			float y = p.y + m_ColliderCenter.y + m_ColliderSize.y/2 * dir; // Bottom of collider
			
			m_Ray = new Ray(new Vector2(x,y), new Vector2(0,dir));
			Debug.DrawRay(m_Ray.origin,m_Ray.direction);
			if (Physics.Raycast(m_Ray,out m_Hit,Mathf.Abs(deltaY),m_CollisionMask)) 
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance (m_Ray.origin, m_Hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > m_Skin) 
				{
					deltaY = dst * dir + m_Skin;
				}
				else 
				{
					deltaY = STOP_VELOCITY;
				}
				
				m_IsGrounded = true;
				
				return deltaY;
				
			}
		}
		return deltaY;
	}
	
	private float GetDeltaX(float deltaX)
	{
		Ray m_Ray;
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE && deltaX != STOP_VELOCITY; ++i)
		{
			float dir = Mathf.Sign(deltaX);
			float y = (p.y + m_ColliderCenter.y - m_ColliderSize.y/2) + m_ColliderSize.y/2 * i;
			float x = p.x + m_ColliderCenter.x + m_ColliderSize.x/2 * dir;
			
			m_Ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			Debug.DrawRay(m_Ray.origin,m_Ray.direction);
			if (Physics.Raycast(m_Ray, out m_Hit, Mathf.Abs(deltaX), m_CollisionMask)) 
			{
				return STOP_VELOCITY;
			}
		}
		
		return deltaX;
	}	
	#endregion
}