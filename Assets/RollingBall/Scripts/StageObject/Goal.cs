using UnityEngine;
using Zenject;

namespace RollingBall.StageObject
{
    /// <summary>
    /// クリアの判定
    /// </summary>
    public sealed class Goal : MonoBehaviour, IStageObject, IGoal
    {
        private ClearAction _clearAction;

        [Inject]
        private void Construct(ClearAction clearAction)
        {
            _clearAction = clearAction;
        }

        public void SetPosition(Vector2 setPosition)
        {
            transform.position = setPosition;
        }

        private Vector2 GetPosition() => transform.position;

        public bool IsEqualPosition(Vector2 roundPosition)
        {
            if (GetPosition() == roundPosition)
            {
                _clearAction.DisplayClearUi();

                return true;
            }

            return false;
        }
    }
}