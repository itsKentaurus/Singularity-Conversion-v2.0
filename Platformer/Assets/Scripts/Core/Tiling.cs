// File: Tiling
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tiling : MonoBehaviour {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	protected void Start()
	{
		Vector3 scale = transform.localScale / 100f;
		GetComponent<Renderer>().material.mainTextureScale = new Vector2(scale.x, scale.y);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}