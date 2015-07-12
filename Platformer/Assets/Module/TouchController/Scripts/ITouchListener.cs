// File: ITouchListener.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITouchListener
{
	bool OnTouchBegin(TouchEvent evt);
	void OnTouchMove(TouchEvent evt);
	void OnTouchEnd(TouchEvent evt);
}