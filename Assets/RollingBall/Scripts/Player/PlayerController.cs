using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerController : IInitializable
{
    [Inject] private readonly PlayerMover _playerMover = default;
    [Inject] private readonly PlayerRotate _playerRotate = default;

    private Image _moveButtonImage;
    private Image _rotateButtonImage;

    public void Initialize()
    {
        _moveButtonImage = _playerMover.moveButton.GetComponent<Image>();
        _rotateButtonImage = _playerRotate.rotateButton.GetComponent<Image>();
    }

    public void ActivatePlayerButton()
    {
        ActivateMoveButton(true);
        ActivateRotateButton(true);
    }

    public void DeactivatePlayerButton()
    {
        ActivateMoveButton(false);
        ActivateRotateButton(false);
    }

    private void ActivateMoveButton(bool value)
    {
        _playerMover.moveButton.enabled = value;
        _moveButtonImage.color = value ? Color.white : Color.gray;
    }

    private void ActivateRotateButton(bool value)
    {
        _playerRotate.rotateButton.enabled = value;
        _rotateButtonImage.color = value ? Color.white : Color.gray;
    }
}