// File: CharacterBase
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : Subject {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	protected virtual void Update()
	{
		if (PauseController.Instance != null &&PauseController.Instance.IsPaused)
		{
			return;
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