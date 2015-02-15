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
	private const string TERRAIN_CONTAINER = "TerrainContainer";

	// enum
	
	// public
	
	// protected
	[SerializeField]
	protected string m_BlockPath = "Prefabs/Terrain";
	[SerializeField]
	protected string m_PlayerPath = "Prefabs/Player";
	[SerializeField]
	protected string m_XMLPath = PlatformerConstants.RESOURCE_PATH + "XML/Stages.xml";

	protected GameObject m_TerrainContainer;
	protected GameObject m_Player;

	// private
	private StageCollection m_StageContainer;
	private int m_CurrentStageIndex = 0;
	private Stage m_CurrentStage;
	
	// properties
	#region Unity API
	protected virtual void Awake()
	{
		LoadFromXML();

		m_CurrentStage = m_StageContainer.m_Stages[m_CurrentStageIndex];

		LoadPlayer();
		LoadStageContainer();
		LoadStage();

		EventTriggerController.Instance.Initialize();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected virtual void LoadStageContainer()
	{
		m_TerrainContainer = new GameObject();
		m_TerrainContainer.name = TERRAIN_CONTAINER;
		m_TerrainContainer.transform.position = Vector3.zero;
		m_TerrainContainer.transform.parent = transform;
	}
	protected virtual void LoadStageByIndex(int stageNumber)
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
	protected virtual void LoadNextStage()
	{
		m_CurrentStageIndex++;
		if (m_CurrentStageIndex > m_StageContainer.m_Stages.Count - 1)
		{
			// Last Level Reached
		}
		else
		{
			ResetStage();
		}
	}
	protected virtual void LoadStage()
	{
		foreach(Block block in m_CurrentStage.m_Blocks)
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(m_BlockPath));
			Terrain t = obj.GetComponent<Terrain>();
			t.m_Type = (Terrain.eTerrainType)block.Type;
			obj.transform.parent = m_TerrainContainer.transform;
			obj.transform.position = block.Location;
			obj.transform.rotation = block.Rotation;
		}
	}
	protected virtual void LoadPlayer()
	{
		if (m_Player == null)
		{
			GameObject player = (GameObject)Instantiate(Resources.Load(m_PlayerPath));
			player.transform.parent = transform;
			m_Player = player;
		}
		m_Player.transform.position = m_CurrentStage.PlayerStartLocation();
	}
	protected virtual void DestroyStage()
	{
		foreach(Transform child in m_TerrainContainer.transform)
		{
			DestroyObject(child.gameObject);
		}
		DestroyObject(m_TerrainContainer);
	}
	protected virtual void ResetStage()
	{
		DestroyStage();
		LoadStage();
	}
	#endregion

	#region Private Methods
	private void LoadFromXML()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(StageCollection));
		FileStream stream = new FileStream(m_XMLPath, FileMode.Open);
		m_StageContainer = serializer.Deserialize(stream) as StageCollection;
		stream.Close();	
	}
	#endregion

	#region Observer Implementation
	#endregion
}