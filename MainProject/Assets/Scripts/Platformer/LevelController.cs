//
// Script name: FallingTest
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using Track;

namespace Platformer
{
    public class LevelController : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected TrackController m_TrackController;
        [SerializeField] protected TrackVehicle m_Vehicule;

        protected bool m_IsInitialized = false;
        #endregion

        #region Unity API
        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Update()
        {
            if (!m_IsInitialized)
            {
                return;
            }

            if (m_Vehicule.IsInAir)
            {
                m_Vehicule.SetTrackPiece(m_TrackController.FindClosestTrack(m_Vehicule.transform.position));
            }
        }
        #endregion

        #region Public Methods
        public virtual void Init()
        {
            if (!m_IsInitialized)
            {
                m_TrackController.Init();
                m_IsInitialized = true;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

    }
}