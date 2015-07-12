// File: IObserver.cs
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IObserver
{
	void RegisterSubjects();
	void OnNotify(ISubject subject, object args = null);
}