using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameMode : MonoBehaviour {

    public static GameMode Instance { get; private set; }

    GameObject deadMask;

    AudioSource audio;

    public UnityEvent OnAlarmEvents;

    void Start () {
        print("GameMode start");
        Instance = this;
        deadMask = GameObject.Find("Canvas").transform.Find("DeadMask").gameObject;
        audio = GetComponent<AudioSource>();

        OnAlarmEvents = new UnityEvent();
    }
    
    public void GameOver()
    {
        deadMask.SetActive(true);
    }

    public void RestartLevel()
    {
        print("Restart " + SceneManager.GetActiveScene().name);
        string curSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(curSceneName);
    }

    private void OnDestroy()
    {
        print("GameMode OnDestroy");
        Instance = null;
    }

    public void Alarm()
    {
        if (audio.isPlaying)
        {
            return;
        }
        AudioClip clip = Resources.Load<AudioClip>("Sound/Alarm");

        audio.clip = clip;
        audio.Play();

        if (OnAlarmEvents != null)
        {
            OnAlarmEvents.Invoke();
        }
    }
}
