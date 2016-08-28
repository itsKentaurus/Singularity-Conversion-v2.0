//
// Script name: TouchManager
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.EventSystems;

namespace TouchAction
{
	public class TouchManager : SingletonComponent<TouchManager> 
	{
		#region Variables
		private const string TOUCH_PREFIX = "Input <color=red>{0}</color> is at position <color=blue>{1}</color>";

		#endregion

		#region Unity API
		protected void Update()
		{
			if (IsInstanceNull())
			{
				return;
			}

			MouseTouch();
		}
		#endregion

		#region Public Methods
		public override void Init()
		{
			base.Init();
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Private Methods
		private void MouseTouch()
		{
			if (Input.GetMouseButtonUp(0))
			{
				OnTouchEnded(Input.mousePosition);
			}

			if (Input.GetMouseButtonDown(0))
			{
				OnTouchBegin(Input.mousePosition);
			}

			if (Input.GetMouseButton(0))
			{
				OnTouchMoved(Input.mousePosition);
			}
		}

		private void OnTouchBegin(Vector3 position)
		{
//			Ray ray = new Ray(position, Camera.main.transform.forward);
//			RaycastHit[] hit = Physics.RaycastAll(ray);
			Debug.LogFormat(TOUCH_PREFIX, "Down", position);
		}

		private void OnTouchMoved(Vector3 position)
		{
			Debug.LogFormat(TOUCH_PREFIX, "Move", position);
		}

		private void OnTouchEnded(Vector3 position)
		{
			Debug.LogFormat(TOUCH_PREFIX, "Ended", position);
		}
		#endregion

	}
}