// File: StageController.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class StageController : MonoBehaviour {
	
	// const
	private const string PATH = "Assets/Resources/XML/Stages.xml";
	private const string BLOCK_PATH = "Prefabs/Terrain";
	private const string PLAYER_PLATH = "Prefabs/Player";

	// enum

	// public

	// protected

	// private
	private StageContainer m_StageContainer;
	private GameObject m_Player;
	private int m_CurrentStageIndex = 0;
	private Stage m_CurrentStage;

	#region Unity API
	protected virtual void Start()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(StageContainer));
		FileStream stream = new FileStream(PATH, FileMode.Open);
		m_StageContainer = serializer.Deserialize(stream) as StageContainer;
		stream.Close();	

	}
	protected virtual void RemoveLevel()
	{
		foreach(Transform child in transform)
		{
			DestroyObject(child.gameObject);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	protected void LoadStage(int stageNumber)
	{
		m_CurrentStageIndex = stageNumber;
		m_CurrentStage = m_StageContainer.m_Stages[m_CurrentStageIndex];
		foreach(Block block in m_CurrentStage.m_Blocks)
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(BLOCK_PATH));
			Terrain t = obj.GetComponent<Terrain>();
			t.m_Type = (Terrain.eTerrainType)block.Type;
			obj.transform.parent = transform;
			obj.transform.position = new Vector3(block.X, block.Y, 0f);
		}

	}
	protected void LoadPlayer()
	{
		if (m_Player != null)
		{
			GameObject player = (GameObject)Instantiate(Resources.Load(PLAYER_PLATH));
			m_Player = player;
		}
		m_Player.transform.position = m_CurrentStage.PlayerStartLocation();
	}
	#endregion

	#region Private Methods
	#endregion
}