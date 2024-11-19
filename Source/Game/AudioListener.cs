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
        //Audio manager referance
        _AudioManager = AudioManagerWrapper.Instance;
    }

    public override void OnUpdate()
    {
        //Set 3D attributes based on Actor
        Vector3 position = Actor.Position;
        Vector3 velocity = Vector3.Zero;
        Vector3 forward = Actor.Transform.Forward;
        Vector3 up = Actor.Transform.Up;

        _AudioManager.Set3DListenerAttributes(position, velocity, forward, up);
    }
}
