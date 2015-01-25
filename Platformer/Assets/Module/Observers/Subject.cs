// File: Subject
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour 
{
	
	// const

	// enum

	// public

	// protected

	// private
	private List<Observer> m_Observers = new List<Observer>();
	#region Unity API
	#endregion

	#region Public Methods
	public void RegisterObserver(Observer o)
	{
		m_Observers.Add(o);
	}
	#endregion

	#region Protected Methods
	protected void NotifyObervers(object obj = null)
	{
		foreach(Observer o in m_Observers)
		{
			o.OnNotify(this, obj);
		}
	}
	#endregion

	#region Private Methods
	#endregion
}