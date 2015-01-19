// File: Observer
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Observer : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected
	[SerializeField]
	protected List<Subject> m_Subject = new List<Subject>();
	// private

	#region Unity API
	protected virtual void Awake()
	{
		foreach(Subject subject in m_Subject)
		{
			subject.RegisterObserver(this);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region IObserver Implementation
	public virtual void OnNotify(Subject subject, Object args)
	{
		
	}
	#endregion
}