﻿// File: Level.cs
// Created by: Rigil Malubay

using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Stage {
	
	// const

	// enum

	// private

	// protected

	// public
	[XmlAttribute("name")]
	public string Name;

	[XmlArray("Blocks")]
	[XmlArrayItem("Block")]
	public List<Block> m_Blocks = new List<Block>();

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion


}