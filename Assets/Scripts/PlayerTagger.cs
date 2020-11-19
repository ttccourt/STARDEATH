using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectsWithTag("Player")[0].transform);
    }
}