using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

    Animator anim;

    AudioClip flySound;

    AudioSource audioSource;

    void Start () {
        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        flySound = Resources.Load<AudioClip>("Sound/HELICOPTER_Flyby_Right_to_Left_Fast_stereo");

    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelDemo.Instance.OnHelicopterEscape(this);
        }
    }

    public void Begin()
    {
        anim.SetBool("begin", true);
        audioSource.clip = flySound;
        audioSource.Play();
    }
}
