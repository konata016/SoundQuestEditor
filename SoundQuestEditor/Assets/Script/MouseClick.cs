using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDefine;

public class MouseClick : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();

    static readonly float kMaxDisRay = 100f;
    static readonly float kDepth = -1f;

    /// <summary> マウス左クリックのイベントを登録 </summary>
    public static event Action<Vector3, GameObject> OnLeftMouseButtonDown;

    /// <summary> マウス左クリックを離した時のイベントを登録 </summary>
    public static event Action<Vector3, GameObject> OnLeftMouseButtonUp;

    /// <summary> マウス右クリックのイベントを登録 </summary>
    public static event Action<Vector3, GameObject> OnRightMouseButtonDown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsReyHit())
            {
                OnLeftMouseButtonDown?.Invoke(GetMousePos(), hitInfo.collider.gameObject);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (IsReyHit())
            {
                OnLeftMouseButtonUp?.Invoke(GetMousePos(), hitInfo.collider.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (IsReyHit())
            {
                OnRightMouseButtonDown(GetMousePos(), hitInfo.collider.gameObject);
            }
        }
    }

    bool IsReyHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, kMaxDisRay);
    }

    Vector3 GetMousePos()
    {
        var pos = hitInfo.transform.position;

        pos.y = GetMousePosY();
        pos.z = kDepth;

        return pos;
    }

    float GetMousePosY()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var y = Mathf.Clamp(pos.y, 0, ScoreCreate.GetHorizontalLineHeight);

        var h = ScoreCreate.GetHorizontalLineHeight / ScoreCreate.GetMaxLaneCaunt;
        for (int i = 0; i < ScoreCreate.GetMaxLaneCaunt; i++)
        {
            if (y > h * i &&
                y <= h * (i + 1))
            {
                return (h * i) + (h / 2);
            }
        }

        return 0;
    }
}
