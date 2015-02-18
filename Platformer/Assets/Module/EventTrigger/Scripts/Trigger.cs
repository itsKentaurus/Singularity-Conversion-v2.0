// File: Trigger
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : EventTrigger {
	
	// const

	// enum
	private enum eTriggerBy
	{
		ALL,
		PLAYER,
		ENEMY
	}

	private enum eTriggerType
	{
		ONE_TIME,
		ON_OFF
	}
	// public

	// protected

	// private
	[SerializeField]
	private eTriggerType m_Type = eTriggerType.ONE_TIME;
	[SerializeField]
	private eTriggerBy m_Users = eTriggerBy.ALL;
	private bool m_Used = false;

	#region Unity API
	#endregion

	#region Public Methods
	public bool CanTriggerAction(CharacterBase character)
	{
		switch(m_Users)
		{
		case eTriggerBy.ENEMY:
			return (character is Enemy);
		case eTriggerBy.PLAYER:
			return (character is Player);
		default:
			return true;
		}
	}
	public void ActivateTrigger()
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
		return (m_Type == eTriggerType.ONE_TIME && !m_Used);
	}

	private bool TriggerSwitch()
	{
		return (m_Type == eTriggerType.ON_OFF);
	}
	#endregion
}