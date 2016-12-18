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
        // Base size of all platforms
        private const float BASE_SIZE = 100f;

        [SerializeField]
        protected Dictionary<float, List<TrackPiece>> m_Tracks = new Dictionary<float, List<TrackPiece>>();

        // Used to load all the track pieces
        private Dictionary<string, TrackPiece> m_TrackHolder = new Dictionary<string, TrackPiece>();
        private Dictionary<string, List<TrackInformation>> m_Information;

        protected bool m_IsInitialized = false;
        #endregion

        #region Unity API
#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            foreach (KeyValuePair<float, List<TrackPiece>> tracks in m_Tracks)
            {
                foreach (TrackPiece track in tracks.Value)
                {
                    track.DrawGizmo();
                }
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
            foreach (KeyValuePair<float, List<TrackPiece>> tracks in m_Tracks)
            {
                foreach (TrackPiece trackPiece in tracks.Value)
                {
                    if (trackPiece.IsGoingToIntersect(position))
                    {
                        return trackPiece;
                    }
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

                    if (!m_Tracks.ContainsKey(piece.Information.Position.x))
                    {
                        m_Tracks.Add(piece.Information.Position.x, new List<TrackPiece>());
                    }

                    m_Tracks[piece.Information.Position.x].Add(piece);
                }
            }

            foreach (KeyValuePair<float, List<TrackPiece>> tracks in m_Tracks)
            {
                tracks.Value.Sort(SortByHeight);
                //foreach (TrackPiece p in tracks.Value)
                //{
                //    Debug.Log(p.Information.Position.y);
                //}
            }
        }

        protected virtual int SortByHeight(TrackPiece p1, TrackPiece p2)
        {
            return p1.Information.Position.y > p2.Information.Position.y ? -1 : 1;
        }

        protected virtual void PlaceTracks()
        {
            foreach (KeyValuePair<float, List<TrackPiece>> tracks in m_Tracks)
            {
                foreach (TrackPiece piece in tracks.Value)
                {
                    piece.transform.position = piece.Information.Position * BASE_SIZE;
                }
            }

            foreach (KeyValuePair<float, List<TrackPiece>> tracks in m_Tracks)
            {
                foreach (TrackPiece trackPiece in tracks.Value)
                {
                    if (m_Tracks.ContainsKey(tracks.Key - 1f))
                    {
                        FindPreviousTrack(trackPiece, m_Tracks[tracks.Key - 1f]);
                    }

                    if (m_Tracks.ContainsKey(tracks.Key + 1f))
                    {
                        FindNextTrack(trackPiece, m_Tracks[tracks.Key + 1f]);
                    }

                }
            }
        }

        protected virtual void FindPreviousTrack(TrackPiece trackPiece, List<TrackPiece> prevCol)
        {
            foreach (TrackPiece track in prevCol)
            {
                if (Mathf.Abs(trackPiece.Information.Position.y - track.Information.Position.y) <= 1f &&
                    trackPiece.StartLocation.position == track.EndLocation.position)
                {
                    trackPiece.SetPreviousTrack(track);
                    break;
                }
            }
        }

        protected virtual void FindNextTrack(TrackPiece trackPiece, List<TrackPiece> nextCol)
        {
            foreach (TrackPiece track in nextCol)
            {
                if (Mathf.Abs(trackPiece.Information.Position.y - track.Information.Position.y) <= 1f &&
                    track.StartLocation.localPosition + track.transform.position == trackPiece.EndLocation.localPosition + trackPiece.transform.position)
                {
                    trackPiece.SetNextTrack(track);
                    break;
                }
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }

}
