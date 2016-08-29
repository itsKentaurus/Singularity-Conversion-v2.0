//
// Script name: TouchEvent
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace TouchAction
{
	public class TouchEvent
	{
		public Vector3 StartPosition;
		public Vector3 CurrentPosition;
		public Vector3 LastPosition;
		public Vector3 DeltaPosition
		{
			get { return CurrentPosition - LastPosition;}
		}

		public TouchEvent()
		{
			Reset();
		}

		public void Reset()
		{
			StartPosition = Vector3.zero;
			CurrentPosition = Vector3.zero;
			LastPosition = Vector3.zero;

		}
	}
}