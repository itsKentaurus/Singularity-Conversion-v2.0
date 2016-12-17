using UnityEngine;
using System.Collections;

public class BaseEditorScript
{
	private string m_Filename = string.Empty;

	public string Filename 
	{
		get { return m_Filename; }
		set { m_Filename = value; }
	}

	public virtual void LoadXML(string fileName)
	{

	}

	public virtual void Save()
	{
	
	}

	public virtual void Display()
	{

	}
}
