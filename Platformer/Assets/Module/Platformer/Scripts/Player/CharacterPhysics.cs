using UnityEngine;
using System.Collections;

public class CharacterPhysics : CharacterBase 
{
	private const int NUMBER_OF_RAYCASE = 3;
	private const int STOP_VELOCITY = 0;
	
	public LayerMask m_CollisionMask;

	protected bool m_IsGrounded;
	protected Vector3 m_FinalTransform = Vector3.zero;
	protected Vector3 m_ExternalModifiers = Vector3.zero;

	private Vector3 m_ColliderSize;
	private Vector3 m_ColliderCenter;
	[SerializeField]
	private float m_Skin = .005f;
	Ray m_Ray;
	RaycastHit m_Hit;

	public Vector3 ForcedMovement
	{
		set	{	m_ExternalModifiers = value;	}
	}

	protected void Awake() 
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		m_ColliderSize = collider.size;
		m_ColliderCenter = collider.center;
	}

	protected virtual void Move(Vector2 moveAmount) 
	{
		m_IsGrounded = false;
		
		float deltaY = GetDeltaY(moveAmount.y);
		float deltaX = GetDeltaX(moveAmount.x);

		m_FinalTransform = new Vector2(deltaX,deltaY);
		
		transform.Translate(m_FinalTransform + m_ExternalModifiers);
	}

	private float GetDeltaY(float deltaY)
	{
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE; ++i)
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
				break;
				
			}
		}
	}

	private float GetDeltaX(float deltaX)
	{
		Vector2 p = transform.position;
		for (int i = 0; i<NUMBER_OF_RAYCASE && deltaX != 0; ++i)
		{
			float dir = Mathf.Sign(deltaX);
			float y = (p.y + m_ColliderCenter.y - m_ColliderSize.y/2) + m_ColliderSize.y/2 * i;
			float x = p.x + m_ColliderCenter.x + m_ColliderSize.x/2 * dir;
			
			m_Ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			Debug.DrawRay(m_Ray.origin,m_Ray.direction);
			if (Physics.Raycast(m_Ray, out m_Hit, Mathf.Abs(deltaX), m_CollisionMask)) 
			{
				return STOP_VELOCITY;
				break;
			}
		}

		return deltaX;
	}
}
