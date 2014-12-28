// File: GameController
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : Observer {
	
	// const

	// enum

	// public

	// protected

	// private

	#region Unity API
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

	#region Observer Implementation
	public override void OnNotify (Subject subject, object args)
	{
		if (subject is StageController)
		{
			if (args is string)
			{
				if (args.ToString() == GameConstants.PLAYER_LOADED)
				{
					Debug.Log(GameConstants.PLAYER_LOADED);
				}
			}
		}
	}
	#endregion
}