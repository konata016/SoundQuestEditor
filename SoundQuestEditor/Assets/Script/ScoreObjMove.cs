using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.x = SoundControl.Audio.time;
        transform.position = pos;
    }
}
