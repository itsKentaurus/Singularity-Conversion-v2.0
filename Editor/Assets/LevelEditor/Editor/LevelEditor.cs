using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;  

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

		FileInformation fileInfo = null;

		if (m_Script == null)
		{
			fileInfo = DropFileLocation();
		}
		else
		{
			if (GUILayout.Button("Reset"))
			{
				Reset();
			}

			if (GUILayout.Button("Save"))
			{
				if (m_Script != null) 
				{
					m_Script.Save();
				}
				else 
				{
					Debug.Log("Nothing to save.");
				}
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
				m_Script = new LevelEditorScript();
				string line;
				StreamReader theReader = new StreamReader(fileInfo.Path, Encoding.Default);
				using (theReader)
				{
					do
					{
						line = theReader.ReadLine();
						if (line != null)
						{
							(m_Script as LevelEditorScript).AddRow(line.ToCharArray());
						}
					}
					while (line != null);
				}
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
		Rect dropArea = new Rect(0f, 0f, Screen.width, Screen.height);
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
