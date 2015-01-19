// File: MainMenuView
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuView : View {
	
	// const

	// enum

	// public
	public UIButton m_Button;

	// protected

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region IObservers Implementation
	public override void OnNotify(Subject subject, Object args)
	{
		base.OnNotify(subject, args);

		if (m_Button.IsFired(subject, args))
		{
			Debug.Log("HERE");
		}
	}
	#endregion
}