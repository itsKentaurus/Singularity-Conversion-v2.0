// File: Blocks
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Blocks {
	[XmlElement("Block")]
	public List<Block> m_Blocks = new List<Block>();

	public Blocks()
	{

	}

	public Blocks(GameObject blockContainer)
	{
		foreach (Transform t in blockContainer.transform)
		{
			m_Blocks.Add(new Block(t.gameObject));
		}
	}
}