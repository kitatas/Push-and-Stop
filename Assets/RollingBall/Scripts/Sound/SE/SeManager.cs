using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SeManager : AudioInitializer
{
    [Inject] private readonly SeTable _seTable = default;
    private Dictionary<SeType, AudioClip> _seList = null;

    protected override void Awake()
    {
        base.Awake();

        _seList = new Dictionary<SeType, AudioClip>
        {
            {SeType.Button, _seTable.buttonClip},
            {SeType.Hit,    _seTable.hitClip},
            {SeType.Clear,  _seTable.clearClip},
        };
    }

    public void PlaySe(SeType seType)
    {
        PlayOneShot(_seList[seType]);
    }
}