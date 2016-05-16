using UnityEngine;
using UnityEditor;
using System.Text;

public class LevelEditor : EditorWindow 
{
	[MenuItem ("Kentaurus / Level Editor")]
	static void Init() 
	{
		// Get existing open window or if none, make a new one:
		LevelEditor window = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
		window.Show();
	}

	private BaseEditorScript m_Script = null;

	private void OnGUI() 
	{
		if (EditorApplication.isCompiling) 
		{
			Reset();
			return;
		}

		FileInformation fileInfo = DropFileLocation();

		if (GUILayout.Button("Reset"))
		{
			Reset();
		}

		if (GUILayout.Button("Save"))
		{
			if (m_Script != null) 
			{
				m_Script.Save ();
			}
			else 
			{
				Debug.Log("Nothing to save.");
			}
		}

		if (m_Script == null && fileInfo == null) 
		{
			return;
		}

		if (fileInfo != null)
		{
			if (m_Script != null) 
			{
				m_Script.Save();
			}

			switch (fileInfo.Type) 
			{
			case FileInformation.FileType.TXT:
				m_Script = new BaseEditorScript();
				break;
			case FileInformation.FileType.XML:
				break;
			case FileInformation.FileType.UNKNOWN:
				Reset();
				Debug.LogError(fileInfo.Filename + " is not a supported file type.");
				return;
			}
		}

		m_Script.Display();
	}

	/// <summary>
	/// Drops the file location.
	/// </summary>
	/// <returns>The file location.</returns>
	private FileInformation DropFileLocation()
	{
		Event evt = Event.current;
		Rect dropArea = GUILayoutUtility.GetRect(0f, 50f, GUILayout.ExpandWidth (true));
		GUI.Box(dropArea, "Drop Level Here");

		var eventType = Event.current.type;

		if (dropArea.Contains(evt.mousePosition) && (eventType == EventType.DragUpdated || eventType == EventType.DragPerform))
		{
			// Show a copy icon on the drag
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (eventType == EventType.DragPerform)
			{
				DragAndDrop.AcceptDrag();
				return new FileInformation(ConvertStringArrayToString(DragAndDrop.paths));
			}

			Event.current.Use();
		}

		return null;
	}

	/// <summary>
	/// Converts the string array to string.
	/// </summary>
	/// <returns>The string array to string.</returns>
	/// <param name="array">Array.</param>
	private string ConvertStringArrayToString(string[] array)
	{
		StringBuilder builder = new StringBuilder();
		foreach(string str in array)
		{
			builder.Append(str);
		}
		return builder.ToString();
	}

	/// <summary>
	/// Reset this instance.
	/// </summary>
	private void Reset()
	{
		Debug.Log("Window has been reset.");
		m_Script = null;
	}
}
