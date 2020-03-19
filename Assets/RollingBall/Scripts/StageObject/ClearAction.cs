using Zenject;

public sealed class ClearAction
{
    private ISeController _seController;
    private ClearText _clearText;
    private ClearRank _clearRank;
    private NextButton _nextButton;

    [Inject]
    private void Construct(ISeController seController, ClearText clearText, ClearRank clearRank, NextButton nextButton)
    {
        _seController = seController;
        _clearText = clearText;
        _clearRank = clearRank;
        _nextButton = nextButton;

        nextButton.Initialize();
    }

    public void DisplayClearUi()
    {
        _seController.PlaySe(SeType.Clear);

        _clearText.TweenClearText();

        _clearRank.DisplayClearRank();

        _nextButton.DisplayNextButton(_clearText.RectTransform());
    }
}