/****************************************************
    文件：LosePanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 22:5:1
    功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LosePanel : PanelRoot 
{
    //public Button btnRefresh;
    public Button btnBackToChoose;

    private void Start()
    {
        //btnRefresh.onClick.AddListener(OnRefreshBtnClick);
        btnBackToChoose.onClick.AddListener(OnBackToChooseBtnClick);

    }

    protected override void InitPanel()
    {
        base.InitPanel();

    }

    //刷新有一点问题，重新加载场景
    //private void OnRefreshBtnClick()
    //{
    //    SetPanelState(false);

    //    int nowCPNum = playGameSystem.NowCheckPointNum;
    //    string sceneName = Tools.UseNumCalculateSceneName(nowCPNum);

    //    playGameSystem.EnterGame(sceneName, nowCPNum);

    //}
    private void OnBackToChooseBtnClick()
    {
        SetPanelState(false);
        resService.AsyncLoadScene(ConstAttribute.loginSceneName, () =>
        {
            enterExitSystem.ShowChoosePanel();
        });
    }
}