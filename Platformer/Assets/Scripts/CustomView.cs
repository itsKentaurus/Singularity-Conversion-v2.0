// File: CustomView
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomView : View {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	public void Start()
	{
		FlowManager.Instance.TriggerAction("Scene2");
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}