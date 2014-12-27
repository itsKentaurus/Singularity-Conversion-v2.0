// File: StageController.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class StageController : MonoBehaviour {
	
	// const
	private const string PATH = "Assets/Resources/XML/Stages.xml";
	// enum

	// private

	// protected

	// public

	#region Unity API
	protected virtual void Start()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(StageContainer));
		FileStream stream = new FileStream(PATH, FileMode.Open);
		StageContainer container = serializer.Deserialize(stream) as StageContainer;
		stream.Close();	

		foreach(Stage stage in container.m_Stages)
		{
			Debug.Log(stage.Name);
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