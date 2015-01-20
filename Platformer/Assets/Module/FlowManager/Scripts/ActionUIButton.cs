// File: ActionUIButton
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionUIButton : UIButton {
	
	// const

	// enum

	// public
	public string m_GoToScene;
	// protected

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected override void OnMouseUp()
	{
		base.OnMouseUp();

		if (FlowManager.Instance != null)
		{
			if (m_GoToScene != "")
			{
				FlowManager.Instance.TriggerAction(m_GoToScene);
			}
			else
			{
				Debug.LogError("ActionUIButton: No scene was set.");
			}
		}
	}
	#endregion

	#region Private Methods
	#endregion
}