// File: CustomPlayerController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomPlayerController : PlayerController {
	
	// const
	private const float CAMERA_DEPTH = 10f;
	// enum

	// public
	public BlackHole m_BlackHolePrefab;

	// protected
	protected BlackHole m_LeftBlackHole;
	protected BlackHole m_RightBlackHole;

	// private
	private List<BlackHole> m_BlackHolesProjectile = new List<BlackHole>();

	#region Unity API
	protected override void Update() 
	{
		base.Update();
		if (Input.GetMouseButtonDown(0) && m_LeftBlackHole == null)
		{
			m_LeftBlackHole = ShootBlackHole();
		}

		if (Input.GetMouseButtonDown(1) && m_RightBlackHole == null)
		{
			m_RightBlackHole = ShootBlackHole();
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual BlackHole ShootBlackHole()
	{
		BlackHole blackHole = (BlackHole)Instantiate(m_BlackHolePrefab);
		blackHole.gameObject.transform.position = transform.position;
		blackHole.gameObject.transform.parent = transform.parent;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = CAMERA_DEPTH;
		blackHole.TargetPosition = Camera.main.ScreenToWorldPoint(mousePos);
		blackHole.Shoot();

		return blackHole;
	}
	#endregion

	#region Private Methods
	#endregion
}