using UnityEngine;
using Zenject;

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

    public bool EqualPosition(Vector2 roundPosition)
    {
        if (GetPosition() == roundPosition)
        {
            _clearAction.DisplayClearUi();
            return true;
        }

        return false;
    }
}