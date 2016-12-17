//
// Script name: TrackController
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Track
{
    public class TrackController : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected List<TrackPiece> m_Tracks = new List<TrackPiece>();

        protected bool m_IsInitialized = false;
        #endregion

        #region Unity API
#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            foreach (TrackPiece track in m_Tracks)
            {
                track.DrawGizmo();
            }
        }
#endif
        #endregion

        #region Public Methods
        public virtual void Init()
        {
            if (!m_IsInitialized)
            {
                InitTrack();
                PlaceTracks();

                m_IsInitialized = true;
            }
        }

        /// <summary>
        /// When set a list of tracks it destroys all the tracks
        /// </summary>
        /// <param name="tracks"></param>
        public void SetTrack(List<TrackPiece> tracks)
        {
            m_Tracks = tracks;
            m_IsInitialized = false;
            Init();
        }

        public TrackPiece FindClosestTrack(Vector3 position)
        {
            foreach (TrackPiece track in m_Tracks)
            {
                if (track.IsIntersecting(position))
                {
                    return track;
                }
            }

            return null;
        }
        #endregion

        #region Protected Methods
        protected virtual void InitTrack()
        {
            TrackPiece previous = null;
            TrackPiece current = null;

            foreach (TrackPiece track in m_Tracks)
            {
                previous = current;

                if (previous != null)
                {
                    previous.SetNextTrack(track);
                }

                current = track;

                current.SetPreviousTrack(previous);
            }
        }

        protected virtual void PlaceTracks()
        {
            TrackPiece previous = null;

            foreach (TrackPiece track in m_Tracks)
            {
                if (previous == null)
                {
                    track.transform.position = Vector3.zero;
                }
                else
                {
                    // Multiplying by -1f to make sure the start position of 
                    // the next track is the same as the end of the current track
                    track.transform.position = previous.EndLocation.position + track.StartLocation.localPosition * -1f;
                }

                previous = track;
            }
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
