using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class ExportNotes : MonoBehaviour
{
    [SerializeField] PrefabDictionary data;
    [SerializeField] string prefabName = "a";

    GameObject parent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Test();
        }
    }

    void Test()
    {
        //昔のプレハブを消去する
        Destroy(parent);

        //入れ物作成
        parent = new GameObject(prefabName);
        parent.tag = Define.GetNotesParentName;
        parent.SetActive(false);
        parent.transform.parent = transform;

        //オブジェクトに変換
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Define.GetNotesPrefabTag);
        for (int i = 0; i < objs.Length; i++)
        {
            var s = objs[i].GetComponent<NotesPrefab>();

            var obj = Instantiate(data.GetTable()[s.GetNotesType], parent.transform);
            obj.NotesType = s.GetNotesType;

            //横幅の修正
            var h = ScoreCreate.GetHorizontalLineHeight / ScoreCreate.GetMaxLaneCaunt;
            var horizontal = 0f;
            for (int f = 0; f < ScoreCreate.GetMaxLaneCaunt; f++)
            {
                if (s.GetPos.y > h * f &&
                    s.GetPos.y <= h * (f + 1))
                {
                    horizontal = (float)f;
                    break;
                }
            }

            //横に伸びていたものを縦に配置する
            obj.transform.localPosition = new Vector3(horizontal, 0, s.GetPos.x);

            if (s.GetNotesType == Define.NotesType.Fall)
            {
                obj.transform.localScale = new Vector3(1, 1, s.GetSiz - s.GetPos.x);
            }
        }
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<Define.NotesType, NotesData, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<Define.NotesType, NotesData>
    {
        public Name2Prefab(Define.NotesType key, NotesData value) : base(key, value) { }
    }
}
