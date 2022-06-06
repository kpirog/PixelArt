using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectButton : MonoBehaviour
{
    private ProjectController projectController;
    public ProjectGrid projectGrid;

    void Start()
    {
        projectController = FindObjectOfType<ProjectController>();
    }
    public void CreateProject()
    {
        projectController.CreateProjectWithGrid(projectGrid);
    }
}
public enum ProjectGrid
{
    Grid_8x8, Grid_16x16, Grid_32x32
}
