// File: GameController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameController : Subject {
	
	// const
	private const float PLAYER_DEPTH = 0f;

	// enum
	
	// public
	
	// protected

	// private
	[SerializeField]
	private LevelController m_LevelController;

	// properties
	#region Unity API
	protected virtual void Awake()
	{
		PauseController.Instance.Pause();
		EventTriggerController.Instance.Initialize();
		m_LevelController.Initialize();
		m_LevelController.LoadLevel(0);
		PauseController.Instance.Resume();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region Observer Implementation
	#endregion
}