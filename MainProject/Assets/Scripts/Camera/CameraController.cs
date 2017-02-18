//
// Script name: CameraController
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField] protected Player m_Character;
    [SerializeField] protected Transform m_CameraAnchor;
    private Vector3 m_CameraTargetPosition = Vector3.zero;
    private Camera m_MainCamera;

    private float m_TargetPlatformHeight = 0f;
    #endregion

    #region Unity API
    protected virtual void Awake()
    {
        if (CustomCamera.CameraManager.IsInstanceNull)
        {
            CustomCamera.CameraManager.Instance.Init();
        }
        m_MainCamera = CustomCamera.CameraManager.Instance.MainCamera;
        m_MainCamera.transform.parent = m_CameraAnchor.transform;
        Vector3 position = Vector3.zero;
        position.z = m_MainCamera.transform.position.z;
        m_MainCamera.transform.localPosition = position;
    }

    protected virtual void Update()
    {
        if (m_Character != null)
        {
            m_CameraTargetPosition = Vector3.zero;
            m_CameraTargetPosition.x = AdjustCameraXPosition();
            m_CameraTargetPosition.y = AdjustCameraYPosition();
            m_CameraTargetPosition.z = m_CameraAnchor.position.z;
            m_CameraAnchor.position = m_CameraTargetPosition;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    protected virtual float AdjustCameraXPosition()
    {
        float viewportPosition = m_MainCamera.WorldToViewportPoint(m_Character.transform.position).x;
        if (viewportPosition >= 0.5f + 0.1f)
        {
            return Mathf.Lerp(m_CameraAnchor.transform.position.x, m_CameraAnchor.transform.position.x + ((viewportPosition - 0.5f) * m_MainCamera.orthographicSize * m_MainCamera.aspect), 0.1f);
        }
        else if (viewportPosition <= 0.5f - 0.1f)
        {
            return Mathf.Lerp(m_CameraAnchor.transform.position.x, m_CameraAnchor.transform.position.x - ((0.5f - viewportPosition) * m_MainCamera.orthographicSize * m_MainCamera.aspect), 0.1f);
        }

        return m_CameraAnchor.position.x;
    }

    protected virtual float AdjustCameraYPosition()
    {
        if (m_Character.IsGrounded)
        {
            if (m_Character.transform.position.y != m_CameraAnchor.transform.position.y)
            {
                m_TargetPlatformHeight = m_Character.transform.position.y;
            }
        }
        else
        {
            if (CustomCamera.CameraManager.Instance.MainCamera.WorldToViewportPoint(m_Character.transform.position).y < 0.5f)
            {
                m_TargetPlatformHeight = m_Character.transform.position.y;
            }
        }

        return Mathf.Lerp(m_CameraAnchor.transform.position.y, m_TargetPlatformHeight, 0.10f);
    }
    #endregion

    #region Private Methods
    #endregion
}