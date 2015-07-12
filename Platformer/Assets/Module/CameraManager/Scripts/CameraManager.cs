// File: CameraManager.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour 
{
	private const string CAMERA_PATH = "Prefab/";
	private const string MAIN_CAMERA_NAME = "Main Camera";
	private const string UI_CAMERA_NAME = "UI Camera";
	private const string EDITOR_CAMERA_NAME = "EditorCamera";

	private static CameraManager m_Instance = null;
	private CameraController m_MainCamera;
	private CameraController m_UICamera;

	public static bool IsInstanceNull
	{
		get { return (m_Instance == null); }
	}

	public static CameraManager Instance
	{
		get
		{
			if (IsInstanceNull)
			{
				GameObject obj = new GameObject("CameraManager");
				m_Instance = obj.AddComponent<CameraManager>();
			}

			return m_Instance;
		}
	}

	public CameraController MainCamera
	{
		private set { m_MainCamera = value; }
		get
		{
			return m_MainCamera;
		}
	}

	public CameraController UICamera
	{
		private set { m_UICamera = value; }
		get
		{
			return m_UICamera;
		}
	}

	#region Pulic Methods
	public void Initialize()
	{
		Destroy(GameObject.Find(EDITOR_CAMERA_NAME));

		GameObject main = Resources.Load(CAMERA_PATH + MAIN_CAMERA_NAME) as GameObject;
		GameObject ui = Resources.Load(CAMERA_PATH + UI_CAMERA_NAME) as GameObject;

		MainCamera = main.GetComponent<CameraController>();
		UICamera = ui.GetComponent<CameraController>();

		Instantiate(MainCamera);
		Instantiate(UICamera);
	}
	#endregion
}