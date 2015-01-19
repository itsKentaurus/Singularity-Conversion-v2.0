// File: AppLauncher
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppLauncher : MonoBehaviour {
	
	private const string MAIN_CAMERA = "Prefabs/Init/Main Camera";
	private const string UI_CAMERA = "Prefabs/Init/UI Camera";
	private const string PAUSE_CONTROLLER = "Prefabs/Init/PauseController";
	private const string FLOW_MANAGER = "Prefabs/Init/FlowManager";
	private const string FIRST_SCENE = "Splash";

	public void Awake()
	{
		InitMainCamera();
		InitUICamera();
		InitPauseController();
		InitFlowManager();

		InitFirstScene();
		Destroy(this.gameObject);
	}

	private void InitMainCamera()
	{
		Instantiate(Resources.Load(MAIN_CAMERA));
	}

	private void InitUICamera()
	{
		Instantiate(Resources.Load(UI_CAMERA));
	}

	private void InitPauseController()
	{
		Instantiate(Resources.Load(PAUSE_CONTROLLER));
	}

	private void InitFlowManager()
	{
		Instantiate(Resources.Load(FLOW_MANAGER));
	}

	private void InitFirstScene()
	{
		FlowManager.Instance.TriggerAction(FIRST_SCENE);
	}
}