// File: View
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class View : Observer {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	protected override void Awake()
	{
		base.Awake();
		if (PauseController.Instance != null)
		{
			PauseController.Instance.IsPaused = false;
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