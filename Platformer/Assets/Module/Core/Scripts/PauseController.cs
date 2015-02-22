// File: PauseController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseController : MonoBehaviour {
	
	// const
	private const string OBJECT_NAME = "PauseController";

	// enum

	// public

	// protected

	// private
	private static PauseController m_Instance = null;
	private bool m_IsPause = false;

	//properties
	public static PauseController Instance
	{
		get
		{
			if (m_Instance == null)
			{
				GameObject obj = new GameObject(OBJECT_NAME);
				obj.AddComponent<PauseController>();
				m_Instance = obj.GetComponent<PauseController>();
			}
			return m_Instance;
		}
	}

	public bool IsPaused
	{
		get
		{
			return m_IsPause;
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
		// Don't need code here just make sure we can call this in came
	}

	public void Pause()
	{
		m_IsPause = true;
	}

	public void Resume()
	{
		m_IsPause = false;
	}
	#endregion

	#region Protected Methods
	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			m_IsPause = !m_IsPause;
		}
	}
	#endregion

	#region Private Methods
	#endregion
}