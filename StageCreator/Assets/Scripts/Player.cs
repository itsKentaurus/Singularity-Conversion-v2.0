// File: Player
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Player {
	[XmlAttribute("x")]
	public float X;
	[XmlAttribute("y")]
	public float Y;

	public Player()
	{

	}
	public Player(GameObject player)
	{
		X = player.transform.position.x;
		Y = player.transform.position.y;
	}
}