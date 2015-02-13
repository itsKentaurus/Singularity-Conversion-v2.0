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

		AquireTarget();

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
	private void AquireTarget()
	{
		for (int i = 0 ; i < 360 ; ++i)
		{
			Ray ray = new Ray(transform.position, new Vector3(Mathf.Cos(i), Mathf.Sin(i)));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, m_RangeOfVision, m_Targets))
			{
				Debug.DrawLine(ray.origin, hit.point);

				m_TargetLocated = true;

				m_TargetPosition = hit.transform.position;

				return;
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
//			m_Projectile.RegisterObserver(this);
			projectile.TargetPosition = m_TargetPosition;
			projectile.Shoot();
		}
		m_Loaded = false;
		m_RechargeTimer.Start();
	}
	#endregion
}