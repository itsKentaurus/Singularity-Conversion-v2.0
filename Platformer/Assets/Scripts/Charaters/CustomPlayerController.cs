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

	// protected

	// private

	#region Unity API
	protected override void Update() 
	{
		base.Update();
		if (Input.GetMouseButtonDown(0))
		{
			NotifyObervers(LEFT_CLICK);
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