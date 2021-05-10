using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class NotesPrefab : MonoBehaviour
{
    [SerializeField] Define.NotesType notesType;
    [SerializeField] LineRenderer line;
    [SerializeField] BoxCollider collider;

    public Define.NotesType GetNotesType => notesType;

    public Vector3 GetPos => line.GetPosition(0);

    public float GetSiz => line.GetPosition(1).x;

    public void Initialize(
        Vector3 pos,
        Define.NotesType type = Define.NotesType.Unknown,
        float length = 0)
    {
        notesType = type;
        transform.localPosition = pos;
        line.SetPosition(0, pos);
        line.SetPosition(1, pos);

        line.startColor = SetColor(type);
        line.endColor = SetColor(type);
    }

    Color SetColor(Define.NotesType type)
    {
        switch (type)
        {
            case Define.NotesType.Unknown: break;
            case Define.NotesType.Fall: return Color.gray;
            case Define.NotesType.Point: return Color.yellow;
            case Define.NotesType.Glaze: return Color.green;
        }

        return Color.black;
    }

    public void SetLength(float length)
    {
        var pos = line.GetPosition(1);
        line.SetPosition(1, new Vector3(length, pos.y, pos.z));

        var siz = collider.size;
        siz.x = length;
        collider.size = siz;

        pos = collider.center;
        pos.x = length / 2;
        collider.center = pos;
    }
}
