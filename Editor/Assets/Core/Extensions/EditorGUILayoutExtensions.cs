using UnityEngine;

public static class EditorGUILayoutExtensions
{
	public static T DragAndDropArea<T>(Rect rect, string label, T defaultObject = null) where T : Object
	{
		object[] objs = DragAndDropArea(rect, label);
		if (objs != null && objs[0] != null)
		{
			if (objs[0] is T)
			{
				return objs[0] as T;
			}
			else
			{
				Debug.LogError("<color=red>" + (objs[0] as Object).name + "</color> is not supported at this drop location.");
			}
		}
		return defaultObject;
	}

	public static Object[] DragAndDropArea(Rect rect, string label)
	{
		Event evt = Event.current;
		GUI.Box(rect, label);

		var eventType = Event.current.type;

		if (rect.Contains(evt.mousePosition) && (eventType == EventType.DragUpdated || eventType == EventType.DragPerform))
		{
			// Show a copy icon on the drag
			UnityEditor.DragAndDrop.visualMode = UnityEditor.DragAndDropVisualMode.Copy;

			if (eventType == EventType.DragPerform)
			{
				UnityEditor.DragAndDrop.AcceptDrag();
				return UnityEditor.DragAndDrop.objectReferences;
			}

			Event.current.Use();
		}

		return null;
	}
}
