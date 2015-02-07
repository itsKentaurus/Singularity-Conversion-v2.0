// File: CustomPlayerController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomPlayerController : PlayerController {
	
	// const
	public const string LEFT_CLICK = "LeftClick";

	// enum

	// public
	public Skills m_Skills;

	// protected

	// private

	#region Unity API
	protected override void Update() 
	{
		base.Update();

		if (m_Skills != null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				m_Skills.ShootBlackHole(gameObject);
			}
		}

	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}