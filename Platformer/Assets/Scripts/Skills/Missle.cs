// File: Missle
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Missle : Projectile {
	
	// const
	private const float FIELD_OF_VISION = 45f;

	// enum

	// public
	public float m_RangeOfVision = 200f;

	// protected
	[SerializeField]
	protected LayerMask m_Targets;

	// private
	private Collider[] m_Colliders;

	#region Unity API
	protected override void Update()
	{
		FindTargets();
		AcquireTarget();
		base.Update();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected override void UpdatePathVector ()
	{
		base.UpdatePathVector ();
		//TODO Rigil: Need to slow down the turning point
	}
	#endregion

	#region Private Methods
	private void FindTargets()
	{
		m_Colliders = Physics.OverlapSphere(transform.position, m_RangeOfVision);
	}
	private void AcquireTarget()
	{
		foreach (Collider c in m_Colliders)
		{
			GameObject obj = c.gameObject;
			if (obj.GetComponent<PlayerController>() != null && IsInFieldOfVision())
			{
				Ray ray = new Ray(transform.position, obj.transform.position);
				if (Physics.Raycast(ray, m_Targets))
				{
					m_TargetPosition = obj.transform.position;
					UpdatePathVector();
					return;
				}
			}
		}
	}

	private bool IsInFieldOfVision()
	{
		return Vector3.Angle(m_PathVector, m_TargetPosition - transform.position) < (FIELD_OF_VISION / 2f);
	}
	#endregion
}