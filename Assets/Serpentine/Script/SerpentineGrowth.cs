using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentineGrowth : MonoBehaviour
{
    List<Vector3> body_position = new List<Vector3>();
    List<GameObject> body_parts = new List<GameObject>();
    GameObject body;
    [SerializeField] private GameObject body_prefab;
    [SerializeField] private int gap;

    // Update is called once per frame
    void Update()
    {
        //calling functions
        growPressed();
        if(growPressed()) growBody();
        bodyFollow();

    }

    bool growPressed()
    {
        return Input.GetKeyDown(KeyCode.P);
    }

    void growBody()
    {
        body = Instantiate(body_prefab);
        body_parts.Add(body);
    }

    void bodyFollow()
    {
        body_position.Insert(0, transform.position);
        int index = 0;
        foreach(GameObject body in body_parts){
            Vector3 point = body_position[Mathf.Min(index * gap, body_position.Count - 1)];
            body.transform.position = point;
            index++;
        }
    }
}
