using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Text;
using System.IO;  

public class LevelEditorScript : BaseEditorScript
{
	private Vector2 m_ScrollPosition = Vector2.zero;

	private class Tile
	{
		public char m_Symbol = ' ';
		public List<Vector3> m_Positions = new List<Vector3>();
		public Texture2D m_Texture = null;

		public Tile(char symbol, Vector3 position)
		{
			m_Symbol = symbol;
			m_Positions.Add(position);
		}

		public void ClearTexture()
		{
			m_Texture = null;
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
		m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition, false, false);
		for (int index = 0; index < m_Tiles.Count; ++index)
		{
			boudingBox = EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Height(100f));
			float side = boudingBox.height * 0.9f;
			fileDropLocation = new Rect(boudingBox.width - boudingBox.height * 0.925f, boudingBox.position.y + boudingBox.height * 0.05f, side, side);

			m_Tiles.ElementAt(index).Value.m_Texture = EditorGUILayout.DropLocation<Texture2D>(fileDropLocation, "Drop \n Texture \n Here", m_Tiles.ElementAt(index).Value.m_Texture);

			if (m_Tiles.ElementAt(index).Value.m_Texture!= null)
			{
				EditorGUI.DrawPreviewTexture(fileDropLocation, m_Tiles.ElementAt(index).Value.m_Texture);	
			}

			Event currentEvent = Event.current;
			if (currentEvent.type == EventType.ContextClick)
			{
				Vector2 mousePos = currentEvent.mousePosition;
				if (fileDropLocation.Contains(mousePos))
				{
					// Now create the menu, add items and show it
					GenericMenu menu = new GenericMenu();
					menu.AddItem(new GUIContent("Clear Texture"), false, m_Tiles.ElementAt(index).Value.ClearTexture);
//					menu.AddSeparator("");
//					menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, Callback, "item 3");
					menu.ShowAsContext();
					currentEvent.Use();
				}
			}
			EditorGUILayout.LabelField(m_Tiles.ElementAt(index).Key.ToString());
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
	}

	public override void Save()
	{
		base.Save();
	}

//	private T DropLocation<T>(Rect rect, string label, T defaultObject = null) where T : Object
//	{
//		Event evt = Event.current;
//		GUI.Box(rect, label);
//
//		var eventType = Event.current.type;
//
//		if (rect.Contains(evt.mousePosition) && (eventType == EventType.DragUpdated || eventType == EventType.DragPerform))
//		{
//			// Show a copy icon on the drag
//			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
//
//			if (eventType == EventType.DragPerform)
//			{
//				DragAndDrop.AcceptDrag();
//				return DragAndDrop.objectReferences[0] as T;
//			}
//			
//			Event.current.Use();
//		}
//
//		return defaultObject;
//	}

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