// File: Observer
// Created by: Rigil Malubay

using UnityEngine;

public abstract class Observer : MonoBehaviour
{
	public abstract void OnNotify(Subject subject, object obj);
}