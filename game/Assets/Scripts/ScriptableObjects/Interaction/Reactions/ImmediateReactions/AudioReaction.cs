using UnityEngine;

// This Reaction is used to play sounds through a given AudioSource.
// Since the AudioSource itself handles delay, this is a Reaction
// rather than an DelayedReaction.
public class AudioReaction : Reaction
{

    [FMODUnity.EventRef]
    public string eventPath;
    public bool setParameter = false;
    public string parameter;
    public float parameterValue;
    FMOD.Studio.EventInstance eventInstance;

    protected override void ImmediateReaction()
    {
        if (string.IsNullOrEmpty(eventPath))
        {
            return;
        }
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        if (!eventInstance.isValid())
        {
            return;
        }
        if (setParameter)
        {
            eventInstance.setParameterByName(parameter, parameterValue);
        }

        eventInstance.start();
    }
}