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
	[XmlAttribute("rotz")]
	public float RotZ;
	[XmlAttribute("type")]
	public int Types;

	public Block()
	{

	}

	public Block(GameObject obj)
	{
		X = obj.transform.position.x;
		Y = obj.transform.position.y;
		RotZ = obj.transform.rotation.eulerAngles.z;
		Types = (int)obj.GetComponent<Terrain>().m_Type;
	}
}