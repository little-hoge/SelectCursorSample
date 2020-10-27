using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
    public Transform Cursor;
    public Text SelectText;
    [SerializeField] private List<Transform> Command = new List<Transform>();

    Vector2Int CursorPoint;
    const float DefaultPosX = -290;
    const float DefaultPosY = 80;
    const float OffsetX = 300;
    const float OffsetY = 100;
    const int CommandUpDownMax = 4;
    const int CommandLeftRightMax = 2;


    private void Start() {
        var CommandField = GameObject.Find("CommandField").transform;

        foreach (Transform child in CommandField) {
            Command.Add(child);
        }

    }


    void Update() {
        Vector3 pos = Cursor.localPosition;

        if (Input.GetKeyDown(KeyCode.Z)) {
            SelectText.text = Command[CursorPoint.x + CursorPoint.y].GetComponent<Text>().text + "を選択";

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            CursorPoint.y = (CursorPoint.y + (CommandUpDownMax - 1)) % CommandUpDownMax;
            pos.y = DefaultPosY - OffsetY * CursorPoint.y;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            CursorPoint.y = (CursorPoint.y + 1) % CommandUpDownMax;
            pos.y = DefaultPosY - OffsetY * CursorPoint.y;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            CursorPoint.x = (CursorPoint.x + CommandUpDownMax) % (CommandUpDownMax * CommandLeftRightMax);
            if (CursorPoint.x < CommandUpDownMax) pos.x = DefaultPosX;
            else pos.x = DefaultPosX + OffsetX;

        }

        Cursor.localPosition = pos;
    }
}
