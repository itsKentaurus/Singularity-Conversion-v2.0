// File: Stage
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Stage{
	
	[XmlAttribute("name")]
	public float Name;

	[XmlElement("Player")]
	public Player m_Player;
	[XmlElement("Blocks")]
	public Blocks m_BlocksContainer;
}