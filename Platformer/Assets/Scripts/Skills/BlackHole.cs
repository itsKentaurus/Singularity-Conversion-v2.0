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
	private const float GRAVITY_RADIUS = 150f;
	private const float DISTANCE = 500f;
	private const float PULL_SPEED = 50f;

	// enum

	// public

	// protected

	// private
	private Vector3 m_DistanceToTravel;
	private Timer m_TravelTimer = new Timer();
	private Timer m_AbsorbTimer = new Timer(ABSORB_TIME);
	private List<CharacterBase> m_ObjectsInPull = new List<CharacterBase>();
	private Vector3 m_InitialScale;

	// properties
	#region Unity API
	protected override void OnTriggerEnter (Collider c)
	{
		// Don't call base
	}
	protected override void Update()
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
		m_TravelTimer.Stop();
		m_AbsorbTimer.Stop();
		m_TravelTimer.m_OnUpdate -= Travel;
		m_TravelTimer.m_OnDone -= Absorb;
		m_AbsorbTimer.m_OnUpdate -= Pull;
		m_AbsorbTimer.m_OnUpdate -= CheckInRange;
		m_AbsorbTimer.m_OnDone -= SelfDestruct;
		ClearPull();
	}
	#endregion

	#region Public Methods
	public override void Shoot()
	{
		FindTargetPosition();
		SetUpPath();
	}
	#endregion

	#region Protected Methods
	protected override void Travel()
	{
		transform.localScale = m_InitialScale * STARTING_SCALE + m_InitialScale * TO_SCALE_UP * m_TravelTimer.Ratio;
		if (Vector3.Distance(transform.position, m_InitialPosition) >= DISTANCE)
		{
			m_TravelTimer.m_OnDone -= Absorb;
			Absorb();
		}
		else
		{
			transform.position = m_InitialPosition + m_DistanceToTravel * m_TravelTimer.Ratio;
		}
	}
	protected void Absorb()
	{
		m_AbsorbTimer.m_OnUpdate = Pull;
		m_AbsorbTimer.m_OnUpdate += CheckInRange;
		m_AbsorbTimer.m_OnDone = SelfDestruct;
		if (!m_AbsorbTimer.IsStarted)
		{
			m_AbsorbTimer.Start();
		}
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
				CharacterBase obj = hit.collider.gameObject.GetComponent<CharacterBase>();
				if (obj != null && !m_ObjectsInPull.Contains(obj))
				{
					m_ObjectsInPull.Add(obj);
					obj.ForcedMovement = -(hit.transform.position - transform.position).normalized * PULL_SPEED;
					Debug.Log("Number of objects in range: " + m_ObjectsInPull.Count);
				}
			}
		}
	}
	protected void CheckInRange()
	{
		for (int i = m_ObjectsInPull.Count - 1; i >= 0 ; --i)
		{
			if (m_ObjectsInPull[i] != null && (transform.position - m_ObjectsInPull[i].gameObject.transform.position).magnitude > GRAVITY_RADIUS)
			{
				m_ObjectsInPull[i].ForcedMovement = Vector3.zero;
				m_ObjectsInPull.RemoveAt(i);
			}
		}
	}
	#endregion

	#region Private Methods
	private void ClearPull()
	{
		foreach (CharacterBase ch in m_ObjectsInPull)
		{
			ch.ForcedMovement = Vector3.zero;
		}
		m_ObjectsInPull.Clear();
	}
	private void FindTargetPosition()
	{
		m_InitialPosition = transform.position;
		m_InitialScale = transform.localScale;

		Vector3 mousePos = Input.mousePosition;
		mousePos.z = CAMERA_DEPTH;
		Ray ray = new Ray(transform.position, Camera.main.ScreenToViewportPoint(mousePos));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, DISTANCE))
		{
			m_TargetPosition = hit.point;
			Debug.DrawLine(ray.origin, hit.point);
		}
		else
		{
			m_TargetPosition = Camera.main.ScreenToWorldPoint(mousePos);
		}
	}

	private void SetUpPath()
	{
		m_DistanceToTravel = m_TargetPosition - m_InitialPosition;
		m_TravelTimer.m_OnUpdate = Travel;
		m_TravelTimer.m_OnDone = Absorb;
		m_TravelTimer.Start(m_DistanceToTravel.magnitude / TRAVEL_BY_UNIT);
	}
	#endregion
}