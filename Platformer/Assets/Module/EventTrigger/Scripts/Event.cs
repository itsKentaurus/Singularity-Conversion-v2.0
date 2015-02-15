// File: Event
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Event : EventTrigger {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	public virtual void TriggerEvent()
	{
		Debug.Log("Event Started");
	}
	#endregion

	#region Private Methods
	#endregion
}