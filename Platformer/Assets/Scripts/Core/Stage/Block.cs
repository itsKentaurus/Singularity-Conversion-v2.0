// File: Block
// Created by: Rigil Malubay

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
	public float X { set; get; }
	[XmlAttribute("y")]
	public float Y { set; get; }
	[XmlAttribute("texture")]
	public string Texture { set; get; }
	// protected

	// private
	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}