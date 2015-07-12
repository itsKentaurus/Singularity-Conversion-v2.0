// File: CameraController.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : Observer 
{
	
	// const

	// enum

	// public

	// protected

	// private
	private Camera m_Camera;

	// properties
	public float Size
	{
		set { m_Camera.orthographicSize = value; }
		get { return m_Camera.orthographicSize; }
	}
	public Transform CameraTransform
	{
		get { return gameObject.transform; }
	}

	public Camera CameraObject
	{
		get 
		{ 
			if (m_Camera == null)
			{
				m_Camera = this.GetComponent<Camera>();
			}
			return m_Camera; 
		}
	}
	#region Unity API
	protected override void Awake()
	{
		m_Camera = this.GetComponent<Camera>();
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}