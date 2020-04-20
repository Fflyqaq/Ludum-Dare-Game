/****************************************************
    文件：EndGamePanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 14:32:28
    功能：结束游戏的界面
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePanel : PanelRoot 
{
    public Button btnCancel;
    public Button btnYes;

    private void Start()
    {
        btnCancel.onClick.AddListener(OnCancelBtnClick);
        btnYes.onClick.AddListener(OnYesBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();

        battleSystem.StopPlayerMove();
    }

    private void OnCancelBtnClick()
    {
        SetPanelState(false);
        battleSystem.ContinuePlayerMove();
    }

    private void OnYesBtnClick()
    {
        resService.AsyncLoadScene(ConstAttribute.loginSceneName, () =>
         {
             SetPanelState(false);
             enterExitSystem.GameBackToLoginPanel();
             battleSystem.GameBackToLoginPanel();
         });
    }
}