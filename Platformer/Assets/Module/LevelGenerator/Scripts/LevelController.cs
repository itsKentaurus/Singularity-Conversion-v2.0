// File: LevelController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LevelController : MonoBehaviour {
	
	// const
	private string LEVEL_XML_PATH = "XML/Levels";
	private string LEVEL_TAG= "Level";
	private string LEVEL_OFFSET_Y = "OffSetY";
	private string LEVEL_OFFSET_X = "OffSetX";
	private string BLOCK_TAG = "Block";
	private string BLOCK_ATTRIBUTE_UNIT = "Unit";
	private string GENERAL_ATTRIBUTE_X = "X";
	private string GENERAL_ATTRIBUTE_Y = "Y";

	// enum
	public enum eSpawnDirection
	{
		BOTTOM,
		LEFT,
		RIGHT,
		TOP
	}

	[System.Serializable]
	public struct Block
	{
		public string m_Name;
		public string m_Key;
		public GameObject m_Prefab;
	}
	
	[System.Serializable]
	public struct Enemy
	{
		public string m_Name;
		public string m_Key;
		public GameObject m_Prefab;
		public eSpawnDirection eDirection;
	}
	
	[System.Serializable]
	public struct EventTrigger
	{
		public string m_Name;
		public string m_EventId;
		public string m_TriggerKey;
		public string m_EventKey;
		public GameObject m_TriggerPrefab;
		public GameObject m_EventPrefab;
	}

	// public

	// protected
	[SerializeField]
	protected Block[] m_Blocks;
	[SerializeField]
	protected Enemy[] m_Enemies;
	[SerializeField]
	protected EventTrigger[] m_EventTriggers;
	[SerializeField]
	protected float m_BlockSize = 50f;

	// private
	private XmlDocument xmlDoc = new XmlDocument();
	private XmlNodeList m_Levels;
	private int m_CurrentLevel = 0;
	private float m_OffSetX = 0;
	private float m_OffSetY = 0;
//	private List<GameObject> m_

	#region Unity API
	protected virtual void Awake()
	{		 
		TextAsset temp = (TextAsset)Resources.Load(LEVEL_XML_PATH);
		xmlDoc.LoadXml(temp.text);

		Initialize();
		LoadLevel(0);
		LoadBlocks();
	}
	#endregion

	#region Public Methods
	public void Initialize()
	{
		if (m_Levels == null)
		{
			m_Levels = xmlDoc.GetElementsByTagName(LEVEL_TAG);
			Debug.Log("Level loaded");
		}
	}
	public void LoadLevel(int index)
	{
		m_CurrentLevel = index;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void LoadEnemies()
	{
	}
	
	private void LoadBlocks()
	{
		foreach (Block b in m_Blocks)
		{
			XmlNodeList blocks = m_Levels[m_CurrentLevel].SelectNodes(BLOCK_TAG + b.m_Key);
			m_OffSetY = float.Parse(m_Levels[m_CurrentLevel].Attributes[LEVEL_OFFSET_Y].Value);
			m_OffSetX = float.Parse(m_Levels[m_CurrentLevel].Attributes[LEVEL_OFFSET_X].Value);

			foreach (XmlNode block in blocks)
			{
				float x = float.Parse(block.Attributes[GENERAL_ATTRIBUTE_X].Value);
				float y = float.Parse(block.Attributes[GENERAL_ATTRIBUTE_Y].Value);
				float unit = float.Parse(block.Attributes[BLOCK_ATTRIBUTE_UNIT].Value);

				float yPosition = m_OffSetY - y;
				float xPosition = x - m_OffSetX;

				GameObject obj = (GameObject)Instantiate(b.m_Prefab);

				PositionObject(obj, b.m_Name, new Vector3(unit, 1f, 1f), new Vector3(xPosition, yPosition, 0));
			}
		}

	}
	
	private void LoadEventTriggers()
	{
		
	}

	private void PositionObject(GameObject obj, string name, Vector3 scale, Vector3 position)
	{
		obj.name = name;
		obj.transform.localScale = scale * m_BlockSize;
		obj.transform.position = position * m_BlockSize;
		obj.transform.parent = this.transform;
	}
	#endregion
}