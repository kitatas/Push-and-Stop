using RollingBall.Common;
using RollingBall.Common.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Title
{
    public sealed class PopupView : MonoBehaviour
    {
        [SerializeField] private PopupCanvas popupCanvas = default;
        [SerializeField] private Button showButton = default;
        [SerializeField] private Button hideButton = default;

        private void Awake()
        {
            popupCanvas.Hide(0.0f);
        }

        private void Start()
        {
            showButton
                .OnClickAsObservable()
                .Subscribe(_ => popupCanvas.Show(Const.POP_ANIMATION_SPEED))
                .AddTo(this);

            hideButton
                .OnClickAsObservable()
                .Subscribe(_ => popupCanvas.Hide(Const.POP_ANIMATION_SPEED))
                .AddTo(this);
        }
    }
}