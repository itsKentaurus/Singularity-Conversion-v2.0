// File: CameraDetection
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraDetection : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	protected void OnTriggerEnter(Collider collider)
	{
		MeshRenderer mesh = collider.gameObject.GetComponent<MeshRenderer>();
		if (mesh != null)
		{
			mesh.enabled = true;
		}
	}

	protected void OnTriggerExit(Collider collider)
	{
		MeshRenderer mesh = collider.gameObject.GetComponent<MeshRenderer>();
		if (mesh != null)
		{
			mesh.enabled = false;
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