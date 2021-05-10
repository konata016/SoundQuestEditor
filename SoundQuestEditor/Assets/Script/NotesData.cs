using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class NotesData : MonoBehaviour
{
    public Define.NotesType NotesType;
    public Vector3 GetPos => transform.localPosition;
    public float GetSiz => transform.localScale.z + transform.localPosition.z;
}
