using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class MusicScoreEditor : MonoBehaviour
{
    [SerializeField] NotesPrefab notes;

    NotesPrefab tmpNotes;

    static Define.NotesType notesType = Define.NotesType.Fall;

    private void Awake()
    {
        MouseClick.OnLeftMouseButtonDown += OnLeftMouseButtonDown;
        MouseClick.OnLeftMouseButtonUp += OnLeftMouseButtonUp;
        MouseClick.OnRightMouseButtonDown += OnRightMouseButtonDown;
    }

    private void OnDestroy()
    {
        MouseClick.OnLeftMouseButtonDown -= OnLeftMouseButtonDown;
        MouseClick.OnLeftMouseButtonUp -= OnLeftMouseButtonUp;
        MouseClick.OnRightMouseButtonDown -= OnRightMouseButtonDown;
    }

    void OnLeftMouseButtonDown(Vector3 pos, GameObject obj)
    {
        tmpNotes = null;

        switch (notesType)
        {
            case Define.NotesType.Unknown: break;
            case Define.NotesType.Fall: tmpNotes = Instant(); break;
            case Define.NotesType.Point: Instant(); break;
            case Define.NotesType.Glaze: Instant(); break;
        }

        NotesPrefab Instant()
        {
            var s = Instantiate(notes, transform);
            s.Initialize(pos, notesType);
            return s;
        }
    }

    void OnLeftMouseButtonUp(Vector3 pos, GameObject obj)
    {
        if (tmpNotes == null)
        {
            tmpNotes = null;
            return;
        }

        tmpNotes.SetLength(pos.x);

        tmpNotes = null;
    }

    void OnRightMouseButtonDown(Vector3 pos, GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (obj.tag != Define.GetNotesPrefabTag)
        {
            return;
        }

        Destroy(obj);
    }

    public static void SetNotesType(Define.NotesType type)
    {
        notesType = type;
    }
}
