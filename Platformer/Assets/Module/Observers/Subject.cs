// File: Subject
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : Observer
{
	[SerializeField]
	private List<Observer> m_Observers = new List<Observer>();

	public void RegisterObserver(Observer observer)
	{
		m_Observers.Add(observer);
	}
	
	public void UnRegisterObserver(Observer observer)
	{
		m_Observers.Remove(observer);
	}
	
	protected void NotifyObservers(object obj = null)
	{
		foreach (Observer o in m_Observers)
		{
			o.OnNotify(this, obj);
		}
	}

	public override void OnNotify(Subject subject, object obj)
	{

	}
}