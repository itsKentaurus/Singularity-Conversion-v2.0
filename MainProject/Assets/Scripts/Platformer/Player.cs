//
// Script name: Character
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using Track;

public class Player : InputController
{
    #region Variables
    [SerializeField] protected TrackVehicle m_Vehicle;
    [SerializeField] protected float m_Gravity = 100f;

    [SerializeField, ReadOnly] private Vector3 m_MoveDirection = Vector3.zero;
    [SerializeField] protected float m_JumpSpeed = 20f;
    [SerializeField] protected float m_Acceleration = 10f;
    [SerializeField] protected float m_MaxSpeed = 100f;
    [SerializeField] protected Vector3 m_NextPosition = Vector3.zero;
    protected bool m_IsIntialized = false;

    public Vector3 Velocity
    {
        get { return m_MoveDirection; }
        set { m_MoveDirection = value; }
    }
    #endregion

    #region Unity API
    public override void OnUpdate()
    {
        base.OnUpdate();

        m_IsGrounded = m_Vehicle.IsOnTrack;

        if (Input.GetAxis(GetAxis(eDirections.LEFT_HORIZONTAL_JS)) != 0f)
        {
            m_MoveDirection.x += m_Acceleration * Input.GetAxis(GetAxis(eDirections.LEFT_HORIZONTAL_JS));
            m_MoveDirection.x = Mathf.Clamp(m_MoveDirection.x, -m_MaxSpeed, m_MaxSpeed);
        }
        else
        {
            m_MoveDirection.x = 0f;
        }

        if (m_IsGrounded)
        {
            if (Input.GetKeyDown(GetKeyCode((int)eButtons.BOTTOM)))
            {
                m_Vehicle.SetTrackPiece(null);
                m_MoveDirection.y = m_JumpSpeed;
            }
            else
            {
                m_MoveDirection.y = 0f;
            }
        }
        else
        {
            m_MoveDirection.y -= m_Gravity * Time.deltaTime;
            m_Vehicle.SetTrackPiece(null);
        }


        transform.Translate(m_MoveDirection);

        m_Vehicle.OnUpdate();

        m_NextPosition.x = transform.position.x;
        m_NextPosition.y = m_Vehicle.TrackHeightPosition();
        m_NextPosition.z = transform.position.z;

        transform.position = m_NextPosition;

        m_NextPosition = Vector3.zero;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    #endregion

    #region Public Methods
    public virtual void Init()
    {
        if (!m_IsIntialized)
        {
            m_IsIntialized = true;
        }
    }

    public virtual void SetTrackPiece(TrackPiece track)
    {
        m_Vehicle.SetTrackPiece(track);
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region IObservable
    #endregion
}
