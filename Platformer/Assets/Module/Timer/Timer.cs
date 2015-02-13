// File: Timer
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer 
{
	// constants
	private const float RATIO_COMPLETION = 1f;

	// enum
	private enum eTimerState
	{
		INACTIVE,
		STARTED,
		DONE,
		PAUSED,
		STOPPED

	}

	// public
	public delegate void OnDone();
	public delegate void OnUpdate();

	public OnDone m_OnDone;
	public OnUpdate m_OnUpdate;
	// protected

	// private
	private float m_Time;
	private float m_CompleteAt;
	private eTimerState m_State = eTimerState.INACTIVE;

	// properties
	public float Ratio
	{
		get
		{
			return m_Time / m_CompleteAt;
		}
	}

	public bool IsStarted
	{
		get
		{
			return (m_State == eTimerState.STARTED);
		}
	}
	#region Unity API
	#endregion

	#region Public Methods
	public Timer()
	{
		m_Time = 0;
	}

	public Timer(float time)
	{
		m_CompleteAt = time;
		m_Time = 0;
	}

	/// <summary>
	/// Used to start the timer. Can set a time here if not set in the new function call.
	/// </summary>
	/// <param name="time">Time.</param>
	public void Start(float time = 0)
	{
		if (time != 0)
		{
			m_CompleteAt = time;
		}
		m_Time = 0f;
		m_State = eTimerState.STARTED;
	}

	/// <summary>
	/// Used to stop the timer and reset it.
	/// </summary>
	public void Stop()
	{
		m_Time = 0;
		m_State = eTimerState.STOPPED;
	}

	/// <summary>
	/// Used to pause the timer.
	/// </summary>
	public void Pause()
	{
		m_State = eTimerState.PAUSED;
	}

	/// <summary>
	/// Used to resume the timer.
	/// </summary>
	public void Resume()
	{
		m_State = eTimerState.STARTED;
	}

	/// <summary>
	/// Must be called in the update function to enable the timer.
	/// </summary>
	public void Update()
	{
//		if (!PauseController.IsInstanceNull)
//		{
//			return;
//		}
		if (IsStarted)
		{
			if (Ratio < RATIO_COMPLETION)
			{
				if (m_OnUpdate != null)
				{
					m_OnUpdate();
				}
				m_Time += Time.deltaTime;
			}
			else
			{
				m_State = eTimerState.STOPPED;
				if (m_OnDone != null)
				{
					m_OnDone();
				}
			}
		}
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}