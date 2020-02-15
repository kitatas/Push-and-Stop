using UnityEngine;
using Zenject;

public sealed class GoalInfo : MonoBehaviour, IStageObject
{
    private ClearAction _clearAction;

    [Inject]
    private void Construct(ClearAction clearAction)
    {
        _clearAction = clearAction;
    }

    public void SetPosition(Vector2 initializePosition)
    {
        transform.position = initializePosition;
    }

    public bool EqualPosition(Vector2 roundPosition)
    {
        if ((Vector2) transform.position == roundPosition)
        {
            _clearAction.DisplayClearUi();
            return true;
        }

        return false;
    }
}