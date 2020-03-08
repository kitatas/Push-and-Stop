using UniRx;
using UnityEngine;
using Zenject;

public sealed class Goal : MonoBehaviour, IStageObject, IGoal
{
    private ReactiveProperty<bool> _isClear;

    [Inject]
    private void Construct(ClearAction clearAction)
    {
        _isClear = new ReactiveProperty<bool>(false);
        _isClear
            .Where(x => x)
            .Subscribe(_ => clearAction.DisplayClearUi());
    }

    public void SetPosition(Vector2 setPosition)
    {
        transform.position = setPosition;
    }

    private Vector2 GetPosition() => transform.position;

    public bool IsEqualPosition(Vector2 roundPosition)
    {
        var isEqual = GetPosition() == roundPosition;
        _isClear.Value = isEqual;
        return isEqual;
    }
}