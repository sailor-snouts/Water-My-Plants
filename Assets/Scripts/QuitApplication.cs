using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("quiting");
            Application.Quit();
        }
    }
}
