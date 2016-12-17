using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Xml;
using System.Xml.Serialization;

public class LevelEditorScript : BaseEditorScript
{
	private Vector2 m_ScrollPosition = Vector2.zero;
	private XMLContainer<Tile> m_Tiles = new XMLContainer<Tile>();
	private int m_CurrentRow = 0;

	public void AddRow(char[] tiles)
	{
		for (int i = 0 ; i < tiles.Length ; ++i)
		{
			if (m_Tiles.Contains(tiles[i].ToString())) 
			{
				m_Tiles.GetElement(tiles[i].ToString()).Positions.Add(new Vector3(i, m_CurrentRow));
			}
			else 
			{
				Tile tile = m_Tiles.Add(tiles[i].ToString());
				tile.Positions.Add(new Vector3(i, m_CurrentRow));
			}
		}

        m_CurrentRow++;
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
            EditorGUILayout.LabelField(m_Tiles.ElementAt(index).m_Key);

            //			float side = boudingBox.height * 0.9f;
            //			fileDropLocation = new Rect(boudingBox.width - boudingBox.height * 0.925f, boudingBox.position.y + boudingBox.height * 0.05f, side, side);

            //			m_Tiles.ElementAt(index).Texture = EditorGUILayoutExtensions.DragAndDropArea<Texture2D>(fileDropLocation, "Drop \n Texture \n Here", m_Tiles.ElementAt(index).Texture);

            //			if (m_Tiles.ElementAt(index).Texture != null)
            //			{
            //				EditorGUI.DrawPreviewTexture(fileDropLocation, m_Tiles.ElementAt(index).Texture);
            //			}

            //			if (string.IsNullOrEmpty(m_Tiles.ElementAt(index).Path) && m_Tiles.ElementAt(index).Texture != null)
            //			{
            //				m_Tiles.ElementAt(index).Path = AssetDatabase.GetAssetPath(m_Tiles.ElementAt(index).Texture);
            //			}

            m_Tiles.ElementAt(index).PrefabName = EditorGUILayout.TextField(m_Tiles.ElementAt(index).PrefabName);

            Event currentEvent = Event.current;
			if (currentEvent.type == EventType.ContextClick)
			{
				Vector2 mousePos = currentEvent.mousePosition;
				if (fileDropLocation.Contains(mousePos))
				{
					// Now create the menu, add items and show it
					GenericMenu menu = new GenericMenu();
					menu.AddItem(new GUIContent("Remove Texture"), false, m_Tiles.ElementAt(index).ClearTexture);
//					menu.AddSeparator("");
//					menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, Callback, "item 3");
					menu.ShowAsContext();
					currentEvent.Use();
				}
			}

			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
	}

	public override void Save()
	{
		base.Save();
		m_Tiles.Save(Filename);
	}

	public override void LoadXML(string fileName)
	{
		base.LoadXML(fileName);
		m_Tiles = XMLContainer<Tile>.Load(Filename);
	}
}