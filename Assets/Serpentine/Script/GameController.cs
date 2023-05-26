using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //framerate
    [SerializeField] int frame_rate = 60;
    //input variable
    bool escape_pressed;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = frame_rate;
    }

    // Update is called once per frame
    void Update()
    {
        //setting up input
        escape_pressed = Input.GetKey(KeyCode.Escape);
        //quit application
        if(escape_pressed)
            Application.Quit();
    }
}
