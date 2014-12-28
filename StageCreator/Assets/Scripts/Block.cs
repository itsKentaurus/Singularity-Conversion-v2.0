// File: Block
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Block  {

	[XmlAttribute("x")]
	public float X;
	[XmlAttribute("y")]
	public float Y;

	public Block()
	{

	}

	public Block(GameObject obj)
	{
		X = obj.transform.position.x;
		Y = obj.transform.position.y;
	}
}