using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
    public Transform Cursor;
    public Text SelectText;
    [SerializeField] private List<Transform> Command = new List<Transform>();

    // テスト用
    [SerializeField] private List<Transform> Toggle = new List<Transform>();

    Vector2Int CursorPoint;
    const float DefaultPosX = -290;
    const float DefaultPosY = -25;
    const float OffsetX = 300;
    const float OffsetY = 100;
    const int CommandUpDownMax = 4;
    const int CommandLeftRightMax = 2;


    private void Start() {
        var CommandField = GameObject.Find("CommandField").transform;
        var ToggleField = GameObject.Find("ToggleField").transform;

        foreach (Transform child in CommandField) {
            Command.Add(child);
        }

        // テスト用
        foreach (Transform child in ToggleField) {
            Toggle.Add(child);
        }
    }


    void Update() {
        Vector3 pos = Cursor.localPosition;
        var cmdActiveFlag = false;
        if (Input.GetKeyDown(KeyCode.Z)) {
            SelectText.text = Command[CursorPoint.x + CursorPoint.y].GetComponent<Text>().text + "を選択";
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {

            for (; cmdActiveFlag != true;) {
                CursorPoint.y = (CursorPoint.y + (CommandUpDownMax - 1)) % CommandUpDownMax;
                pos.y = DefaultPosY - OffsetY * CursorPoint.y;
                cmdActiveFlag = IsCommandName(CursorPoint.y + CursorPoint.x);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            for (; cmdActiveFlag != true; ) {
                CursorPoint.y = (CursorPoint.y + 1) % CommandUpDownMax;
                pos.y = DefaultPosY - OffsetY * CursorPoint.y;
                cmdActiveFlag = IsCommandName(CursorPoint.y + CursorPoint.x);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            for (; cmdActiveFlag != true;) {
                CursorPoint.x = (CursorPoint.x + CommandUpDownMax) % (CommandUpDownMax * CommandLeftRightMax);
                if (CursorPoint.x < CommandUpDownMax) pos.x = DefaultPosX;
                else pos.x = DefaultPosX + OffsetX;
                cmdActiveFlag = IsCommandName(CursorPoint.y + CursorPoint.x);
            }
        }

        Cursor.localPosition = pos;
    }

    // Commandが有る時、true
    bool IsCommandName(int CursorPos) {
        return Command[CursorPos].GetComponent<Text>().text != "";
    }
        

    // ボタン押下時動作
    public void OnClicActiveCommand() {

        for (int index = 0; index < Toggle.Count; index++) {

            // OFF時
            if (Toggle[index].GetComponent<Toggle>().isOn) {
                var CmdCheck = 0;
                var ToggleText = Toggle[index].GetChild(1).GetComponent<Text>().text;
                // 既に登録済みか検索し、なかった場合
                for (int index2 = 0; index2 < Command.Count; index2++) {
                    if (Command[index2].GetComponent<Text>().text == ToggleText) {
                        CmdCheck = 1;
                        break;
                    }
                }
                // 空きに1つコマンド追加後、終了
                if (CmdCheck != 1) {
                    for (int index2 = 0; index2 < Command.Count; index2++) {
                        if (Command[index2].GetComponent<Text>().text == "") {
                            Command[index2].GetComponent<Text>().text = ToggleText;
                            break;
                        }
                    }
                }
            }

            // ON時
            else {
                var ToggleText = Toggle[index].GetChild(1).GetComponent<Text>().text;

                // 消去するコマンドを検索、発見時消去して検索中止
                for (int index2 = 0; index2 < Command.Count; index2++) {
                    if (Command[index2].GetComponent<Text>().text == ToggleText) {
                        Command[index2].GetComponent<Text>().text = "";
                        break;
                    }
                }
            }
        }
    }
}
