// File: CameraDetection
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerCamera : MonoBehaviour {
	
	// const
	private float TIME_TO_REACH = 1f;
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

	private void LateUpdate()
	{
		if (m_Player == null)
		{
			GameObject player = GameObject.Find("Player");
			if (player != null)
			{
				m_Player = player.GetComponent<Player>();
			}
			else
			{
				Debug.LogError("Player not found");
			}
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

			transform.position = LerpVectors(translation);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private Vector3 LerpVectors(Vector3 translation)
	{
		float x = Mathf.Lerp(transform.position.x, transform.position.x + translation.x, TIME_TO_REACH);
		float y = Mathf.Lerp(transform.position.y, transform.position.y + translation.y, TIME_TO_REACH);

		return new Vector3(x, y, transform.position.z);
	}
	#endregion
}