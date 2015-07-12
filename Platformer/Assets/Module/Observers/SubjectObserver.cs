// File: SubjectObserver.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubjectObserver : MonoBehaviour, ISubject, IObserver
{
	[SerializeField]
	private List<Subject> m_Subject = new List<Subject>();
	private List<IObserver> m_Observers = new List<IObserver>();

	protected virtual void Awake()
	{
		RegisterSubjects();
	}

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

	public void RegisterObserver(IObserver o)
	{
		m_Observers.Add(o);
	}
	public void NotifyObervers(object obj = null)
	{
		foreach(Observer o in m_Observers)
		{
			o.OnNotify(this, obj);
		}	
	}
}