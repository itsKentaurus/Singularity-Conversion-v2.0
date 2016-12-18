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
        protected TrackPiece m_ClosestTrack = null;

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
                if (m_ClosestTrack == null)
                {
                    m_ClosestTrack = m_TrackController.FindClosestTrack(m_Player.transform.position);
                }
                else
                {
                    if (m_ClosestTrack.GetHeightOnTrack(m_Player.transform.position.x) > m_Player.transform.position.y)
                    {
                        m_Player.SetTrackPiece(m_ClosestTrack);
                    }
                }
            }
            else if (m_ClosestTrack != null)
            {
                m_ClosestTrack = null;
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