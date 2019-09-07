using UnityEngine;

public class InfoButton : BaseButton
{
    [SerializeField] private GameObject infoObj = null;
    [SerializeField] private bool isActive = default;

    protected override void OnPush()
    {
        base.OnPush();

        infoObj.SetActive(isActive);
    }
}