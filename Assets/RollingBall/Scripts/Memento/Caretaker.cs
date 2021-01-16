using System.Collections.Generic;
using RollingBall.StageObject;
using UnityEngine;

namespace RollingBall.Memento
{
    /// <summary>
    /// ステージ内で移動するオブジェクトの位置を保持
    /// </summary>
    public sealed class Caretaker : ICaretakerInitializable, ICaretakerPushable, ICaretakerPopable
    {
        private Stack<global::RollingBall.Memento.Memento[]> _mementoStack;
        private List<IMoveObject> _moveObjects;

        public void Initialize()
        {
            _mementoStack = new Stack<global::RollingBall.Memento.Memento[]>();

            _moveObjects = new List<IMoveObject>();
            foreach (var component in Object.FindObjectsOfType<Component>())
            {
                if (component is IMoveObject moveObject)
                {
                    _moveObjects.Add(moveObject);
                }
            }
        }

        /// <summary>
        /// 移動するオブジェクトの位置をStackに追加
        /// </summary>
        public void PushMementoStack()
        {
            var mementoArray = new global::RollingBall.Memento.Memento[_moveObjects.Count];
            for (int i = 0; i < _moveObjects.Count; i++)
            {
                mementoArray[i] = new global::RollingBall.Memento.Memento(_moveObjects[i].GetPosition());
            }

            _mementoStack.Push(mementoArray);
        }

        /// <summary>
        /// Stackから移動するオブジェクトの位置を更新
        /// </summary>
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
}