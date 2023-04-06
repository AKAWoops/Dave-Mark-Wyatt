using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AreaTrigger : MonoBehaviour
{
    public string AreaNameText;
    public string AreaNumberText;
    public bool AreaLocked;

    public CanvasGroup AreaUI;
    public TextMeshProUGUI AreaNumber;
    public TextMeshProUGUI AreaName;
    public GameObject AreaLockedText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AreaNumber.text = "Area " + AreaNumberText;
            AreaName.text = AreaNameText;

            if(AreaLocked)
                AreaLockedText.SetActive(true);
            else
                AreaLockedText.SetActive(false);

            ShowUI(AreaUI);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AreaNumber.text = " ";
            AreaName.text = " ";
            HideUI(AreaUI);
        }
    }

    public void ShowUI(CanvasGroup c)
    { 
        c.alpha = 1.0f;
        c.blocksRaycasts = true;
        c.interactable = true;
    }

    public void HideUI(CanvasGroup c)
    {
        c.alpha = 0.0f;
        c.blocksRaycasts = false;
        c.interactable = false;
    }
}
