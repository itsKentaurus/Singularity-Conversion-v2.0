// File: Player
// Created by: Rigil Malubay

using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Player {
	
	// const

	// enum

	// public
	[XmlAttribute("x")]
	public float X { set; get; }
	[XmlAttribute("y")]
	public float Y { set; get; }

	// protected

	// private
	public Vector3 StartLocation
	{
		get
		{
			return new Vector3(X, Y, 0f);
		}
	}
	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}