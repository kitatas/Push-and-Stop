using System.Collections.Generic;
using UnityEngine;

public sealed class Caretaker : ICaretakerInitializable, ICaretakerPushable, ICaretakerPopable
{
    private Stack<Memento[]> _mementoStack;
    private List<IMoveObject> _moveObjects;

    public void Initialize()
    {
        _mementoStack = new Stack<Memento[]>();

        _moveObjects = new List<IMoveObject>();
        foreach (var component in Object.FindObjectsOfType<Component>())
        {
            if (component is IMoveObject moveObject)
            {
                _moveObjects.Add(moveObject);
            }
        }
    }

    public void PushMementoStack()
    {
        var mementoArray = new Memento[_moveObjects.Count];
        for (int i = 0; i < _moveObjects.Count; i++)
        {
            mementoArray[i] = new Memento(_moveObjects[i].GetPosition());
        }

        _mementoStack.Push(mementoArray);
    }

    public void PopMementoStack()
    {
        var mementoArray = _mementoStack.Peek();
        for (int i = 0; i < _moveObjects.Count; i++)
        {
            _moveObjects[i].SetPosition(mementoArray[i].GetPosition());
        }

        _mementoStack.Pop();
    }

    public bool IsMementoStackEmpty() => _mementoStack.Count == 0;
}