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
			Ray ray = new Ray(CustomCamera.CameraManager.Instance.UICamera.ScreenToWorldPoint(position), CustomCamera.CameraManager.Instance.UICamera.transform.forward);
			RaycastHit[] hit = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.NameToLayer("UI"));
			BaseTouch touch = null;
			Collider collider = null;

			TouchEvent evt = new TouchEvent();
			evt.StartPosition = CustomCamera.CameraManager.Instance.UICamera.ScreenToWorldPoint(position);

			for (int i = 0; i < hit.Length; ++i)
			{
				collider = hit[i].collider;
				if (collider != null)
				{
					touch = collider.gameObject.GetComponent<BaseTouch>();
					if (touch != null)
					{
						touch.OnTouchBegin(evt);
					}
				}

			}

			ray = new Ray(position, CustomCamera.CameraManager.Instance.MainCamera.transform.forward);
			hit = Physics.RaycastAll(ray, Mathf.Infinity, LayerMask.NameToLayer("Default"));
		}

		private void OnTouchMoved(Vector3 position)
		{

		}

		private void OnTouchEnded(Vector3 position)
		{
		
		}
		#endregion

	}
}