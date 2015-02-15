// File: EventTriggerEnum
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;

public class EventTriggerEnum : MonoBehaviour {
	public enum eTriggerType
	{
		ONE_TIME,
		ON_OFF
	}

	public enum eTriggerBy
	{
		ALL,
		PLAYER,
		ENEMY
	}
}