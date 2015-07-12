// File: FlowManager.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class FlowManager : MonoBehaviour 
{
	private static FlowManager m_Instance = null;
	private const string VIEW = "view";
	private const string NAME = "name";
	private const string ACTION = "action";
	private XmlDocument xmlDoc = new XmlDocument();
	private Dictionary<string, Hashtable> m_Actions = new Dictionary<string, Hashtable>();
	private Dictionary<string, Hashtable> m_Views = new Dictionary<string, Hashtable>();
	private string m_PreviousScene = string.Empty;
	private string m_CurrentScene = string.Empty;

	public static bool IsInstanceNull
	{
		get { return m_Instance == null; }
	}

	public string CurrentScene
	{
		get { return m_CurrentScene; }
	}

	public string PreviousScene
	{
		get { return m_PreviousScene; }
	}

	public static FlowManager Instance
	{
		get
		{
			if (IsInstanceNull)
			{
				GameObject obj = new GameObject("FlowManager");
				m_Instance = obj.AddComponent<FlowManager>();
			}

			return m_Instance;
		}
	}

	public void Launch(string action, Dictionary<string, Object> parameters = null)
	{
		Destroy(GameObject.Find("Launcher"));

		TextAsset temp = (TextAsset)Resources.Load("XML/Flow");
		xmlDoc.LoadXml(temp.text);
		foreach (XmlNode node in xmlDoc.GetElementsByTagName(ACTION))
		{
			Hashtable hash = new Hashtable();
			hash.Add(VIEW, node.Attributes[VIEW].Value.ToString());
			
			m_Actions.Add(node.Attributes[NAME].Value.ToString(), hash);
		}

		foreach (XmlNode node in xmlDoc.GetElementsByTagName(VIEW))
		{
			Hashtable hash = new Hashtable();
			hash.Add(VIEW, node.Attributes[VIEW].Value.ToString());
			
			m_Views.Add(node.Attributes[NAME].Value.ToString(), hash);
		}

		StartCoroutine(PerformAction(action, parameters));

	}

	public void TriggerAction(string action, Dictionary<string, Object> parameters = null)
	{
		StartCoroutine(PerformAction(action, parameters));
	}
	IEnumerator PerformAction(string action, Dictionary<string, Object> parameters = null)
	{
		if (m_Actions.ContainsKey(action))
		{
			string targetView = m_Actions[action][VIEW].ToString();
			
			if (m_Views.ContainsKey(targetView))
			{
				string viewName = m_Views[targetView][VIEW].ToString();
				
				Application.LoadLevelAdditive(viewName);
				
				yield return 0;
				
				GameObject scene = GameObject.Find(viewName);

				if (scene != null)
				{
					scene.transform.parent = this.transform;

					View currentView = scene.GetComponent<View>();
					
					if (currentView != null)
					{
						currentView.OnViewLoaded(parameters);
					}
					else
					{
						Debug.LogError("There is no view in the scene");
					}
				}
				else
				{
					Debug.LogError("The scene " + viewName + " object does not exist");
				}

				m_CurrentScene = viewName;

				if (!string.IsNullOrEmpty(m_PreviousScene))
				{
					GameObject previousScene = GameObject.Find(m_PreviousScene);
					
					View previousView = previousScene.GetComponent<View>();
					
					if (previousView != null)
					{
						previousView.OnViewClosed();
					}
					else
					{
						Debug.LogError("There is no view in the scene");
					}
					
					yield return 0;
					
					if (previousScene != null)
					{
						Destroy(previousScene);
					}
					else
					{
						Debug.LogError("Previous scene cannot be found");
					}
				}
				m_PreviousScene = m_CurrentScene;
			}
			else
			{
				Debug.LogError("View does not exit in the flow xml");
			}
		}
		else
		{
			Debug.LogError("Action does not exit in the flow xml");
		}	
	}
}