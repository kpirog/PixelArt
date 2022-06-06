using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public List<Panel> panels;
    
    private void Start()
    {
        OpenPanel(panels[0]);
    }

    public void OpenPanel(Panel chosenPanel)
    {
        foreach (Panel panel in panels)
        {
            panel.gameObject.SetActive(panel == chosenPanel);
        }
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
