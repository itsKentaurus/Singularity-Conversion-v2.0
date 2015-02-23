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
	[SerializeField] protected float m_Skin = .005f;
	[SerializeField] protected bool m_AffectedByExternals = true;
	[SerializeField] protected bool m_AffectedByGravity = true;
	[SerializeField] protected bool m_IsHitableByProjectile = true;

	protected bool m_IsGrounded = true;
	protected bool m_IsOnWall = false;
	protected Vector3 m_FinalTransform = Vector3.zero;
	protected Vector3 m_ExternalModifiers = Vector3.zero;
	protected Vector2 m_AmountToMove = Vector2.zero;
	protected Vector3 m_InitialScale = Vector3.zero;
	protected float m_CurrentSpeed;

	// private

	// properties
	public Vector3 ForcedMovement
	{
		set	{	m_ExternalModifiers = value;	}
	}

	public bool HitByProjectile
	{
		get { return m_IsHitableByProjectile;	}
	}
	#region Unity API
	protected virtual void OnTriggerEnter(Collider c)
	{
		Trigger trigger = c.GetComponent<Trigger>();
		if (trigger != null && trigger.CanTriggerAction(this))
		{
			trigger.ActivateTrigger();
		}

		MeshRenderer mesh = GetComponent<MeshRenderer>();
		if (c.GetComponent<PlatformerCamera>() != null && mesh != null)
		{
			mesh.enabled = true;
		}
	}

	protected virtual void OnTriggerExit(Collider c)
	{
		MeshRenderer mesh = GetComponent<MeshRenderer>();
		if (c.GetComponent<PlatformerCamera>() != null && mesh != null)
		{
			mesh.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	protected virtual void Awake() 
	{
		m_InitialScale = transform.localScale;
	}

	protected virtual void Update()
	{
		if (PauseManager.Instance.IsPaused)
		{
			return;
		}

		ApplyGravity();
		Move(m_AmountToMove * Time.deltaTime);
	}
	#endregion

	#region Public Methods
	public virtual void Hit(Projectile proj)
	{
		// TODO Rigil: Do stuff with projectile
	}
	#endregion

	#region Protected Methods
	protected virtual void ApplyGravity()
	{
		if (m_AffectedByGravity)
		{
			m_AmountToMove.y -= m_Gravity * Time.deltaTime;
		}
	}
	protected virtual void Move(Vector2 moveAmount) 
	{
		m_IsGrounded = false;
		
		float deltaY = GetDeltaY(moveAmount.y);
		float deltaX = GetDeltaX(moveAmount.x);
		
		m_IsOnWall = (deltaX == STOP_VELOCITY);

		m_FinalTransform = new Vector2(deltaX,deltaY);

		transform.Translate(m_FinalTransform + (m_AffectedByExternals ? m_ExternalModifiers : Vector3.zero));
	}
	#endregion

	#region Private Methods
	private float GetDeltaY(float deltaY)
	{
		Ray ray;
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE && deltaY != STOP_VELOCITY; ++i)
		{
			float dir = Mathf.Sign(deltaY);
			float x = p.x + m_InitialScale.x/2 * i; // Left, centre and then rightmost point of collider
			float y = p.y + m_InitialScale.y/2 * dir; // Bottom of collider
			
			ray = new Ray(new Vector2(x,y), new Vector2(0,dir));
			Debug.DrawRay(ray.origin, ray.direction);
			if (Physics.Raycast(ray,out m_Hit,Mathf.Abs(deltaY), m_CollisionMask)) 
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance (ray.origin, m_Hit.point);
				
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
		Ray ray;
		RaycastHit m_Hit;
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE && deltaX != STOP_VELOCITY; ++i)
		{
			float dir = Mathf.Sign(deltaX);
			float y = (p.y  - m_InitialScale.y/2) + m_InitialScale.y/2 * i;
			float x = p.x + m_InitialScale.x/2 * dir;
			
			ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			Debug.DrawRay(ray.origin,ray.direction);
			if (Physics.Raycast(ray, out m_Hit, Mathf.Abs(deltaX), m_CollisionMask)) 
			{
				return STOP_VELOCITY;
			}
		}
		
		return deltaX;
	}	
	#endregion
}