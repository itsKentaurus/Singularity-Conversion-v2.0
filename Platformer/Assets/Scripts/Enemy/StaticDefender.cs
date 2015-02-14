// File: StaticDefender
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticDefender : Enemy {
	
	// const

	// enum

	// public
	public float m_RangeOfVision = 200f;

	// protected
	[SerializeField]
	protected GameObject m_Projectile;
	[SerializeField]
	protected float m_RechargeTime = 0.5f;

	// private
	private Timer m_RechargeTimer;
	private bool m_Loaded = false;
	private bool m_TargetLocated = false;
	private Vector3 m_TargetPosition;
	private Collider[] m_Colliders;

	#region Unity API
	protected override void Start()
	{
		base.Start();
		m_RechargeTimer = new Timer(m_RechargeTime);
		m_RechargeTimer.m_OnDone += Reload;
		m_Loaded = true;
	}

	protected override void Update()
	{
		base.Update();
		if (m_RechargeTimer != null)
		{
			m_RechargeTimer.Update();
		}

		FindTargets();
		AcquireTarget();

		if (m_Loaded && m_TargetLocated)
		{
			Shoot();
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods

	#endregion

	#region Private Methods
	private void Reload()
	{
		m_Loaded = true;
	}
	private void FindTargets()
	{
		m_Colliders = Physics.OverlapSphere(transform.position, m_RangeOfVision);
	}
	private void AcquireTarget()
	{
		foreach (Collider c in m_Colliders)
		{
			GameObject obj = c.gameObject;
			if (obj.GetComponent<PlayerController>() != null)
			{
				m_TargetPosition = obj.transform.position;
				Ray ray = new Ray(transform.position, m_TargetPosition);
				if (Physics.Raycast(ray, m_Targets))
				{
					m_TargetLocated = true;
					return;
				}
			}
		}
		m_TargetLocated = false;
	}

	private void Shoot()
	{
		if (m_Projectile != null)
		{
			GameObject obj = (GameObject)Instantiate(m_Projectile);
			obj.transform.position = this.transform.position;
			obj.transform.parent = this.transform.parent;
			Projectile projectile = obj.GetComponent<Projectile>();
			projectile.TargetPosition = m_TargetPosition;
			projectile.Shoot();
		}
		m_Loaded = false;
		m_RechargeTimer.Start();
	}
	#endregion


//	// Get all the objects in range
//	Collider[] cols = Physics.OverlapSphere(transform.position, range);
//	// Assign the first object as the nearest for now
//	Collider nearest = cols[0];
//	
//	// Loop through objects
//	foreach (Collider c in cols)
//	{
//		// Get the current objects rigidbody
//		Rigidbody rb = c.attachedRigidbody;
//		// Get the force between the objects
//		Vector3 offset = transform.position - c.transform.position;
//		// Get the square of the force (distance)
//		float sqmag = offset.sqrMagnitude;
//		
//		// if this object is closer
//		if (rigidbody != rb && sqmag < nearest.transform.position.sqrMagnitude && rb != rigidbody && rb.tag != "Player")
//		{
//			nearest = c; //assign this as the new nearest
//		}
//	}
//	// double check that we are not refering to self
//	if (nearest.rigidbody != rigidbody)
//	{
//		//get the force of the nearest object
//		Vector3 force = transform.position - nearest.transform.position;
//		// Normalize the force
//		force.Normalize();
//		// Apply the force
//		rigidbody.AddForce(GravityConstant * force * rigidbody.mass * nearest.rigidbody.mass / force.sqrMagnitude);
//	}
}