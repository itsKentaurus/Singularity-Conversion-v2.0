// File: UIButton.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIButton : Subject, ITouchListener
{
	
	// const

	// enum

	// public
	public bool m_SwallowTouches = true;
	// protected

	// private

	// properties

	#region Unity API
	#endregion

	#region Public Methods
	public bool IsFired(ISubject subject, object args)
	{
		return (subject == this);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region IObservable Implementation
	public virtual bool OnTouchBegin(TouchEvent evt)
	{
		Debug.Log("I need an adult D=");
		return m_SwallowTouches;
	}

	public virtual void OnTouchMove(TouchEvent evt)
	{
		Debug.Log("Staph D=");
	}

	public virtual void OnTouchEnd(TouchEvent evt)
	{
		Debug.Log("It ended =D");
		NotifyObervers();
	}
	#endregion
}