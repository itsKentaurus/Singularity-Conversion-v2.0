// File: Observer
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Observer : MonoBehaviour, IObserver {
	
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
		RegisterSubjects();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region IObserver Implementation
	public void RegisterSubjects()
	{
		foreach(ISubject subject in m_Subject)
		{
			subject.RegisterObserver(this);
		}
	}
	public virtual void OnNotify(ISubject subject, object args)
	{
		
	}
	#endregion
}