using Zenject;

public sealed class ClearAction
{
    private ISeController _seController;
    private ClearText _clearText;
    private ClearRank _clearRank;
    private ClearButton _clearButton;

    [Inject]
    private void Construct(ISeController seController, ClearText clearText, ClearRank clearRank, ClearButton clearButton)
    {
        _seController = seController;
        _clearText = clearText;
        _clearRank = clearRank;
        _clearButton = clearButton;

        clearButton.Initialize();
    }

    public void DisplayClearUi()
    {
        _seController.PlaySe(SeType.Clear);

        _clearText.TweenClearText();

        _clearRank.DisplayClearRank();

        _clearButton.DisplayNextButton(_clearText.RectTransform());
    }
}