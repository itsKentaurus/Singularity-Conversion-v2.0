//
// Script name: CameraManager
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using System.IO;

namespace CustomCamera
{
	public class CameraManager : SingletonComponent<CameraManager> 
	{
		#region Variables
		private const string BASIC_MAIN_CAMERA = "BasicMainCamera";
		private const string BASIC_UI_CAMERA = "BasicUICamera";

		private const string CUSTOM_MAIN_CAMERA = "MainCamera";
		private const string CUSTOM_UI_CAMERA = "UICamera";

		private const string CAMERA_PATH = "Prefabs/Camera";

		private Camera m_UICamera;
		private Camera m_MainCamera;


		public Camera UICamera 
		{
			get { return m_UICamera; }
		}

		public Camera MainCamera 
		{
			get { return m_MainCamera; }
		}

		#endregion

		#region Unity API
		#endregion

		#region Public Methods
		public override void Init()
		{
			base.Init();
			m_UICamera = LoadCamera(CUSTOM_UI_CAMERA, BASIC_UI_CAMERA);
			m_MainCamera = LoadCamera(CUSTOM_MAIN_CAMERA, BASIC_MAIN_CAMERA);
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Private Methods
		private Camera LoadCamera(string customCamera, string baseCamera)
		{
			Camera camera = Resources.Load<Camera>(Path.Combine(CAMERA_PATH, customCamera));

			if (camera != null)
			{
				camera = Instantiate<Camera>(camera);
				camera.name = customCamera;
			}
			else
			{
				camera = Resources.Load<Camera>(Path.Combine(CAMERA_PATH, baseCamera));

				if (camera != null)
				{
					camera = Instantiate<Camera>(camera);
					camera.name = baseCamera;
				}
			}

			camera.transform.parent = transform;

			return camera;
		}
		#endregion

	}
}
