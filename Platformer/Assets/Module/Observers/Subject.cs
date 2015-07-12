// File: Subject
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour, ISubject
{
	
	// const

	// enum

	// public

	// protected

	// private
	private List<IObserver> m_Observers = new List<IObserver>();
	#region Unity API
	#endregion

	#region Public Methods
	public void RegisterObserver(IObserver o)
	{
		m_Observers.Add(o);
	}
	#endregion

	#region Protected Methods
	public void NotifyObervers(object obj = null)
	{
		foreach(IObserver o in m_Observers)
		{
			o.OnNotify(this, obj);
		}
	}
	#endregion

	#region Private Methods
	#endregion
}