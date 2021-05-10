using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyDefine
{

    public class Define
    {
        public enum NotesType
        {
            Unknown,
            Fall, Point, Glaze
        }

        public static readonly string GetNotesPrefabTag = "Notes";
        public static readonly string GetNotesParentName = "NotesParent";
    }

    public class Calculation
    {

        /// <summary>
        /// 拍子
        /// 
        /// rhythm: 拍子数
        /// </summary>
        public static float GetBeat(int rhythm)
        {
            return 4f / rhythm;
        }

        /// <summary>
        /// 1小節の時間
        /// 
        /// rhythm: 拍子数
        /// bpm:    曲の速さ
        /// </summary>
        public static float GetBarTime(int rhythm, int bpm)
        {
            return (float)rhythm * 60 / (float)bpm;
        }

        /// <summary>
        /// 残りの小節数
        /// 
        /// time:   現在の時間
        /// rhythm: 拍子数
        /// bpm:    曲の速さ
        /// </summary>
        public static int GetMaxBarCount(float time, int rhythm, int bpm)
        {
            return (int)(time / (60 * (float)rhythm / (float)bpm));
        }
    }

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 目的の値に最も近い値を返します (float)
        /// </summary>
        public static float Nearest(this IEnumerable<float> self, float target)
        {
            var min = self.Min(c => Math.Abs(c - target));
            return self.First(c => Math.Abs(c - target) == min);
        }
    }
}
