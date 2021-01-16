using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace RollingBall.Sound
{
    /// <summary>
    /// SEの音量を調整するSlider
    /// </summary>
    public sealed class SeSlider : Slider
    {
        private ISeController _seController;

        [Inject]
        private void Construct(ISeController seController)
        {
            _seController = seController;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _seController.PlaySe(SeType.DecisionButton);
        }
    }
}