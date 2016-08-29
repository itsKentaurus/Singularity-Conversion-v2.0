//
// Script name: TouchManager
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TouchAction
{
	public class TouchManager : SingletonComponent<TouchManager> 
	{
		#region Variables
		private List<BaseTouch> m_CurrentTouches = new List<BaseTouch>();
		private TouchEvent m_CurrentTouchEvent = new TouchEvent();
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
			RaycastHit[] hits = Physics.RaycastAll(CustomCamera.CameraManager.Instance.UICamera.ScreenPointToRay(position));

			// Settings touch event information
			m_CurrentTouchEvent.StartPosition = CustomCamera.CameraManager.Instance.UICamera.ScreenToWorldPoint(position);
			m_CurrentTouchEvent.CurrentPosition = CustomCamera.CameraManager.Instance.UICamera.ScreenToWorldPoint(position);

			BaseTouch touch = null;

			List<RaycastHit> hitsList = GetLayersAndSort("UI", hits);

			for (int i = 0; i < hitsList.Count; ++i)
			{
				if (hitsList[i].collider)
				{
					touch = hitsList[i].collider.gameObject.GetComponent<BaseTouch>();
					if (touch != null)
					{
						m_CurrentTouches.Add(touch);
						if (touch.OnTouchBegin(m_CurrentTouchEvent))
						{
							break;
						}
					}
				}
			}

			hitsList = GetLayersAndSort("Default", hits);

			for (int i = 0; i < hitsList.Count; ++i)
			{
				if (hitsList[i].collider)
				{
					touch = hitsList[i].collider.gameObject.GetComponent<BaseTouch>();
					if (touch != null)
					{
						m_CurrentTouches.Add(touch);
						if (touch.OnTouchBegin(m_CurrentTouchEvent))
						{
							break;
						}
					}
				}
			}
		}

		private List<RaycastHit> GetLayersAndSort(string layerName, RaycastHit[] hits)
		{
			List<RaycastHit> hitsList = new List<RaycastHit>();

			for (int i = 0; i < hits.Length; ++i)
			{
				if (hits[i].collider != null && hits[i].collider.gameObject.layer == LayerMask.NameToLayer(layerName))
				{
					hitsList.Add(hits[i]);
				}
			}

			return BubbleSort(hitsList);
		}

		private List<RaycastHit> BubbleSort(List<RaycastHit> hitsList)
		{
			RaycastHit temp;
			for (int i = 0; i < hitsList.Count; ++i)
			{
				for (int j = i + 1; j < hitsList.Count; ++j)
				{
					if (hitsList[j].distance < hitsList[i].distance)
					{
						temp = hitsList[j];
						hitsList[j] = hitsList[i];
						hitsList[i] = temp;
					}
				}
			}

			return hitsList;
		}

		private void OnTouchMoved(Vector3 position)
		{
			for (int i = 0; i < m_CurrentTouches.Count; ++i)
			{
				UpdateCurrentTouchEvent(position, m_CurrentTouches[i].gameObject.layer);
				m_CurrentTouches[i].OnTouchMoved(m_CurrentTouchEvent);
			}
		}

		private void OnTouchEnded(Vector3 position)
		{
			for (int i = 0; i < m_CurrentTouches.Count; ++i)
			{
				UpdateCurrentTouchEvent(position, m_CurrentTouches[i].gameObject.layer);
				m_CurrentTouches[i].OnTouchEnded(m_CurrentTouchEvent);
			}
			m_CurrentTouches.Clear();
			m_CurrentTouchEvent.Reset();
		}

		private void UpdateCurrentTouchEvent(Vector3 position, int layer)
		{
			m_CurrentTouchEvent.LastPosition = m_CurrentTouchEvent.CurrentPosition;
			if (layer == LayerMask.NameToLayer("UI"))
			{
				m_CurrentTouchEvent.CurrentPosition = CustomCamera.CameraManager.Instance.UICamera.ScreenToWorldPoint(position);
			}
			else if (layer == LayerMask.NameToLayer("Default"))
			{
				m_CurrentTouchEvent.CurrentPosition = CustomCamera.CameraManager.Instance.MainCamera.ScreenToWorldPoint(position);
			}
		}
		#endregion
	}
}