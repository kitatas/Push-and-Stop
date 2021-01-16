using RollingBall.Button.BaseButton;
using RollingBall.Memento;
using RollingBall.MoveCounter;
using Zenject;

namespace RollingBall.Button
{
    /// <summary>
    /// 一手前に戻るボタン
    /// </summary>
    public sealed class UndoButton : BaseButton.BaseButton
    {
        private IMoveCountUpdatable _moveCountUpdatable;
        private ICaretakerPopable _caretaker;

        [Inject]
        private void Construct(IMoveCountUpdatable moveCountUpdatable, ICaretakerPopable caretaker)
        {
            _moveCountUpdatable = moveCountUpdatable;
            _caretaker = caretaker;

            InteractButton(false);
        }

        protected override void OnPush(ButtonType buttonType)
        {
            base.OnPush(ButtonType.Cancel);

            // 移動回数の更新
            _moveCountUpdatable.UpdateMoveCount(UpdateType.Decrease);

            // 保存した位置情報を削除
            _caretaker.PopMementoStack();

            // 保存した位置情報がない場合、ボタン無効化
            if (_caretaker.IsMementoStackEmpty())
            {
                InteractButton(false);
            }
        }

        public void InteractButton(bool value)
        {
            button.interactable = value;
        }
    }
}