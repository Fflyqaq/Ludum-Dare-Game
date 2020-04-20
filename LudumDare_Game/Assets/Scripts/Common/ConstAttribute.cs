/****************************************************
    文件：PathDefine.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 14:16:55
    功能：常量的定义
*****************************************************/

using UnityEngine;

public class ConstAttribute
{
    public const string loginSceneName = "Scene_Login";

    public const string talkDataPath = "ResXML/talk";
    public const string talkChooseDataPath = "ResXML/talkChoose";
    public const string mapDataPath = "ResXML/map";


    public const string playerSpritePath = "Player/player_icon";
    public const string rabbitSpritePath = "Player/rabbit_icon";
    public const string playerPrefabPath = "PrefabPlayer/Player";
    public const string rabbitPrefabPath = "PrefabPlayer/Rabbit";
    public const string arrowPrefabPath = "PrefabTrap/Arrow";

    public const string sceneGame = "-Game";

    public const string checkPointDBName = "CheckPoint";
    public const string checkPointDBMsg = "10000";
    public const string maxCheckPointDBName = "MaxCheckPoint";
    public const int maxCheckPointDBMsg = 5;

    #region audio地址
    public const string AudioBGM1_4 = "bgm1_4"; 
    public const string AudioBGM5 = "bgm5"; 
    public const string AudioUIClick = "btnClick"; 
    #endregion

    #region Sprite地址
    public const string switchOnSpritePath = "Sprite/UI/Trap/sc_0001s_0001_switch_on"; 
    public const string switchOffSpritePath = "Sprite/UI/Trap/sc_0001s_0000_switch_off";

    public const string trapOnSpritePath = "Sprite/UI/Trap/sc_0000s_0001_tp2";
    public const string trapOffSpritePath = "Sprite/UI/Trap/sc_0000s_0001_tp4";
    #endregion
}