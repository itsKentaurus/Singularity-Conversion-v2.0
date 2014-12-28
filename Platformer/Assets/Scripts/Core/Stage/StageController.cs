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

	#region Unity API
	protected virtual void Start()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(StageContainer));
		FileStream stream = new FileStream(PATH, FileMode.Open);
		StageContainer container = serializer.Deserialize(stream) as StageContainer;
		stream.Close();	

		foreach(Stage stage in container.m_Stages)
		{
			foreach(Block block in stage.m_Blocks)
			{
				GameObject obj = (GameObject)Instantiate(Resources.Load(BLOCK_PATH));
				Terrain t = obj.GetComponent<Terrain>();
				t.m_Type = (Terrain.eTerrainType)block.Type;
				obj.transform.parent = transform;
				obj.transform.position = new Vector3(block.X, block.Y, 0f);
			}
		}
		GameObject player = (GameObject)Instantiate(Resources.Load(PLAYER_PLATH));
		player.transform.position = new Vector3(container.m_Stages[0].m_Player.X, container.m_Stages[0].m_Player.Y, 0f);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}