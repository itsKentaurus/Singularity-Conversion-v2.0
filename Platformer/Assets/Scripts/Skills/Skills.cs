// File: Skills
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skills : Observer {
	
	// const

	// enum

	// public
	public GameObject m_PrimarySkill;

	// protected

	// private
	private int m_MaxBlackHoleCount = 2;
	private int m_BlackHoleCount = 0;
	#region Unity API
	protected void Update()
	{
		Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	public virtual void ShootBlackHole(GameObject subject)
	{
		if (m_BlackHoleCount < m_MaxBlackHoleCount)
		{
			m_BlackHoleCount++;
			GameObject obj = (GameObject)Instantiate(m_PrimarySkill);
			obj.transform.position = subject.transform.position;
			obj.transform.parent = subject.transform.parent;
			BlackHole blackHole = obj.GetComponent<BlackHole>();
			blackHole.RegisterObserver(this);
			blackHole.Shoot();
		}
	}
	#endregion

	#region Private Methods
	#endregion
	#region IObserver Implementation
	public override void OnNotify(ISubject subject, object args)
	{
		base.OnNotify(subject, args);
		if (subject is Projectile && args is string)
		{
			if (args.ToString() == Projectile.SELF_DESTRUCT)
			{
				Projectile proj = subject as Projectile;
				switch(proj.m_Type)
				{
				case Projectile.ePorjectileType.Blackhole:
					m_BlackHoleCount--;
					break;
				}
			}
		}
	}
	#endregion
}