using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{

    public enum GroundType
    {
        Carpet = 0,
        Metal,
        Dirt,
        Stopped
    }

    [FMODUnity.EventRef]
    public string eventPath;
    public float footRadius = 2.0f;
    public Transform footOrigin;


    FMOD.Studio.EventInstance eventInstance;
    readonly string GROUND_TYPE_PARAM = "GroundType";
    bool checkGroundType = false;
    


    // Start is called before the first frame update
    void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        eventInstance.start();
        StopFootstepAudio();
    }

    public void StartFootstepAudio()
    {
        checkGroundType = true;
    }

    public void StopFootstepAudio()
    {
        checkGroundType = false;
        eventInstance.setParameterByName(GROUND_TYPE_PARAM, (float)GroundType.Stopped);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkGroundType)
        {
            Collider[] colliders = Physics.OverlapSphere(footOrigin.position, footRadius);
            foreach (var collider in colliders)
            {
                var ground = collider.transform.GetComponent<Ground>();
                if (ground)
                {
                    float curGroundType;
                    eventInstance.getParameterByName(GROUND_TYPE_PARAM, out curGroundType);
                    if ((float)ground.groundType != curGroundType)
                    {
                        eventInstance.setParameterByName(GROUND_TYPE_PARAM, (float)ground.groundType);
                        eventInstance.getParameterByName(GROUND_TYPE_PARAM, out curGroundType);
                    }
                    break;
                }
            }
        }
    }
}
