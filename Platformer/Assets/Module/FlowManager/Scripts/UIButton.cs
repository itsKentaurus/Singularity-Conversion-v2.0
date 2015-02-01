// File: UIButton
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIButton : Subject {
	
	// const

	// enum

	// public
	public Texture2D m_OnButtonUp;
	public Texture2D m_OnButtonDown;

	// protected

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void OnMouseHover()
	{
	}

	protected virtual void OnMouseDown()
	{
		if (m_OnButtonDown != null)
		{
			gameObject.renderer.material.mainTexture = m_OnButtonDown;
		}
	}

	protected virtual void OnMouseUp()
	{
		if (m_OnButtonUp != null)
		{
			gameObject.renderer.material.mainTexture = m_OnButtonUp;
		}
		NotifyObervers();
	}

	public virtual bool IsFired(ISubject subject, object args)
	{
		return (subject is UIButton && (UIButton)subject == this);
	}
	#endregion

	#region Private Methods
	#endregion
}