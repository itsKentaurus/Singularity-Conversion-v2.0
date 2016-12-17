using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Tile : BlockInformation
{
    [XmlAttribute("Key")] private string m_PrefabName = "";
    [XmlArray("Position")] [XmlArrayItem("Vector")] private List<Vector3> m_Positions = new List<Vector3>();
	//[XmlIgnoreAttribute] private Texture2D m_Texture = null;
	[XmlAttribute("Path")] private string m_Path = string.Empty;
	[XmlAttribute("TextureName")] private string m_TextureName = string.Empty;

//	public Texture2D Texture 
//	{
//		get { return m_Texture; }
//		set { m_Texture = value; }
//	}

    public string PrefabName
    {
        get { return m_PrefabName; }
        set { m_PrefabName = value; }
    }

    public string Path 
	{
		get { return m_Path; }
		set { m_Path = value; }
	}

	public string TextureName 
	{
		get { return m_TextureName; }
		set { m_TextureName = value; }
	}

	public List<Vector3> Positions 
	{
		get { return m_Positions; }
		set { m_Positions = value; }
	}

	public void ClearTexture()
	{
//		m_Texture = null;
		m_Path = string.Empty;
	}
}