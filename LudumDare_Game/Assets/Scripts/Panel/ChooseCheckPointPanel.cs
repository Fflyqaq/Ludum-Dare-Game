/****************************************************
    文件：ChooseChackPointPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 15:11:6
    功能：关卡选择界面
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCheckPointPanel : PanelRoot 
{
    public Button btnBack;

    protected override void InitPanel()
    {
        base.InitPanel();

        btnBack.onClick.AddListener(OnBackBtnClick);

    }

    private void OnBackBtnClick()
    {
        enterExitSystem.BackToLoginPanel();
    }

}