/****************************************************
    文件：BaseData.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:45:38
    功能：Nothing
*****************************************************/

using UnityEngine;

public class BaseData : MonoBehaviour 
{

}

public class TalkData
{
    public int ID;
    public string talk; //对话信息
}



public class MapData
{
    public int ID;
    public string sceneName;
    public int checkPointNum;
    public Vector3 playerBornPos;
    public Vector3 rabbitBornPos;

}
