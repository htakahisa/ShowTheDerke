using System.Collections.Generic;
using UnityEngine;

public class DerkeManager : MonoBehaviour
{
    public static DerkeManager derkeManager;

    public GameObject buyPanel;
    public GameObject havePanel;
    public GameObject Panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        derkeManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Panel.SetActive(!Panel.activeSelf);
        }
    }


}
