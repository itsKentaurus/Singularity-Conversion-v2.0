// File: StageController.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class StageController : Subject {
	
	// const
	private const string PATH = "Assets/Resources/XML/Stages.xml";
	private const string BLOCK_PATH = "Prefabs/Terrain";
	private const string PLAYER_PLATH = "Prefabs/Player";
	private const float PLAYER_DEPTH = 0f;

	// enum

	// public

	// protected

	// private
	private StageCollection m_StageContainer;
	private GameObject m_Player;
	private int m_CurrentStageIndex = 0;
	private Stage m_CurrentStage;

	// properties
	

	#region Unity API
	protected virtual void Start()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(StageCollection));
		FileStream stream = new FileStream(PATH, FileMode.Open);
		m_StageContainer = serializer.Deserialize(stream) as StageCollection;
		stream.Close();	
		LoadStage();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected void LoadStageByIndex(int stageNumber)
	{
		if (m_StageContainer.m_Stages.Count > stageNumber)
		{
			m_CurrentStageIndex = stageNumber;
		}
		else
		{
			Debug.LogError("StageController: Stage number exceeds the number of stage created.");
		}
	}
	protected void LoadNextStage()
	{
		m_CurrentStageIndex++;
		if (m_CurrentStageIndex > m_StageContainer.m_Stages.Count - 1)
		{
			// End of Game
		}
		else
		{
			ResetStage();
		}
	}
	protected void LoadStage()
	{
		m_CurrentStage = m_StageContainer.m_Stages[m_CurrentStageIndex];
		foreach(Block block in m_CurrentStage.m_Blocks)
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(BLOCK_PATH));
			Terrain t = obj.GetComponent<Terrain>();
			t.m_Type = (Terrain.eTerrainType)block.Type;
			obj.transform.parent = transform;
			obj.transform.position = new Vector3(block.X, block.Y, PLAYER_DEPTH);
		}
		LoadPlayer();
	}
	protected void LoadPlayer()
	{
		if (m_Player == null)
		{
			GameObject player = (GameObject)Instantiate(Resources.Load(PLAYER_PLATH));
			m_Player = player;
			NotifyObservers(GameConstants.PLAYER_LOADED);
		}
		m_Player.transform.position = m_CurrentStage.PlayerStartLocation();
	}
	protected void DestroyStage()
	{
		foreach(Transform child in transform)
		{
			DestroyObject(child.gameObject);
		}
	}
	protected void ResetStage()
	{
		DestroyStage();
		LoadStage();
	}
	#endregion

	#region Private Methods
	#endregion
}