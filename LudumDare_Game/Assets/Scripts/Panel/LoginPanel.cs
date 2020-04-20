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
    public Button btnTips;

    private void Start()
    {
        btnEnterGame.onClick.AddListener(OnEnterChoosePanelBtnClick);
        btnTips.onClick.AddListener(OnShowTipsPanelBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();
       
        audioService.PlayBGMusic(ConstAttribute.AudioBGM1_4);
    }

    private void OnEnterChoosePanelBtnClick()
    {
        EnterExitSystem.Instance.ShowChoosePanel();
        SetPanelState(false);

        audioService.PlayUIAudio(ConstAttribute.AudioUIClick);
    }
    private void OnShowTipsPanelBtnClick()
    {
        enterExitSystem.ShowTipsPanel();
        audioService.PlayUIAudio(ConstAttribute.AudioUIClick);
    }
}
