// File: BlackHole
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : Projectile {
	
	// const
	private const float CAMERA_DEPTH = 10f;
	private const float TRAVEL_BY_UNIT = 1000f;
	private const float ABSORB_TIME = 2f;
	private const float STARTING_SCALE = 0.15f;
	private const float TO_SCALE_UP = 1f - STARTING_SCALE;
	private const float GRAVITY_RADIUS = 100f;

	// enum

	// public

	// protected

	// private
	private Vector3 m_TargetPosition;
	private Vector3 m_DistanceToTravel;
	private Vector3 m_InitialPosition;
	private Timer m_AbsorbTimer = new Timer(ABSORB_TIME);
	private List<CharacterPhysics> m_ObjectsInPull = new List<CharacterPhysics>();
	private Vector3 m_InitialScale;

	// properties
	#region Unity API
	protected void Awake()
	{
		m_InitialScale = transform.localScale;
	}
	protected void Update()
	{
		if (m_TravelTimer != null)
		{
			m_TravelTimer.Update();
		}
		if (m_AbsorbTimer != null)
		{
			m_AbsorbTimer.Update();
		}
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();
		m_AbsorbTimer.Stop();
		m_TravelTimer.m_OnUpdate -= Travel;
		m_TravelTimer.m_OnDone -= Absorb;
		m_AbsorbTimer.m_OnUpdate -= Pull;
		m_AbsorbTimer.m_OnUpdate -= CheckInRange;
		m_AbsorbTimer.m_OnDone -= SelfDestruct;
	}

	#endregion

	#region Public Methods
	public void Shoot()
	{
		FindTargetPosition();
		SetUpPath();
	}
	#endregion

	#region Protected Methods
	protected virtual void Travel()
	{
		transform.position = m_InitialPosition + m_DistanceToTravel * m_TravelTimer.Ratio;
		transform.localScale = m_InitialScale * STARTING_SCALE + m_InitialScale * TO_SCALE_UP * m_TravelTimer.Ratio;
	}
	protected void Absorb()
	{
		m_AbsorbTimer.m_OnUpdate = Pull;
		m_AbsorbTimer.m_OnUpdate += CheckInRange;
		m_AbsorbTimer.m_OnDone = SelfDestruct;
		m_AbsorbTimer.Start();
	}

	protected void Pull()
	{
		for (int i = 0 ; i < 360 ; ++i)
		{
			Ray ray = new Ray(transform.position, new Vector3(Mathf.Cos(i), Mathf.Sin(i)));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, GRAVITY_RADIUS))
			{
				Debug.DrawLine(ray.origin, hit.point);
				CharacterPhysics obj = hit.collider.gameObject.GetComponent<CharacterPhysics>();
				if (obj != null && !m_ObjectsInPull.Contains(obj))
				{
					m_ObjectsInPull.Add(obj);
					obj.ForcedMovement = -(hit.transform.position - transform.position).normalized * 0.25f;
				}
			}
		}
	}
	protected void CheckInRange()
	{
		for (int i = m_ObjectsInPull.Count - 1; i >= 0 ; --i)
		{
			if (m_ObjectsInPull[i] != null && (transform.position - m_ObjectsInPull[i].gameObject.transform.position).magnitude > GRAVITY_RADIUS + 0.5f)
			{
				m_ObjectsInPull[i].ForcedMovement = Vector3.zero;
				m_ObjectsInPull.RemoveAt(i);
			}
		}
	}
	#endregion

	#region Private Methods
	private void FindTargetPosition()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = CAMERA_DEPTH;
		m_TargetPosition = Camera.main.ScreenToWorldPoint(mousePos);
	}

	private void SetUpPath()
	{
		m_InitialPosition = transform.position;
		m_DistanceToTravel = m_TargetPosition - m_InitialPosition;
		m_TravelTimer.m_OnUpdate = Travel;
		m_TravelTimer.m_OnDone = Absorb;
		m_TravelTimer.Start( m_DistanceToTravel.magnitude / TRAVEL_BY_UNIT);
	}
	#endregion
}