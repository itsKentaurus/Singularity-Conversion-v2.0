using UnityEngine;
using System.Collections;

public class BaseEditorScript
{
	public virtual void Save()
	{
		Debug.Log("Save");
	}

	public virtual void Display()
	{
		Debug.Log("Displaying");
	}
}
