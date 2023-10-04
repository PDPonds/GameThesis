using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainObserver : MonoBehaviour
{
    List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void ActiveAllObserver(ActionObserver action)
    {
        _observers.ForEach((_observers) => { _observers.FuncToDo(action); });
    }

}

public interface IObserver
{
    public void FuncToDo(ActionObserver action);
}

public enum ActionObserver
{
    PlayerHoldPunch, PlayerPunch, PlayerAttackHit
}