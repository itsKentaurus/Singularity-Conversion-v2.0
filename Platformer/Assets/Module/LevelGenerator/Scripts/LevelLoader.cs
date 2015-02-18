// File: LevelLoader
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {
	
	// const

	// enum
	public enum eSpawnDirection
	{
		BOTTOM,
		LEFT,
		RIGHT,
		TOP
	}

	// public
	[System.Serializable]
	public struct Blocks
	{
		public string m_Name;
		public string m_Key;
		public GameObject m_Prefab;
	}

	[System.Serializable]
	public struct Enemies
	{
		public string m_Name;
		public string m_Key;
		public GameObject m_Prefab;
		public eSpawnDirection eDirection;
	}

	[System.Serializable]
	public struct EventTriggers
	{
		public string m_Name;
		public string m_EventId;
		public string m_TriggerKey;
		public string m_EventKey;
		public GameObject m_TriggerPrefab;
		public GameObject m_EventPrefab;
	}

	// protected
	[SerializeField]
	protected Blocks[] m_Blocks;
	[SerializeField]
	protected Enemies[] m_Enemies;
	[SerializeField]
	protected EventTriggers[] m_EventTriggers;

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}