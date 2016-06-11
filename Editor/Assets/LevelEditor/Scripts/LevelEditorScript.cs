using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Text;
using System.IO;  

public class LevelEditorScript : BaseEditorScript
{
	private class Tile
	{
		public char m_Symbol = ' ';
		public List<Vector3> m_Positions = new List<Vector3>();

		public Tile(char symbol, Vector3 position)
		{
			m_Symbol = symbol;
			m_Positions.Add(position);
		}
	}

	Dictionary<char, Tile> m_Tiles = new Dictionary<char, Tile>();
	private int m_CurrentRow = 0;

	public void AddRow(char[] tiles)
	{
		for (int i = 0 ; i < tiles.Length ; ++i)
		{
			if (m_Tiles.ContainsKey(tiles[i])) 
			{
				m_Tiles[tiles[i]].m_Positions.Add(new Vector3(i, m_CurrentRow));
			}
			else 
			{
				m_Tiles.Add(tiles[i], new Tile(tiles[i], new Vector3(i, m_CurrentRow)));
			}
		}
	}

	public override void Display()
	{
		base.Display();
		Rect boudingBox = new Rect();
		Rect fileDropLocation = new Rect();
		for (int index = 0; index < m_Tiles.Count; ++index)
		{
			boudingBox = EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Height(100f));
			float side = boudingBox.height * 0.9f;
			fileDropLocation = new Rect(boudingBox.width - boudingBox.height * 0.925f, boudingBox.position.y + boudingBox.height * 0.05f, side, side);

			DropFileLocation(fileDropLocation, "");

			Event currentEvent = Event.current;
			if (currentEvent.type == EventType.ContextClick)
			{
				Vector2 mousePos = currentEvent.mousePosition;
				if (fileDropLocation.Contains(mousePos))
				{
					// Now create the menu, add items and show it
					GenericMenu menu = new GenericMenu();
					menu.AddItem(new GUIContent("MenuItem" + index), false, null);
//					menu.AddSeparator("");
//					menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, Callback, "item 3");
					menu.ShowAsContext();
					currentEvent.Use();
				}
			}
			EditorGUILayout.LabelField(m_Tiles.ElementAt(index).Key.ToString());
			EditorGUILayout.EndHorizontal();
		}
	}

	public override void Save()
	{
		base.Save();
	}

	private string DropFileLocation(Rect rect, string label)
	{
		Event evt = Event.current;
		GUI.Box(rect, label);

		var eventType = Event.current.type;

		if (rect.Contains(evt.mousePosition) && (eventType == EventType.DragUpdated || eventType == EventType.DragPerform))
		{
			// Show a copy icon on the drag
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (eventType == EventType.DragPerform)
			{
				DragAndDrop.AcceptDrag();
				return ConvertStringArrayToString(DragAndDrop.paths);
			}
			
			Event.current.Use();
		}

		return string.Empty;
	}

	private string ConvertStringArrayToString(string[] array)
	{
		StringBuilder builder = new StringBuilder();
		foreach(string str in array)
		{
			builder.Append(str);
		}
		return builder.ToString();
	}

}