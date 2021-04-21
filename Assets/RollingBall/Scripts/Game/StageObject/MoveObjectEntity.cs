using System.Collections.Generic;

namespace RollingBall.Game.StageObject
{
    public sealed class MoveObjectEntity
    {
        private readonly List<IMoveObject> _moveObjects;

        public MoveObjectEntity()
        {
            _moveObjects = new List<IMoveObject>();
        }

        public List<IMoveObject> moveObjects => _moveObjects;

        public void Add(IMoveObject moveObject) => moveObjects.Add(moveObject);

        public int Count() => moveObjects.Count;

        public IMoveObject Get(int index) => moveObjects[index];
    }
}