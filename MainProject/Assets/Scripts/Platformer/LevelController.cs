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
        [SerializeField] protected TrackBuilder m_TrackController;
        [SerializeField] protected Player m_Player;
        protected XMLToTrackConverter m_Converter = new XMLToTrackConverter();

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

            if (m_Player.IsInAir)
            {
                //m_Player.SetTrackPiece(m_TrackController.FindClosestTrack(m_Player.transform.position));
            }
        }
        #endregion

        #region Public Methods
        public virtual void Init()
        {
            if (!m_IsInitialized)
            {
                m_TrackController.SetTracks(m_Converter.Load("hi"));
                m_Player.Init();
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