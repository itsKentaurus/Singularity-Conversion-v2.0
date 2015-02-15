// File: FlowManager
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class FlowManager : MonoBehaviour {

	// const
	private const string OBJECT_NAME = "FlowMananger";
	private const string VIEW_TAG = "View";
	private const string FLOW_XML_PATH = "XML/Flow";
	private const string VIEW_NAME = "name";
	private const string SCENE_NAME = "scene";
	private const string ACTION = "action";

	// private 
	private static FlowManager m_Instance;
	private View m_CurrentScene;
	private GameObject m_MainCamera;
	Dictionary<string, Dictionary<string, string>> m_Views = new Dictionary<string, Dictionary<string, string>>();
	// properties
	public static FlowManager Instance
	{
		get
		{
			if (m_Instance == null)
			{
				GameObject obj = new GameObject(OBJECT_NAME);
				obj.AddComponent<FlowManager>();
				m_Instance = obj.GetComponent<FlowManager>();
			}
			return m_Instance;
		}
	}

	public void Initialize()
	{
		LoadViews();
	}

	public void TriggerPopUp(string scene)
	{
		Application.LoadLevelAdditive(scene);
	}

	public void TriggerAction(string scene)
	{
		Application.LoadLevelAdditive(scene);
		if (m_CurrentScene != null)
		{
			m_CurrentScene.gameObject.SetActive(false);
		}
		StartCoroutine(AssignParent());
	}

	private IEnumerator AssignParent()
	{
		yield return 0;

		View view = GameObject.FindObjectOfType<View>();

		if (view != null)
		{
			view.gameObject.transform.parent = transform;
		}
		else
		{
			Debug.LogError("FlowManager: Scene is null.");
			return false;
		}
		if (m_CurrentScene != null)
		{
			DestroyObject(m_CurrentScene.gameObject);
			m_CurrentScene = null;
		}
		m_CurrentScene = view;
		
	}

	private void LoadViews()
	{
		XmlDocument xmlDoc = new XmlDocument();
		TextAsset temp = (TextAsset)Resources.Load(FLOW_XML_PATH);
		xmlDoc.LoadXml(temp.text);
		XmlNodeList views = xmlDoc.GetElementsByTagName(VIEW_TAG);
	
		foreach (XmlNode viewInfo in views)
		{
			Dictionary<string, string> obj = new Dictionary<string,string>();
			string action = viewInfo.Attributes[ACTION].Value;

			string name = viewInfo.Attributes[VIEW_NAME].Value;
			obj.Add(VIEW_NAME, name);
			string scene = viewInfo.Attributes[SCENE_NAME].Value;
			obj.Add(SCENE_NAME, scene);

			m_Views.Add(action, obj);
		}
	}
}