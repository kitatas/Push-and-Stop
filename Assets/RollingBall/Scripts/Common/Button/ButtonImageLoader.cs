using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace RollingBall.Common.Button
{
    public sealed class ButtonImageLoader : MonoBehaviour
    {
        [SerializeField] private SpriteAtlas spriteAtlas = default;
        [SerializeField] private Image buttonImage = default;

        public void LoadButtonImage(string imageName)
        {
            buttonImage.sprite = spriteAtlas.GetSprite(imageName);
        }
    }
}