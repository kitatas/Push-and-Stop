using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class UnityAudioSeController : BaseAudioSource, ISeController
{
    private Dictionary<SeType, AudioClip> _seList;

    [Inject]
    private void Construct(UnityAudioSeTable unityAudioSeTable)
    {
        _seList = new Dictionary<SeType, AudioClip>
        {
            {SeType.DecisionButton, unityAudioSeTable.decisionButton},
            {SeType.CancelButton,   unityAudioSeTable.cancelButton},
            {SeType.Hit,            unityAudioSeTable.hit},
            {SeType.Clear,          unityAudioSeTable.clear},
        };
    }

    public void PlaySe(SeType seType)
    {
        if (_seList.ContainsKey(seType) == false)
        {
            return;
        }

        audioSource.PlayOneShot(_seList[seType]);
    }

    private void OnApplicationQuit()
    {
        ES3.Save<float>(ConstantList.seVolumeKey, GetVolume());
    }
}