/****************************************************
    文件：PanelRoot.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:53:46
    功能：界面基类
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class PanelRoot : MonoBehaviour 
{
    protected ResService resService = null;
    protected AudioService audioService = null;
    protected EnterExitSystem enterExitSystem = null;
    protected BattleSystem battleSystem = null;

    public void SetPanelState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
            SetActive(gameObject, isActive);
        //gameObject.SetActive(isActive);

        if (isActive)
            InitPanel();
        else
            ClearPanel();
    }

    /// <summary>
    /// 初始化界面
    /// </summary>
    protected virtual void InitPanel()
    {
        resService = ResService.Instance;
        audioService = AudioService.Instance;
        enterExitSystem = EnterExitSystem.Instance;
        battleSystem = BattleSystem.Instance;
    }
    /// <summary>
    /// 离开界面时的清理工作
    /// </summary>
    protected virtual void ClearPanel()
    {
    }

    #region 封装方法
    protected void SetText(Text txt, string content = "")
    {
        txt.text = content;
    }

    protected void SetSprite(Image image, string path)
    {
        Sprite sprite = ResService.Instance.LoadSprite(path, true);

        image.sprite = sprite;
    }

    protected void SetActive(GameObject go, bool state = true) { go.SetActive(state); }
    protected void SetActive(Transform trans, bool state = true) { trans.gameObject.SetActive(state); }
    protected void SetActive(RectTransform rectTrans, bool state = true) { rectTrans.gameObject.SetActive(state); }
    protected void SetActive(Image img, bool state = true) { img.transform.gameObject.SetActive(state); }
    protected void SetActive(Text txt, bool state = true) { txt.transform.gameObject.SetActive(state); }

    #endregion
}