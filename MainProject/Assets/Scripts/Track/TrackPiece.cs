//
// Script name: TrackPiece
//
//
// Programmer: Kentaurus
//

using UnityEngine;

namespace Track
{
    [System.Serializable]
    public class TrackInformation
    {
        [SerializeField, ReadOnly] public Vector3 Position;
        [SerializeField, ReadOnly] public float CurrentScale;


        public TrackInformation() { }
    }

    public class TrackPiece : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform m_StartLocation;
        [SerializeField] private Transform m_EndLocation;
        [SerializeField, ReadOnly] private TrackPiece m_PreviousTrack = null;
        [SerializeField, ReadOnly] private TrackPiece m_NextTrack = null;
        [SerializeField, ReadOnly] private TrackInformation m_TrackInformation;
#if UNITY_EDITOR
        protected Vector3? m_IntersectedPoint = Vector3.zero;
#endif

        protected bool m_IsInitialized = false;

        public Vector3 Right
        {
            get { return Vector3.Normalize( EndLocation.position - m_StartLocation.position );  }
        }

        public Transform StartLocation
        {
            get { return m_StartLocation; }
        }

        public Transform EndLocation
        {
            get { return m_EndLocation; }
        }

        public TrackInformation Information
        {
            get { return m_TrackInformation; }
        }
        #endregion

        #region Unity API
#if UNITY_EDITOR
        public void DrawGizmo()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_StartLocation.position, EndLocation.position);
            if (m_IntersectedPoint != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube((Vector3) m_IntersectedPoint, Vector3.one * 20f);
            }
        }
#endif
        #endregion

        #region Public Methods
        public virtual void Init()
        {
            if (!m_IsInitialized)
            {
                m_IsInitialized = true;
            }
        }

        public TrackPiece GetNextTrackPiece(Transform transform)
        {
            if (transform.position.x < m_StartLocation.position.x)
            {
                return m_PreviousTrack;
            }
            else if (transform.position.x > EndLocation.position.x)
            {
                return m_NextTrack;
            }
            return this;
        }

        public virtual void SetTrackInformation(TrackInformation information)
        {
            m_TrackInformation = information;
        }

        public virtual void SetNextTrack(TrackPiece piece)
        {
            m_NextTrack = piece;
        }

        public virtual void SetPreviousTrack(TrackPiece piece)
        {
            m_PreviousTrack = piece;
        }

        public virtual bool IsGoingToIntersect(Vector3 origin, Vector3 direction)
        {
            Vector3 position = GetIntersectedPositon(origin, direction);

            return IsGoingToIntersect(position);
        }

        public virtual Vector3 GetIntersectedPositon(Vector3 origin, Vector3 direction)
        {
            float trackSlop = GetSlope();
            float trackInitial = GetInitial(trackSlop);

            float slope = direction.y / direction.x;
            float initial = slope * origin.x - origin.y;

            float leftSide = trackInitial - initial;
            float rightSide = slope - trackSlop;


            Vector3 position = Vector3.zero;

            position.x = leftSide / rightSide;

            position.y = GetHeightOnTrack(position.x);

            position.z = origin.z;

            m_IntersectedPoint = (Vector3?) position + origin;

            return position;
        }

        public virtual bool IsGoingToIntersect(Vector3 position)
        {
            bool isGoingToIntersect = true;

            isGoingToIntersect &= StartLocation.position.x < position.x;
            isGoingToIntersect &= EndLocation.position.x > position.x;

            if (!isGoingToIntersect)
            {
                return false;
            }

            isGoingToIntersect &= GetHeightOnTrack(position.x) < position.y;

            return isGoingToIntersect;
        }

        public virtual float DeltaY(Vector3 position)
        {
            return position.y - GetHeightOnTrack(position.x);
        }

        public virtual float GetHeightOnTrack(float xPosition)
        {
            float slope = GetSlope();
            float initial = GetInitial(slope);

            return slope * xPosition + initial;
        }

        public virtual void Scale(float scale)
        {
            m_StartLocation.localPosition = m_StartLocation.localPosition * scale / m_TrackInformation.CurrentScale;
            m_EndLocation.localPosition = m_EndLocation.localPosition * scale / m_TrackInformation.CurrentScale;
            m_TrackInformation.CurrentScale = scale;
        }
        #endregion

        #region Protected Methods
        protected float GetSlope()
        {
            float numerator = m_EndLocation.position.y - m_StartLocation.position.y;
            float denominator = m_EndLocation.position.x - m_StartLocation.position.x;
            return numerator / denominator;
        }

        protected float GetInitial(float slope)
        {
            return m_StartLocation.position.y - (  slope * m_StartLocation.position.x);
        }
        #endregion

        #region Private Methods
        #endregion

    }
}