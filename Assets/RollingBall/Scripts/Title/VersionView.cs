using RollingBall.Common;
using TMPro;
using UnityEngine;

namespace RollingBall.Title
{
    public sealed class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI version = default;

        private void Start()
        {
            version.text = $"{Const.MAJOR_VERSION}.{Const.MINOR_VERSION}";
        }
    }
}