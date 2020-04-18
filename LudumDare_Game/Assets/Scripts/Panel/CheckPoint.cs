/****************************************************
    文件：CheckPoint.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 15:28:21
    功能：选择关卡
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour 
{
	private Button btnEnter;
    public int sceneNum;

	private void Start(){
		btnEnter = GetComponent<Button>();
		btnEnter.onClick.AddListener(OnEnterBtnClick);
	}

    public void OnEnterBtnClick()
    {
        string sceneName = "";

        if (sceneNum < 10)
            sceneName = "0" + sceneNum.ToString() + ConstAttribute.sceneGame;
        else
            sceneName = sceneNum.ToString() + ConstAttribute.sceneGame;

        PlayGameSystem.Instance.EnterGame(sceneName, sceneNum);
    }
}