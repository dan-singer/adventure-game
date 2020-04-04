using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{

    public enum GroundType
    {
        Carpet = 0,
        Metal,
        Dirt
    }

    [FMODUnity.EventRef]
    public string eventPath;
    public float timeBetweenSteps = 0.43f;
    public float footRadius = 2.0f;
    public Transform footOrigin;


    FMOD.Studio.EventInstance eventInstance;
    readonly string GROUND_TYPE_PARAM = "GroundType";
    float timer = 0.0f;

    


    // Start is called before the first frame update
    void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
    }

    public void LoopFootstepAudio()
    {
        Collider[] colliders = Physics.OverlapSphere(footOrigin.position, footRadius);
        foreach (var collider in colliders)
        {
            var ground = collider.transform.GetComponent<Ground>();
            if (ground)
            {
                eventInstance.setParameterByName(GROUND_TYPE_PARAM, (float)ground.groundType);
                break;
            }
        }
        if (timer > timeBetweenSteps)
        {
            eventInstance.start();
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }

    public void StopFootstepAudio()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
