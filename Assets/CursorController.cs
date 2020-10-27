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
    Queue<string> ActiveCommandList = new Queue<string>();
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
            for (; cmdActiveFlag != true;) {
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

    // コマンドが有る時、trueを返す
    bool IsCommandName(int CursorPos) {
        return Command[CursorPos].GetComponent<Text>().text != "";
    }


    // ボタン押下時動作
    public void OnClicActiveCommand() {
        ActiveCommandList.Clear();

        // 有効コマンドを保存
        for (int index = 0; index < Toggle.Count; index++) {
            if (Toggle[index].GetComponent<Toggle>().isOn) {
                var ToggleText = Toggle[index].GetChild(1).GetComponent<Text>().text;
                ActiveCommandList.Enqueue(ToggleText);
            }
        }

#if UNITY_EDITOR
        // 有効コマンドリスト確認
        Debug.Log(string.Join(", ", ActiveCommandList));
#endif

        // 可変コマンド(たたかう、アイテム以外)初期化
        for (int index = 1; index < Command.Count; index++) {

            if (index == 3) continue;

            // アイテム以外初期化
            Command[index].GetComponent<Text>().text = "";
            
            // 有効コマンド設定
            if (ActiveCommandList.Count != 0) {
                Command[index].GetComponent<Text>().text = ActiveCommandList.Dequeue();
            }
        }
    }
}
