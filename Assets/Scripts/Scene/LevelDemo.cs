using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(3)]
public class LevelDemo : MonoBehaviour {

    public Barrier barrier;
    public EnemyCharacter gateKeeper;

    public static LevelDemo Instance { get; private set; }

    GameObject player;
    PlayerCharacter cha;

	void Start () {
        print("LevelDemo start");
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        cha = player.GetComponent<PlayerCharacter>();

        InitQuest();
        GameMode.Instance.OnAlarmEvents.AddListener(OnAlarm);
    }

    public void InitQuest()
    {
        var q = Quest.Instance;
        q.AddQuest("找到敌我识别设备");
        q.AddQuest("窃取识别码");
        q.AddQuest("安全撤退");
    }
	
	void Update ()
    {
        Collider[] colliders = Physics.OverlapSphere(barrier.transform.position, 5);
        bool tankIsNearGate = false;
        foreach (var c in colliders)
        {
            if (c.tag == "Tank")
            {
                tankIsNearGate = true;
            }
        }
        if (tankIsNearGate)
        {
            barrier.OpenGate();
        }
        else
        {
            barrier.CloseGate();
        }
	}

    bool foundRadar = false;

    public void OnRadarFound()
    {
        print("发现雷达");
        foundRadar = true;

        Quest.Instance.FinishQuest(0);
        Quest.Instance.FinishQuest(1);
    }

    public void OnHelicopterEscape(Helicopter heli)
    {
        if (foundRadar == false)
        {
            return;
        }
        cha.DisableSelf();
        heli.Begin();
        Quest.Instance.FinishQuest(2);

        StartCoroutine(MissionAccomplished());
    }

    IEnumerator MissionAccomplished()
    {
        yield return new WaitForSeconds(1.5f);

        AudioClip ma = Resources.Load<AudioClip>("Sound/mission_accomplished");
        cha.PlaySound(ma);

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Canvas/MissionAccomplished").GetComponent<Text>().enabled = true;
    }

    private void OnDestroy()
    {
        print("LevelDemo OnDestroy");
        Instance = null;
    }

    void OnAlarm()
    {
        print("LevelDemo Alarm");
        Transform root = GameObject.Find("Enemies").transform;
        //root.Find("AlarmGuard1").gameObject.SetActive(true);
        //root.Find("AlarmGuard2").gameObject.SetActive(true);
    }

}
