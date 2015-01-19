// File: PauseController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseController : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected

	// private
	private static PauseController m_Instance;
	private bool m_IsPause = false;

	//properties
	public static PauseController Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(m_Instance == null)
			{
				m_Instance = GameObject.FindObjectOfType<PauseController>();
			}
			return m_Instance;
		}
	}

	public bool IsPaused
	{
		set
		{
			m_IsPause = value;
		}
		get
		{
			return m_IsPause;
		}
	}
	#region Unity API
	#endregion

	#region Public Methods
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