using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanelManager : MonoBehaviour
{
    public string displayText;

    public void ShowInfoPanel()
    {
        Transform infoPanel = GameObject.Find("InfoPanel").transform;
        Transform textBox = infoPanel.GetChild(1);
        Debug.Log(displayText);
        textBox.GetComponent<TMP_Text>().SetText(displayText);
        infoPanel.localScale = new Vector3(1, 1);
    }

    public void CloseInfoPanel()
    {
        GameObject.Find("InfoPanel").transform.localScale = new Vector3(0, 0);
    }
}
