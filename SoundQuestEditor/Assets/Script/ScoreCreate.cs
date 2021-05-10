using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class ScoreCreate : MonoBehaviour
{
    [SerializeField] GameObject line;
    [SerializeField] LineRenderer horizontalLine;
    [SerializeField, Header("曲の長さ")] float endTime = 60;

    [SerializeField, Header("小節数")] int maxBarCount;
    [SerializeField, Header("拍子数")] int rhythm = 4;
    [SerializeField, Header("1小節に置く音符数")] int beat = 16;
    [SerializeField, Header("曲の速さ")] int bpm = 120;

    List<LineRenderer> lineList = new List<LineRenderer>();

    static readonly int kMaxBeat = 16;
    static readonly float kLongLine = 2;
    static readonly float kShortLine = 1.5f;
    static readonly float kHorizontalLineHeight = 1.5f;
    static readonly int kMaxLaneCaunt = 4;

    /// <summary> 横の線の最大数 </summary>
    public static int GetMaxLaneCaunt => kMaxLaneCaunt;

    /// <summary> 横のラインの最大高さ </summary>
    public static float GetHorizontalLineHeight => kHorizontalLineHeight;

    /// <summary> ノーツの位置 </summary>
    public static List<float> NotesTime { get; private set; } = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        endTime = SoundControl.EndTime;

        var maxCount = endTime * kMaxBeat;
        for (int i = 0; i < maxCount; i++)
        {
            var obj = Instantiate(line, transform);
            var lr = obj.GetComponent<LineRenderer>();
            lineList.Add(lr);
            obj.SetActive(false);

            //ノーツの位置を記録するための入れ物を作成
            NotesTime.Add(-1);
        }

        SetLine(0, beat, 0);
        SetHorizontalLine(kMaxLaneCaunt);
    }

    void SetLine(float elapsedTime, int beat, int resetLineCount)
    {
        //残り時間の計算
        var limitTime = endTime - elapsedTime;

        //残り時間から〇拍子様にLineの引き直す数を決める
        var maxCount = Calculation.GetMaxBarCount(limitTime, rhythm, bpm) * beat;

        //幅の計算
        var note = Calculation.GetBarTime(rhythm, bpm) / beat;
        Debug.Log(Calculation.GetBarTime(rhythm, bpm));

        //Lineを引く
        for (int i = resetLineCount; i < resetLineCount + (int)maxCount; i++)
        {
            var range = note * i;
            if (i % beat == 0)
            {
                lineList[i].SetPosition(0, new Vector3(range, 0, 0));
                lineList[i].SetPosition(1, new Vector3(range, kLongLine, 0));
                maxBarCount = i / beat;//とりあえず
            }
            else
            {
                lineList[i].SetPosition(0, new Vector3(range, 0, 0));
                lineList[i].SetPosition(1, new Vector3(range, kShortLine, 0));
            }
            lineList[i].gameObject.transform.localPosition = new Vector3(range, 0, 0);
            lineList[i].gameObject.SetActive(true);

            NotesTime[i] = range;
        }

        //いらないLineを消す
        for (int i = resetLineCount + (int)maxCount; i < lineList.Count; i++)
        {
            lineList[i].gameObject.SetActive(false);

            NotesTime[i] = -1;
        }
    }

    void SetHorizontalLine(int maxCount)
    {
        for (int i = 0; i < maxCount; i++)
        {
            var line = Instantiate(horizontalLine, transform);
            var h = kHorizontalLineHeight / maxCount;
            line.SetPosition(0, new Vector3(0, h * i, -0.1f));
            line.SetPosition(1, new Vector3(200, h * i, -0.1f));
        }
    }
}
