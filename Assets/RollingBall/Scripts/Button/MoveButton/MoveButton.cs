using System;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// プレイヤーの移動を行うボタン
/// </summary>
public sealed class MoveButton : BaseButton
{
    private readonly Subject<Vector3> _subject = new Subject<Vector3>();
    public IObservable<Vector3> OnPushed() => _subject;

    [SerializeField] private MoveDirection moveDirection = default;

    private IMoveCountUpdatable _moveCountUpdatable;
    private ICaretakerPushable _caretaker;

    [Inject]
    private void Construct(IMoveCountUpdatable moveCountUpdatable, ICaretakerPushable caretaker)
    {
        _moveCountUpdatable = moveCountUpdatable;
        _caretaker = caretaker;
    }

    protected override void OnPush(ButtonType buttonType)
    {
        base.OnPush(ButtonType.Decision);

        // 移動回数の更新
        _moveCountUpdatable.UpdateMoveCount(UpdateType.Increase);

        // 移動前の位置を保存
        _caretaker.PushMementoStack();

        _subject.OnNext(ConstantList.moveDirection[moveDirection]);
    }

    public void InteractButton(bool value)
    {
        button.interactable = value;
    }
}