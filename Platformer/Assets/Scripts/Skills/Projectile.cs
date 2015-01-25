// File: Projectile
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : Subject {
	
	// const
	public const string SELF_DESTRUCT = "SelfDestruct";
	public const float DISTANCE = 200f;
	// enum
	public enum ePorjectileType
	{
		Blackhole,
		Bullet
	}

	// public
	public ePorjectileType m_Type;

	// protected
	protected Timer m_TravelTimer = new Timer();

	// private

	#region Unity API
	protected virtual void OnTriggerEnter()
	{
		SelfDestruct();
	}
	protected virtual void OnDestroy()
	{
		m_TravelTimer.Stop();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void SelfDestruct()
	{
		NotifyObervers(SELF_DESTRUCT);
		Destroy(gameObject);
	}
	#endregion

	#region Private Methods
	#endregion
}