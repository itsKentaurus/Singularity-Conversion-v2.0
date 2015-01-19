// File: FlowManager
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowManager : MonoBehaviour {
	//Here is a private reference only this class can access
	private static FlowManager m_Instance;

	private View m_CurrentScene;
	private GameObject m_MainCamera;

	//This is the public reference that other classes will use
	public static FlowManager Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(m_Instance == null)
			{
				m_Instance = GameObject.FindObjectOfType<FlowManager>();
			}
			return m_Instance;
		}
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
}