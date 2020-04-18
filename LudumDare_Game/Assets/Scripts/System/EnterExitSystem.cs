/****************************************************
    文件：NewBehaviourScript.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 14:46:56
    功能：进入退出界面
*****************************************************/

using UnityEngine;

public class EnterExitSystem : SystemRoot 
{
    public static EnterExitSystem Instance;

    public LoginPanel loginPanel;
    public ChooseCheckPointPanel chooseCheckPointPanel;
    public EndGamePanel endGamePanel;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;

        loginPanel.SetPanelState();
    }

    public void ShowChoosePanel()
    {
        chooseCheckPointPanel.SetPanelState();
    }

    public void HideChoosePanel()
    {
        chooseCheckPointPanel.SetPanelState(false);
    }

    public void BackToLoginPanel()
    {
        loginPanel.SetPanelState();
        chooseCheckPointPanel.SetPanelState(false);
    }
}