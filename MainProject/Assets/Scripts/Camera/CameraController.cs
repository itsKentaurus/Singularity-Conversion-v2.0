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
    [SerializeField] protected Transform m_Character;
    [SerializeField] protected Transform m_CameraAnchor;
    private Vector3 m_CameraTargetPosition = Vector3.zero;

    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if (m_Character)
        {
            m_CameraTargetPosition = Vector3.zero;
            m_CameraTargetPosition.x = m_CameraAnchor.position.x + AdjustCameraXPosition();
            m_CameraTargetPosition.y = AdjustCameraYPosition();
            m_CameraTargetPosition.z = m_CameraAnchor.position.z;
            m_CameraAnchor.position = m_CameraTargetPosition;//Vector3.Lerp(m_CameraAnchor.position, m_Character.position, 0.075f);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    protected virtual float AdjustCameraXPosition()
    {
        return 0f;
    }

    protected virtual float AdjustCameraYPosition()
    {
        return Mathf.Lerp(m_CameraAnchor.position.y, m_Character.position.y, 0.075f);
    }
    #endregion

    #region Private Methods
    #endregion
}