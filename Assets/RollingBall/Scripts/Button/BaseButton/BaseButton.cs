using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// ボタン系の抽象クラス
/// </summary>
public abstract class BaseButton : MonoBehaviour
{
    private Button _button;

    protected Button button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            return _button;
        }
    }

    private ISeController _seController;

    [Inject]
    private void Construct(ISeController seController)
    {
        _seController = seController;
    }

    protected virtual void Awake()
    {
        button
            .OnClickAsObservable()
            .Subscribe(_ => OnPush(default))
            .AddTo(this);
    }

    protected virtual void OnPush(ButtonType buttonType)
    {
        _seController.PlaySe(GetSeType(buttonType));
    }

    private SeType GetSeType(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.Decision:
                return SeType.DecisionButton;
            case ButtonType.Cancel:
                return SeType.CancelButton;
            default:
                throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
        }
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
    }

    public bool IsInteractable() => button.interactable;
}