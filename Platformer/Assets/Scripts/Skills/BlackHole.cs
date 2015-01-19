// File: BlackHole
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : MonoBehaviour {
	
	// const
	private const float TRAVEL_BY_UNIT = 0.25f;
	private const float ABSORB_TIME = 2f;
	private const float STARTING_SCALE = 0.15f;
	private const float TO_SCALE_UP = 1f - STARTING_SCALE;

	// enum

	// public

	// protected

	// private
	private Vector3 m_TargetPosition;
	private Vector3 m_DistanceToTravel;
	private Vector3 m_InitialPosition;
	private Timer m_TravelTimer = new Timer();
	private Timer m_AbsorbTimer = new Timer(ABSORB_TIME);

	// properties
	public Vector3 TargetPosition
	{
		set
		{
			m_TargetPosition = value;
		}
	}
	#region Unity API
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
	protected void OnDestroy()
	{
		m_TravelTimer.Stop();
		m_AbsorbTimer.Stop();
		m_TravelTimer.m_OnUpdate -= Travel;
		m_TravelTimer.m_OnDone -= Absorb;
		m_AbsorbTimer.m_OnUpdate -= Pull;
		m_AbsorbTimer.m_OnDone -= SelfDestruct;
	}
	#endregion

	#region Public Methods
	public void Shoot()
	{
		m_InitialPosition = transform.position;
		m_DistanceToTravel = m_TargetPosition - m_InitialPosition;
		m_TravelTimer.m_OnUpdate += Travel;
		m_TravelTimer.m_OnDone += Absorb;
		m_TravelTimer.Start(TRAVEL_BY_UNIT * m_DistanceToTravel.magnitude);
	}
	#endregion

	#region Protected Methods
	protected void Travel()
	{
		transform.position = m_InitialPosition + m_DistanceToTravel * m_TravelTimer.Ratio;
		transform.localScale = Vector3.one * STARTING_SCALE + Vector3.one * TO_SCALE_UP * m_TravelTimer.Ratio;
	}
	protected void Absorb()
	{
		m_AbsorbTimer.m_OnUpdate += Pull;
		m_AbsorbTimer.m_OnDone += SelfDestruct;
		m_AbsorbTimer.Start();
	}

	protected void Pull()
	{
		// TODO Rigil: Detect all character in range and pull towards
	}

	protected void SelfDestruct()
	{
		Destroy(gameObject);
	}
	#endregion

	#region Private Methods
	#endregion
}