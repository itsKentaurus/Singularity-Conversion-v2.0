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
	[System.Serializable]
	public struct Player
	{
		public string m_Key;
		public GameObject m_Prefab;
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
	}
	
	[System.Serializable]
	public struct Trigger
	{
		public string m_Name;
		public string m_EventId;
		public string m_TriggerKey;
		public GameObject m_EventPrefab;
	}

	[System.Serializable]
	public struct Event
	{
		public string m_Name;
		public string m_EventId;
		public string m_EventKey;
		public GameObject m_EventPrefab;
	}
	

	// public

	// protected
	[SerializeField]
	protected float m_BlockSize = 100f;
	[SerializeField]
	protected Block[] m_Blocks;
	[SerializeField]
	protected Enemy[] m_Enemies;
	[SerializeField]
	protected Trigger[] m_Triggers;
	[SerializeField]
	protected Event[] m_Events;
	[SerializeField]
	protected Player m_Player;

	// private
	private XmlDocument xmlDoc = new XmlDocument();
	private XmlNodeList m_Levels;
	private int m_CurrentLevel = 0;
	private float m_OffSetX = 0;
	private float m_OffSetY = 0;
	private List<GameObject> m_Objects = new List<GameObject>();

	// properties
	public Vector3 OffSet
	{
		get { return (new Vector3(m_OffSetX, m_OffSetY) - new Vector3(8f, 4.5f)) * m_BlockSize; }
	}
	#region Unity API
	protected void OnDestroy()
	{
		foreach(GameObject obj in m_Objects)
		{
			Destroy(obj);
		}
	}

	// properties
	#endregion

	#region Public Methods
	public void Initialize()
	{
		TextAsset temp = (TextAsset)Resources.Load(LEVEL_XML_PATH);
		xmlDoc.LoadXml(temp.text);
		m_Levels = xmlDoc.GetElementsByTagName(LEVEL_TAG);
	}

	public void LoadLevel(int index)
	{
		m_CurrentLevel = index;

		ClearLevel();

		LoadObjects();
	}

	public void ClearLevel()
	{
		foreach(GameObject obj in m_Objects)
		{
			Destroy(obj);
		}
	}

	public void RestartLevel()
	{
		ClearLevel();

		LoadObjects();
	}

	public void NextLevel()
	{
		LoadLevel(++m_CurrentLevel);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void LoadObjects()
	{
		LoadOffSet();
		
		LoadBlocks();
		
		LoadEnemies();
		
		LoadEventTriggers();
		
		LoadPlayer();
	}

	private void LoadOffSet()
	{
		m_OffSetY = float.Parse(m_Levels[m_CurrentLevel].Attributes[LEVEL_OFFSET_Y].Value);
		m_OffSetX = float.Parse(m_Levels[m_CurrentLevel].Attributes[LEVEL_OFFSET_X].Value);
	}

	private void LoadEnemies()
	{
		foreach (Enemy e in m_Enemies)
		{
			XmlNodeList enemies = m_Levels[m_CurrentLevel].SelectNodes(BLOCK_TAG + e.m_Key);

			foreach (XmlNode enemy in enemies)
			{
				float x = float.Parse(enemy.Attributes[GENERAL_ATTRIBUTE_X].Value);
				float y = float.Parse(enemy.Attributes[GENERAL_ATTRIBUTE_Y].Value);

				float yPosition = m_OffSetY - y;
				float xPosition = x - m_OffSetX;
				
				GameObject obj = (GameObject)Instantiate(e.m_Prefab);
				
				PositionObject(obj, e.m_Name, Vector3.one, new Vector3(xPosition, yPosition, 0));
			}
		}
	}
	
	private void LoadBlocks()
	{
		foreach (Block b in m_Blocks)
		{
			XmlNodeList blocks = m_Levels[m_CurrentLevel].SelectNodes(BLOCK_TAG + b.m_Key);

			foreach (XmlNode block in blocks)
			{
				float x = float.Parse(block.Attributes[GENERAL_ATTRIBUTE_X].Value);
				float y = float.Parse(block.Attributes[GENERAL_ATTRIBUTE_Y].Value);
				float unit = float.Parse(block.Attributes[BLOCK_ATTRIBUTE_UNIT].Value);

				float yPosition = m_OffSetY - y;
				float xPosition = -(m_OffSetX - x);

				GameObject obj = (GameObject)Instantiate(b.m_Prefab);

				PositionObject(obj, b.m_Name, new Vector3(unit, 1f, 1f), new Vector3(xPosition, yPosition, 0));
			}
		}
	}
	
	private void LoadEventTriggers()
	{

	}

	private void LoadPlayer()
	{
		XmlNodeList player = m_Levels[m_CurrentLevel].SelectNodes(BLOCK_TAG + m_Player.m_Key);
		if (player.Count == 1)
		{
			float x = float.Parse(player[0].Attributes[GENERAL_ATTRIBUTE_X].Value);
			float y = float.Parse(player[0].Attributes[GENERAL_ATTRIBUTE_Y].Value);

			float yPosition = m_OffSetY - y;
			float xPosition = -(m_OffSetX - x);

			PositionObject((GameObject)Instantiate(m_Player.m_Prefab), "Player", Vector3.one, new Vector3(xPosition, yPosition, 0));
		}
	}

	private void PositionObject(GameObject obj, string name, Vector3 scale, Vector3 position)
	{
		obj.name = name;
		obj.transform.localScale = scale * m_BlockSize;
		obj.transform.position = position * m_BlockSize;
		obj.transform.parent = this.transform;

		m_Objects.Add(obj);
	}
	#endregion
}