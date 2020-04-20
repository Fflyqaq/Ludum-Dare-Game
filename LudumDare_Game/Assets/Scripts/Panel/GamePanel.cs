/****************************************************
    文件：GamePanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 15:54:6
    功能：游戏界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelRoot 
{
    public Button btnExit;
    public Text txtWin;

    private void Start()
    {
        btnExit.onClick.AddListener(OnExitBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();

        //battleSystem.InitMapData();
    }

    private void OnExitBtnClick()
    {
        EnterExitSystem.Instance.endGamePanel.SetPanelState();
    }

    public void SetWinText(string winStr)
    {
        txtWin.text = winStr;
    }
}