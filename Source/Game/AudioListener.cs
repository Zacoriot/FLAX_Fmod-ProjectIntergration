using FlaxEngine;

namespace Game;

/// <summary>
/// AudioListener Script.
/// </summary>
public class AudioListener : Script
{
    AudioManagerWrapper _AudioManager;

    public override void OnStart()
    {
        _AudioManager = AudioManagerWrapper.Instance;
    }

    public override void OnUpdate()
    {
        Vector3 position = Actor.Position;
        Vector3 velocity = Vector3.Zero;
        Vector3 forward = Actor.Transform.Forward;
        Vector3 up = Actor.Transform.Up;

        _AudioManager.Set3DListenerAttributes(position, velocity, forward, up);
    }
}
