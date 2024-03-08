using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class Quest : MonoBehaviour {

    public static Quest Instance { get; private set; }
    List<QuestData> quests;

    GameObject questPanel;
    List<GameObject> questsUI;

    public Sprite spriteComplete;
    public Sprite spriteNotComplete;

    public RectTransform btnToggleQuest;

    void Start () {
        print("Quest start");
        Instance = this;
        quests = new List<QuestData>();

        questPanel = GameObject.Find("QuestPanel");

        questsUI = new List<GameObject>();
        int i = 1;
        Transform go = questPanel.transform.Find("Text" + i);
        while (go != null)
        {
            questsUI.Add(go.gameObject);

            i++;
            go = questPanel.transform.Find("Text" + i);
        }
	}

    public void ToggleQuestPanel()
    {
        SetQuestPanelVisibility(!questPanel.activeInHierarchy);
    }

    public void SetQuestPanelVisibility(bool show)
    {
        if (show)
        {
            questPanel.SetActive(true);
            btnToggleQuest.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            questPanel.SetActive(false);
            btnToggleQuest.localScale = new Vector3(1, 1, 1);
        }
    }

    class QuestData
    {
        public bool complete;
        public string text;

        public QuestData(string text, bool complete)
        {
            this.text = text;
            this.complete = complete;
        }
    }

	
    public void AddQuest(string text)
    {
        quests.Add(new QuestData(text, false));
        RefreshQuest();
    }

    public void RemoveQuest(int index)
    {
        quests.RemoveAt(index);
        RefreshQuest();
    }

    public void FinishQuest(int index)
    {
        quests[index].complete = true;
        RefreshQuest();
    }

    public void RefreshQuest()
    {
        for (int i=0; i<questsUI.Count; i++)
        {
            GameObject go = questsUI[i];
            if (i >= quests.Count)
            {
                go.SetActive(false);
                continue;
            }
            go.SetActive(true);

            var label = go.GetComponent<Text>();
            label.text = quests[i].text;
            var img = go.GetComponentInChildren<Image>();
            if (quests[i].complete)
            {
                img.sprite = spriteComplete;
            }
            else
            {
                img.sprite = spriteNotComplete;
            }
        }
    }

    private void OnDestroy()
    {
        print("Quest OnDestroy");
        Instance = null;
    }
}
