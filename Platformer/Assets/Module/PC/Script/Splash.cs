// File: Splash
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Splash : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	protected void Update()
	{
		CheckKeys();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void CheckKeys()
	{
		if (Input.anyKey)
		{
			FlowManager.Instance.TriggerAction(GameConstants.GO_TO_MAIN_MENU);
		}
	}
	#endregion

	#region Private Methods
	#endregion
}