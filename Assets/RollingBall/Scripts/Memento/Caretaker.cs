using System.Collections.Generic;
using UnityEngine;

public sealed class Caretaker : MonoBehaviour
{
    private List<IMoveObject> _moveObjects;
    private Stack<List<Memento>> _mementoStack;

    public void Initialize()
    {
        _mementoStack = new Stack<List<Memento>>();

        _moveObjects = new List<IMoveObject>();
        foreach (var component in FindObjectsOfType<Component>())
        {
            if (component is IMoveObject moveObject)
            {
                _moveObjects.Add(moveObject);
            }
        }
    }

    public bool IsMementoStackEmpty() => _mementoStack.Count == 0;

    public void PushMementoStack()
    {
        var mementos = new List<Memento>();
        foreach (var moveObject in _moveObjects)
        {
            var memento = new Memento(moveObject.GetPosition());
            mementos.Add(memento);
        }

        _mementoStack.Push(mementos);
    }

    public void PopMementoStack()
    {
        var mementoList = _mementoStack.Peek();
        for (int i = 0; i < _moveObjects.Count; i++)
        {
            _moveObjects[i].SetPosition(mementoList[i].GetPosition());
        }

        _mementoStack.Pop();
    }
}