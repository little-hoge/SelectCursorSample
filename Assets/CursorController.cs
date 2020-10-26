using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
    public Transform Cursor;
    public Text SelectText;

    int CursorPoint;
    const int DefaultPos = 219;
    const int CursorOffset = 100;
    enum CommandList {
        ATTACK,
        DEFENCE,
        MAGIC,
        ITEM,
        COMANDMAX
    }


    void Update() {
        Vector3 pos = Cursor.localPosition;


        if (Input.GetKeyDown(KeyCode.Z)) {
            SelectText.text = "Select：" + (CommandList)CursorPoint;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            CursorPoint = (CursorPoint + (int)CommandList.COMANDMAX - 1) % (int)CommandList.COMANDMAX;
            pos.y = DefaultPos - CursorOffset * CursorPoint;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
           
            CursorPoint = (CursorPoint + 1) % (int)CommandList.COMANDMAX;
            pos.y = DefaultPos - CursorOffset * CursorPoint;
        }
        Cursor.localPosition = pos;
    }
}
