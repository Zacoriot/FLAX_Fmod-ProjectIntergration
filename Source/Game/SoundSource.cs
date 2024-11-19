using FlaxEngine;
using System;

namespace Game;

/// <summary>
/// SoundSource Script.
/// </summary>
public class SoundSource : Script
{
    AudioManagerWrapper _AudioManager;
    IntPtr _EventInstance;
    [ShowInEditor, Serialize] string _EventGUID;
    [ShowInEditor, Serialize] string _BankName;
    [ShowInEditor, Serialize] bool _PlayOnStart;


    public override void OnStart()
    {
        _AudioManager = AudioManagerWrapper.Instance;

        string bankPath = BankPaths.Path + _BankName;

        if(!_AudioManager.IsBankLoaded(bankPath))
        {
            _AudioManager.LoadBank(bankPath);
            Debug.Log($"{_BankName} has been loaded!");
        }

        CreateEventInstance();

        if(_PlayOnStart)
        {
            Play();
        }
    }

    private void CreateEventInstance()
    {
        if (string.IsNullOrEmpty(_EventGUID))
        {
            Debug.LogWarning("Event path is not set");
            return;
        }
        _EventInstance = _AudioManager.CreateEventInstance(_EventGUID);
    }

    public void Play()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.PlayEvent(_EventInstance);
        }
    }

    public bool IsPlaying()
    {
        if(_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.IsEventPlaying(_EventInstance);
        }

        return false;
    }

    public void Stop(int stopMode = 0)
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.StopEvent(_EventInstance, stopMode);
        }
    }

    public void SetParameter(string parameterName, float value)
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.SetEventParameter(_EventInstance, parameterName, value);
        }
    }

    public float GetMinDistance()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.GetMinDistance(_EventInstance);     
        }

        return 0;
    }

    public float GetMaxDistance()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.GetMaxDistance(_EventInstance);
        }

        return 0;
    }

    public override void OnUpdate()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.Set3DEventAttributes(_EventInstance, Actor.Position, Vector3.Zero);
        }
    }

    public override void OnDisable()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.ReleaseEventInstance(_EventInstance);
            _EventInstance = IntPtr.Zero;
        }
    }
}