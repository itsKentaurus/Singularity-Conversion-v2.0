// File: Trigger
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : EventTrigger {
	
	// const

	// enum

	// public

	// protected
	[SerializeField]
	protected EventTriggerEnum.eTriggerType m_Type = EventTriggerEnum.eTriggerType.ONE_TIME;
	[SerializeField]
	protected EventTriggerEnum.eTriggerBy m_Users = EventTriggerEnum.eTriggerBy.ALL;

	// private
	private bool m_Used = false;

	#region Unity API
	#endregion

	#region Public Methods
	public virtual bool CanTriggerAction(CharacterBase character)
	{
		switch(m_Users)
		{
		case EventTriggerEnum.eTriggerBy.ENEMY:
			return (character is Enemy);
		case EventTriggerEnum.eTriggerBy.PLAYER:
			return (character is PlayerController);
		default:
			return true;
		}
	}
	public virtual void ActivateTrigger()
	{
		if (TriggerSwitch() || OneTimeUse())
		{
			m_Used = true;
			EventTriggerController.Instance.TriggerEvent(this);
		}
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private bool OneTimeUse()
	{
		return (m_Type == EventTriggerEnum.eTriggerType.ONE_TIME && !m_Used);
	}

	private bool TriggerSwitch()
	{
		return (m_Type == EventTriggerEnum.eTriggerType.ON_OFF);
	}
	#endregion
}