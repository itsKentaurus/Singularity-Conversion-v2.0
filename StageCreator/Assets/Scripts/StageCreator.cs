// File: StageCreator
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class StageCreator : MonoBehaviour {

	public GameObject m_Player;
	public GameObject m_Blocks;

	public void Start()
	{
		Debug.Log("Writing With XmlTextWriter");
		
		XmlSerializer serializer = new XmlSerializer(typeof(Stage));
		// Create an XmlTextWriter using a FileStream.

		Stage s = new Stage();

		s.m_Player = new Player(m_Player);
		s.m_BlocksContainer = new Blocks(m_Blocks);

		Stream fs = new FileStream("Assets/Resources/XML/Stage.xml", FileMode.Append);
		XmlWriter writer = 
			new XmlTextWriter(fs, Encoding.Unicode);
		// Serialize using the XmlTextWriter.
		serializer.Serialize(writer, s);
		writer.Close();
	}}