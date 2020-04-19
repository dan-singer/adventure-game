using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseText;
    [FMODUnity.EventRef]
    public string pauseEventPath;
    bool paused = false;
    FMOD.Studio.EventInstance pauseEventInstance;

    // Start is called before the first frame update
    void Start()
    {
        pauseText.SetActive(false);
        pauseEventInstance = FMODUnity.RuntimeManager.CreateInstance(pauseEventPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0.1f;
                pauseEventInstance.start();
            }
            else
            {
                Time.timeScale = 1.0f;
                pauseEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
            pauseText.SetActive(paused);
        }
    }
}
