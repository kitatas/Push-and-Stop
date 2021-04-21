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
        public StageRepository(int level, StageDataTable stageDataTable, StageObjectTable stageObjectTable,
            PlayerController player, Goal goal, Caretaker caretaker, TargetMoveCountView targetMoveCountView)
        {
            var stageEntity = JsonUtility.FromJson<StageEntity>(stageDataTable.stageDataList[level].ToString());
            goal.SetTargetMoveCount(stageEntity.targetMoveCount);
            targetMoveCountView.Initialize(stageEntity.targetMoveCount);

            foreach (var stageObjectData in stageEntity.stageObjects)
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

                stageObject?.SetPosition(stageObjectData.GetPosition());
            }
        }
    }
}