//
// Script name: SingletonComponent
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

public class SingletonComponent<T> : SubjectObserver where T : Component
{
	#region Variables
	protected static T m_Instance;
	public static T Instance
	{
		get
		{
			if (IsInstanceNull())
			{
				GameObject obj = new GameObject(typeof(T).Name);
				m_Instance = obj.AddComponent<T>();
			}
			return m_Instance;
		}
	}
	#endregion

	#region Unity API
	#endregion

	#region Public Methods
	public static bool IsInstanceNull()
	{
		return m_Instance == null;
	}

	public virtual void Init()
	{

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
