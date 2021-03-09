using System;
using RollingBall.Game.Memento;
using RollingBall.Game.Player;
using RollingBall.Game.StageObject;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RollingBall.Game.StageData
{
    public sealed class StageRepository
    {
        private readonly int _level;
        private readonly StageEntity _stageEntity;

        public StageRepository(int level, StageDataTable stageDataTable, StageObjectTable stageObjectTable,
            PlayerController player, Goal goal, Caretaker caretaker)
        {
            _level = level;
            _stageEntity = JsonUtility.FromJson<StageEntity>(stageDataTable.stageDataList[level].ToString());

            foreach (var stageObjectData in _stageEntity.stageObjects)
            {
                IStageObject stageObject;
                switch (stageObjectData.type)
                {
                    case StageObjectType.Player:
                        stageObject = player;
                        caretaker.AddMoveObject(player);
                        break;
                    case StageObjectType.Goal:
                        stageObject = goal;
                        break;
                    case StageObjectType.Block:
                    {
                        var data = stageObjectTable.stageObjectDataList
                            .Find(x => x.type == stageObjectData.type);
                        var block = Object.Instantiate(data.block);
                        stageObject = block as IStageObject;
                        break;
                    }
                    case StageObjectType.MoveBlock:
                    case StageObjectType.BallBlock:
                    {
                        var data = stageObjectTable.stageObjectDataList
                            .Find(x => x.type == stageObjectData.type);
                        var block = Object.Instantiate(data.block);
                        stageObject = block as IStageObject;
                        caretaker.AddMoveObject(block as IMoveObject);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                stageObject.SetPosition(stageObjectData.GetPosition());
            }
        }

        public int GetLevel() => _level;
        public int GetTargetMoveCount() => _stageEntity.targetMoveCount;
    }
}