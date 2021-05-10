using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class NotesType : MonoBehaviour
{

    [SerializeField] Define.NotesType notesType;

    public void SetNotesType()
    {
        MusicScoreEditor.SetNotesType(notesType);
    }
}
