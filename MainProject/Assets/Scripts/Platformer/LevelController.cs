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
        [Header("General")]
        [SerializeField] protected TrackBuilder m_TrackController;
        [SerializeField] protected Player m_Player;
        protected XMLToTrackConverter m_Converter = new XMLToTrackConverter();
        protected TrackPiece m_ClosestTrack = null;

        [Header("Testing")]
        [SerializeField] protected Transform m_Shadow = null;
        [SerializeField, ReadOnly] protected Vector3 m_NextPosition = Vector3.zero;
        [SerializeField] protected Transform m_Landing = null;
        [SerializeField, ReadOnly] protected Vector3 m_TargetDirectionToMouse = Vector3.zero;
        [SerializeField, ReadOnly] protected Vector3 m_TargetPosition = Vector3.zero;
        [SerializeField, ReadOnly] protected Vector3 m_LandingPositoon = Vector3.zero;

        protected bool m_IsInitialized = false;
        #endregion

        #region Unity API
        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnDrawGizmos()
        {
            //Gizmos.color = Color.cyan;
            //Gizmos.DrawLine(m_Player.transform.position, m_TargetPosition);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(m_Player.transform.position, m_LandingPositoon);
        }

        protected virtual void Update()
        {
            if (!m_IsInitialized)
            {
                return;
            }

            // Removed shooting code for now
            //m_TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //m_TargetPosition.z = m_Player.transform.position.z;
            //m_TargetDirectionToMouse = m_TargetPosition - m_Player.transform.position;


            //m_TargetDirectionToMouse.z = m_Player.transform.position.z;
            //FindGround(m_Player.transform.position, m_TargetDirectionToMouse);
            FindClosestTrack();

            m_Player.OnUpdate();

            MoveShadhow();
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
        /// <summary>
        /// For every movement detect where the next landing track will be
        /// </summary>
        protected virtual void FindClosestTrack()
        {
            if (m_ClosestTrack != null && m_ClosestTrack.GetHeightOnTrack(m_Player.transform.position.x) > m_Player.transform.position.y)
            {
                m_Player.SetTrackPiece(m_ClosestTrack);
            }

            if (!m_Player.IsGrounded)
            {
                m_ClosestTrack = m_TrackController.FindClosestTrack(m_Player.transform.position);
            }
            else if (m_ClosestTrack != null)
            {
                m_ClosestTrack = null;
            }
        }

        /// <summary>
        /// Mostly used for testing
        /// </summary>
        protected virtual void MoveShadhow()
        {
            if (m_Shadow != null)
            {
                m_NextPosition = m_Player.transform.position;
                if (m_ClosestTrack != null)
                {
                    m_NextPosition.y = m_ClosestTrack.GetHeightOnTrack(m_Player.transform.position.x);
                }
                m_Shadow.transform.position = m_NextPosition;
                m_NextPosition = Vector3.zero;
            }
        }

        protected virtual void FindGround(Vector3 origin, Vector3 direction)
        {
            Vector3? position = m_TrackController.FindLandingPoint(origin, direction);

            if (position == null)
            {
                position = origin;
            }

            m_Landing.position = (Vector3) position;
            m_LandingPositoon = m_Landing.position;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}