// File: ISubject
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISubject {
	void RegisterObserver(IObserver o);
	void NotifyObervers(object obj = null);
}