/****************************************************
    文件：TipsPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 10:5:15
    功能：提示界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : PanelRoot 
{
    public Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(OnCloseBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();

    }

    private void OnCloseBtnClick()
    {
        SetPanelState(false);
        audioService.PlayUIAudio(ConstAttribute.AudioUIClick);
    }
}