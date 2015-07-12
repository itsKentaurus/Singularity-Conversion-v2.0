// File: TouchController.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{
	private static TouchManager m_Instance;

	private List<ITouchListener> m_CurrentObjects = new List<ITouchListener>();

	public static bool IsInstanceNull
	{
		get { return m_Instance == null; }
	}

	public static TouchManager Instance
	{
		get 
		{
			if (IsInstanceNull)
			{
				GameObject obj = new GameObject("TouchManager");
				m_Instance = obj.AddComponent<TouchManager>();
			}
			return m_Instance;
		}
	}

	public enum eTouchState
	{
		NONE,
		BEGIN,
		HOLD,
		END
	}

	#region Unity API
	protected void Update()
	{
#if UNITY_EDITOR
		EditorTouch();
#endif
	}
	#endregion

	#region Public Methods
	public void Initialize()
	{

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void EditorTouch()
	{
		eTouchState touchState = eTouchState.NONE;
		if (Input.GetMouseButtonDown(0))
		{
			touchState = eTouchState.BEGIN;
		}
		else if (Input.GetMouseButton(0))
		{
			touchState = eTouchState.HOLD;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			touchState = eTouchState.END;
		}
		ApplyTouch(touchState, Input.mousePosition);
	}

	private void ApplyTouch(eTouchState touchState, Vector3 touchPosition)
	{
		if (touchState == eTouchState.NONE)
		{
			return;
		}

		Ray ray = CameraManager.Instance.UICamera.CameraObject.ScreenPointToRay(touchPosition);

		RaycastHit[] hits = Physics.RaycastAll(ray);

		List<ITouchListener> list = FilterHits(hits);

		TouchEvent touchEvent = new TouchEvent();
		touchEvent.m_TouchPosition = ray.GetPoint(-ray.GetPoint(0).z);

		switch(touchState)
		{
		case eTouchState.BEGIN:
			UpToLayer(list, touchEvent);
			break;
		case eTouchState.HOLD:
			HoldLayers(touchEvent);
			break;
		case eTouchState.END:
			EndLayers(touchEvent);
			break;
		}
	}

	private List<ITouchListener> FilterHits(RaycastHit[] hits)
	{
		List<ITouchListener> touchList = new List<ITouchListener>();
		System.Array.Reverse(hits);
		for  (int i = 0 ; i < hits.Length ; ++i) 
		{
			RaycastHit hit = hits[i];
			ITouchListener touchedObject = hit.transform.GetComponent<ITouchListener>();

			if (touchedObject != null)
			{
				touchList.Add(touchedObject);
			}
		}
		return touchList;
	}

	private void UpToLayer(List<ITouchListener> touchObject, TouchEvent evt)
	{
		foreach(ITouchListener touch in touchObject)
		{
			if (touch.OnTouchBegin(evt))
			{
				m_CurrentObjects.Add(touch);
				return;
			}
		}
	}

	private void HoldLayers(TouchEvent evt)
	{
		foreach(ITouchListener touch in m_CurrentObjects)
		{
			touch.OnTouchMove(evt);
		}
	}

	private void EndLayers(TouchEvent evt)
	{
		foreach(ITouchListener touch in m_CurrentObjects)
		{
			touch.OnTouchEnd(evt);
		}
		m_CurrentObjects.Clear();
	}
	#endregion
}