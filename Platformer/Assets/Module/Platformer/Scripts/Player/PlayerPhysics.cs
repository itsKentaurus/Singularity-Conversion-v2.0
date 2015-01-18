using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask m_CollisionMask;

	private BoxCollider collider;
	private Vector3 m_ColliderSize;
	private Vector3 m_ColliderCenter;

	[SerializeField]
	private float m_Skin = .005f;
	
	[HideInInspector]
	public bool m_IsGrounded;
	
	Ray m_Ray;
	RaycastHit m_Hit;
	
	void Start() {
		collider = GetComponent<BoxCollider>();
		m_ColliderSize = collider.size;
		m_ColliderCenter = collider.center;
	}

	public void Move(Vector2 moveAmount) 
	{
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;
		
		// Check collisions above and below
		m_IsGrounded = false;
		
		for (int i = 0; i<3; ++i)
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
					deltaY = 0;
				}
				
				m_IsGrounded = true;
				
				break;
				
			}
		}
		
		for (int i = 0; i<3 && deltaX != 0; ++i)
		{
			float dir = Mathf.Sign(deltaX);
			float y = (p.y + m_ColliderCenter.y - m_ColliderSize.y/2) + m_ColliderSize.y/2 * i;
			float x = p.x + m_ColliderCenter.x + m_ColliderSize.x/2 * dir;
			
			m_Ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			Debug.DrawRay(m_Ray.origin,m_Ray.direction);
			if (Physics.Raycast(m_Ray, out m_Hit, Mathf.Abs(deltaX), m_CollisionMask)) 
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance (m_Ray.origin, m_Hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				deltaX = 0;
				
				break;
				
			}
		}

		
		Vector2 finalTransform = new Vector2(deltaX,deltaY);
		
		transform.Translate(finalTransform);
	}
	
}
