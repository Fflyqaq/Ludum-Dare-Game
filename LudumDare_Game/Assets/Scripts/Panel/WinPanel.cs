/****************************************************
    文件：WinPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 21:10:55
    功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class WinPanel : PanelRoot 
{
    //public Button btnNext;
    public Button btnBackToChoose;

    private void Start()
    {
        //btnNext.onClick.AddListener(OnNextCheckPointGameBtnClick);
        btnBackToChoose.onClick.AddListener(OnBackToChooseBtnClick);

    }

    protected override void InitPanel()
    {
        base.InitPanel();

    }

    //下一关没问题
    //private void OnNextCheckPointGameBtnClick()
    //{
    //    int sceneNum = Tools.CalculateMaxCanPlayNum();
    //    string sceneName = Tools.CalculateMaxCanPlay();

    //    playGameSystem.EnterGame(sceneName, sceneNum);

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