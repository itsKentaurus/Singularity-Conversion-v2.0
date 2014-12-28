// File: Block
// Created by: Rigil Malubay

using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Block {
	
	// const

	// enum

	// public
	[XmlAttribute("type")]
	public int Type { set; get; }
	[XmlAttribute("x")]
	public float X { set; private get; }
	[XmlAttribute("y")]
	public float Y { set; private get; }
	[XmlAttribute("rotz")]
	public float RotZ { set; private get; }
	[XmlAttribute("texture")]
	public string Texture { set; get; }
	// protected

	// private

	// protected
	public Vector3 Location
	{
		get
		{
			return new Vector3(X, Y, 0f);
		}
	}
	public Quaternion Rotation
	{
		get
		{
			return Quaternion.Euler(0, 0, RotZ);
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