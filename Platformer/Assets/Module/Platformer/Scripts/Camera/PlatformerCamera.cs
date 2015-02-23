// File: CameraDetection
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerCamera : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected

	// private
	private Vector3 m_CameraOffset = Vector3.zero;
	private Player m_Player = null;

	// properties
	public Vector3 Offset
	{
		set 
		{ 
			m_CameraOffset = value; 
		}
	}

	#region Unity API
	private void OnTriggerEnter(Collider collider)
	{
		MeshRenderer mesh = collider.gameObject.GetComponent<MeshRenderer>();
		if (mesh != null)
		{
			mesh.enabled = true;
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		MeshRenderer mesh = collider.gameObject.GetComponent<MeshRenderer>();
		if (mesh != null)
		{
			mesh.enabled = false;
		}
	}

	private void FixedUpdate()
	{
		if (m_Player == null)
		{
			m_Player = GameObject.Find("Player").GetComponent<Player>();
		}
		else
		{
			float xPosition = Mathf.Clamp(transform.position.x, -Mathf.Abs(m_CameraOffset.x), Mathf.Abs(m_CameraOffset.x));
			float yPosition = Mathf.Clamp(transform.position.y, -Mathf.Abs(m_CameraOffset.y), Mathf.Abs(m_CameraOffset.y));

			Vector3 translation = m_Player.gameObject.transform.position - new Vector3(xPosition, yPosition);
			translation.z = 0f;

			if (Mathf.Abs(xPosition) >= Mathf.Abs(m_CameraOffset.x) && Mathf.Abs(m_Player.transform.position.x) > Mathf.Abs(m_CameraOffset.x))
			{
				translation.x = 0f;
			}

			if (Mathf.Abs(yPosition) >= Mathf.Abs(m_CameraOffset.y) && Mathf.Abs(m_Player.transform.position.y) > Mathf.Abs(m_CameraOffset.y))
			{
				translation.y = 0f;
			}

			transform.Translate(translation * Time.deltaTime);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}