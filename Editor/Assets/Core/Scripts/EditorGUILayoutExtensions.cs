using UnityEngine;
using System.Collections;
using UnityEditor;

public static class EditorGUILayoutExtensions
{
	public static T DropLocation<T>(this EditorGUILayout trans, Rect rect, string label, T defaultObject = null) where T : Object
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
				return DragAndDrop.objectReferences[0] as T;
			}

			Event.current.Use();
		}

		return defaultObject;
	}
}