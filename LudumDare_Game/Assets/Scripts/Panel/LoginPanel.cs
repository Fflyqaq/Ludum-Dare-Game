/****************************************************
    文件：EndGamePanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:10:54
    功能：登陆界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : PanelRoot
{
    public Button btnEnterGame;

    protected override void InitPanel()
    {
        base.InitPanel();

        btnEnterGame.onClick.AddListener(OnEnterChoosePanelBtnClick);
    }

    private void OnEnterChoosePanelBtnClick()
    {
        EnterExitSystem.Instance.ShowChoosePanel();
        SetPanelState(false);
    }
}
