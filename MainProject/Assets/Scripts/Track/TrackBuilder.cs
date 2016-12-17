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
    public class TrackBuilder : MonoBehaviour
    {
        #region Variables
        private const string PATH_TO_PREFABS = "Prefabs/TrackPieces/{0}";
        private const float SCALING_SIZE = 50f;

        [SerializeField] protected List<TrackPiece> m_Tracks = new List<TrackPiece>();

        // Used to load all the track pieces
        private Dictionary<string, TrackPiece> m_TrackHolder = new Dictionary<string, TrackPiece>();
        private Dictionary<string, List<TrackInformation>> m_Information;

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
        public void SetTracks(Dictionary<string, List<TrackInformation>> tracks)
        {
            m_Information = tracks;
            m_Information.Remove(string.Empty);
            foreach (string str in tracks.Keys)
            {
                if (!string.IsNullOrEmpty(str) && !m_TrackHolder.ContainsKey(str))
                {
                    m_TrackHolder.Add(str, Resources.Load<TrackPiece>(string.Format(PATH_TO_PREFABS, str)));
                }
            }
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
            TrackPiece piece = null;
            foreach (KeyValuePair<string, List<TrackInformation>> info in m_Information)
            {
                foreach (TrackInformation trackInfo in info.Value)
                {
                    piece = Instantiate<TrackPiece>(m_TrackHolder[info.Key]);
                    // Makes sure it's parented
                    piece.transform.parent = transform;
                    // Reset name to not have the (clone)
                    piece.name = info.Key;
                    // Set Track information
                    piece.SetTrackInformation(trackInfo);
                    m_Tracks.Add(piece);
                }
            }
        }

        protected virtual void PlaceTracks()
        {
            foreach (TrackPiece piece in m_Tracks)
            {
                piece.transform.position = piece.Information.Position * SCALING_SIZE;
            }
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
