﻿//
// Script name: Character
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using Track;

public class Player : SubjectObserver
{
    #region Variables
    [SerializeField] protected InputController m_InputController;
    [SerializeField] protected TrackVehicle m_Vehicle;
    [SerializeField] protected float m_MovementVelocity = 75f;
    [SerializeField] protected float m_Gravity = 100f;
    [SerializeField, Range(0f, 1f)] protected float m_SpeedMofier = 1f;
    protected bool m_IsIntialized = false;

    public bool IsInAir
    {
        get { return !m_Vehicle.IsOnTrack;  }
    }
    #endregion

    #region Unity API
    protected void Update()
    {
        if (IsInAir)
        {
            transform.position += Vector3.down * m_Gravity * Time.deltaTime * m_SpeedMofier;
        }

        m_InputController.OnUpdate();
        m_Vehicle.OnUpdate();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (m_InputController != null)
        {
            m_InputController.RegisterObserver(this);
        }
    }
    #endregion

    #region Public Methods
    public virtual void Init()
    {
        if (!m_IsIntialized)
        {
            m_InputController.RegisterObserver(this);
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
