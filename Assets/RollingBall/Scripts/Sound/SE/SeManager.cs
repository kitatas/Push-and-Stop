using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class SeManager : AudioInitializer
{
    private Dictionary<SeType, AudioClip> _seList = null;

    [Inject]
    private void Construct(SeTable seTable)
    {
        _seList = new Dictionary<SeType, AudioClip>
        {
            {SeType.Button, seTable.buttonClip},
            {SeType.Hit,    seTable.hitClip},
            {SeType.Clear,  seTable.clearClip},
        };
    }

    public void PlaySe(SeType seType)
    {
        audioSource.PlayOneShot(_seList[seType]);
    }
}