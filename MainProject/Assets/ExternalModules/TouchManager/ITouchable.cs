//
// Script name: ITouchable
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace TouchAction
{
	public interface ITouchable
	{
		bool OnTouchBegin(TouchEvent evt);
		void OnTouchMoved(TouchEvent evt);
		void OnTouchEnded(TouchEvent evt);
	}
}