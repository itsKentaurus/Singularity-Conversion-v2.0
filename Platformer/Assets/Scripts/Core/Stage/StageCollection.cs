// File: LevelController.cs
// Created by: Rigil Malubay

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("StageCollection")]
public class StageCollection {
	
	// const

	// enum

	// private

	// protected

	// public
	[XmlArray("Stages")]
	[XmlArrayItem("Stage")]
	public List<Stage> m_Stages= new List<Stage>();

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}