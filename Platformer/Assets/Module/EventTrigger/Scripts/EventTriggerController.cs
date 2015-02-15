// File: EventTriggerController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventTriggerController : MonoBehaviour {
	
	// const
	private const string OBJECT_NAME = "EventTriggerController";

	// enum

	// public

	// protected

	// private
	private static EventTriggerController m_Instance;
	private Dictionary<string, Event> m_EventList = new Dictionary<string, Event>();

	// properties
	public static EventTriggerController Instance
	{
		get
		{
			if (m_Instance == null)
			{
				GameObject obj = new GameObject(OBJECT_NAME);
				obj.AddComponent<EventTriggerController>();
				m_Instance = obj.GetComponent<EventTriggerController>();
			}
			return m_Instance;
		}
	}
	public static bool IsInstanceNull
	{
		get
		{
			return m_Instance == null;
		}
	}
	#region Unity API
	#endregion

	#region Public Methods
	public void Initialize()
	{
		ClearEventList();
		Event[] eventList = FindObjectsOfType<Event>();
		foreach(Event evt in eventList)
		{
			m_EventList.Add(evt.EventId, evt);
		}
	}
	public void TriggerEvent(Trigger trigger)
	{
		if (m_EventList.ContainsKey(trigger.EventId))
		{
			m_EventList[trigger.EventId].TriggerEvent();
		}
		else
		{
			Debug.LogError("The event key " + trigger.EventId + " does not exist in the event list.");
		}
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void ClearEventList()
	{
		m_EventList.Clear();
	}
	#endregion
}