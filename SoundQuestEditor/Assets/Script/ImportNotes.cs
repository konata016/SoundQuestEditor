using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class ImportNotes : MonoBehaviour
{
    [SerializeField] GameObject parentNotes;

    [SerializeField] NotesPrefab notesPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (parentNotes == null)
        {
            return;
        }

        var notesData = parentNotes.GetComponentsInChildren<NotesData>();
        foreach (var notes in notesData)
        {
            var s = Instantiate(notesPrefab, transform);
            s.gameObject.SetActive(true);

            //横幅の修正
            var pos = notes.GetPos;
            var h = ScoreCreate.GetHorizontalLineHeight / ScoreCreate.GetMaxLaneCaunt;
            pos.x = (h * pos.x) + (h / 2);

            s.Initialize(new Vector3(pos.z, pos.x, -1f), notes.NotesType);

            if (notes.NotesType == Define.NotesType.Fall)
            {
                s.SetLength(notes.GetSiz);
            }
        }
    }
}
