//
// Script name: TrackVehicule
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace Track
{
    public class TrackVehicle : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected TrackPiece m_CurrentTrack;

        public bool IsOnTrack
        {
            get { return m_CurrentTrack != null; }
        }
        #endregion

        #region Unity API
        public void OnUpdate()
        {
            Move();
        }
        #endregion

        #region Public Methods
        public float TrackHeightPosition()
        {
            float position = transform.position.y;

            if (m_CurrentTrack != null)
            {
                position = m_CurrentTrack.GetHeightOnTrack(transform.position.x);
            }

            return position;
        }

        public void SetTrackPiece(TrackPiece track)
        {
            m_CurrentTrack = track;
        }

        public void Move()
        {
            if (m_CurrentTrack != null)
            {
                m_CurrentTrack = m_CurrentTrack.GetNextTrackPiece(transform);
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

    }
}