using RollingBall.Common;
using RollingBall.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Title
{
    public sealed class PopupAllHideView : MonoBehaviour
    {
        [SerializeField] private PopupCanvas[] popupCanvasArray = default;
        [SerializeField] private Button hideButton = default;

        private void Start()
        {
            hideButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    foreach (var popupCanvas in popupCanvasArray)
                    {
                        popupCanvas.Hide(Const.POP_ANIMATION_SPEED);
                    }
                })
                .AddTo(this);
        }
    }
}