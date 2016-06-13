using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Tile : BlockInformation
{
	[XmlArray("Position")] [XmlArrayItem("Vector")] public List<Vector3> m_Positions = new List<Vector3>();
	[XmlAttribute("Texture")] public Texture2D m_Texture = null;
	[XmlAttribute("Path")] public string m_Path = string.Empty;

	public void ClearTexture()
	{
		m_Texture = null;
		m_Path = string.Empty;
	}
}