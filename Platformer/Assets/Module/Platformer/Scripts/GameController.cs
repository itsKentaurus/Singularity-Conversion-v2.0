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
	[SerializeField]
	private PlatformerCamera m_PlatformerCamera;

	// properties
	#region Unity API
	protected virtual void Awake()
	{
		PauseManager.Instance.Pause();
		EventTriggerManager.Instance.Initialize();
		m_LevelController.Initialize();
		m_LevelController.LoadLevel(0);
		PlatformerCamera camera = (PlatformerCamera)Instantiate(m_PlatformerCamera);
		camera.Offset = m_LevelController.OffSet;
		PauseManager.Instance.Resume();
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