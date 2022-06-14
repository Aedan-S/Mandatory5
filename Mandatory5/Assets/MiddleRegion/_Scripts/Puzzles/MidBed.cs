using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quests;

public class MidBed : MonoBehaviour
{
    private bool withinRange, activated;
    public Material[] skyboxes;
    [SerializeField] private CanvasGroup fadeCanvasGroup, worldCanvasGroup;
        
    private void Start()
    {
        //QuestMenuRenderer.currentWorld = Quest.World.ChickRepublic;
        //QuestManager.AddQuest(new Quest(Quest.World.ChickRepublic, "Go to sleep", 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!activated)
            {
                withinRange = true;
                worldCanvasGroup.alpha = 1;  
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!activated)
            {
                withinRange = false;
                worldCanvasGroup.alpha = 0;
            }
            
        }
    }


    private void Update()
    {
        if (!activated)
        {
            if (Input.GetKey(KeyCode.E) && withinRange)
            {
                activated = true;
                StartCoroutine("Sleeb");
            } 
        }
        
    }

    

    private IEnumerator Sleeb()
    {
        float elapsedTime = 0f;
        bool fadeIn = true;
        
        while (fadeCanvasGroup.alpha < 0.99f)
        {
            //fadeCanvasGroup.alpha += 0.00001f * Time.unscaledDeltaTime;
            
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / 2); 
            yield return null;
        }
        fadeIn = false;
        
        if (fadeIn == false)
        {
            GameObject.Find("Directional Light").GetComponent<Light>().color = new Color32(49, 44, 30, 1);
        
            RenderSettings.skybox = skyboxes[0];
            elapsedTime = 0f;
            while (fadeCanvasGroup.alpha > 0.01f)
            {
                fadeIn = false;
                elapsedTime += Time.deltaTime;
                fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / 1);
                yield return null;
            }
        }
        //activated = true;
        RiddleManager.Instance.RiddleSolved();
        QuestManager.SetNormalQuestStatus(2,true);
        worldCanvasGroup.alpha = 0;
        yield break;
        
    }
}