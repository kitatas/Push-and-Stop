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
                        stageObject = Object.Instantiate(stageObjectTable.normalBlock);
                        break;
                    case StageObjectType.MoveBlock:
                        var moveBlock = Object.Instantiate(stageObjectTable.moveBlock);
                        stageObject = moveBlock;
                        caretaker.AddMoveObject(moveBlock);
                        break;
                    case StageObjectType.BallBlock:
                        var ballBlock = Object.Instantiate(stageObjectTable.ballBlock);
                        stageObject = ballBlock;
                        caretaker.AddMoveObject(ballBlock);
                        break;
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