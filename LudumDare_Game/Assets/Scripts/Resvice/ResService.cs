/****************************************************
    文件：ResResvice.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:31:12
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ResService : MonoBehaviour 
{
    //单例模式
    public static ResService Instance = null;

    public void InitSvc()
    {
        Instance = this;

        InitAutoGuide(ConstAttribute.talkDataPath);
    }


    private Dictionary<string, GameObject> cacheGODict = new Dictionary<string, GameObject>();
    /// <summary>
    /// 加载游戏物体
    /// </summary>
    public GameObject LoadPrefab(string path, bool isCache = false)
    {
        GameObject temp = null;
        if (!cacheGODict.TryGetValue(path, out temp))
        {
            temp = Resources.Load<GameObject>(path);
            if (isCache)
                cacheGODict.Add(path, temp);
        }

        GameObject go = null;
        if (temp != null)
            go = GameObject.Instantiate(temp);
        return go;
    }

    private Dictionary<string, AudioClip> audioCacheDict = new Dictionary<string, AudioClip>();
    /// <summary>
    /// 加载音效
    /// </summary>
    /// <param name="cache">是否需要缓存这个音效</param>
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip audio = null;
        if (audioCacheDict.TryGetValue(path, out audio) == false)
        {
            audio = Resources.Load<AudioClip>(path);
            if (cache)
            {
                audioCacheDict.Add(path, audio);
            }
        }

        return audio;
    }
    private Dictionary<string, Sprite> cacheSpriteDict = new Dictionary<string, Sprite>();
    /// <summary>
    /// 加载图片
    /// </summary>
    public Sprite LoadSprite(string path, bool cache = false)

    {
        Sprite sprite = null;
        if (cacheSpriteDict.TryGetValue(path, out sprite) == false)
        {
            sprite = Resources.Load<Sprite>(path);
            if (cache)
            {
                cacheSpriteDict.Add(path, sprite);
            }
        }

        return sprite;
    }

    #region 读取对话信息XML
    private Dictionary<int, TalkData> talkDict = new Dictionary<int, TalkData>();
    /// <summary>
    /// 初始化XML中数据
    /// </summary>
    private void InitAutoGuide(string path)
    {
        TextAsset nameText = Resources.Load<TextAsset>(path);
        if (nameText == null)
        {
            Debug.LogError("xml file:" + path + "not exist");
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(nameText.text);

            XmlNodeList nodeList = doc.SelectSingleNode("root").ChildNodes;

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement element = nodeList[i] as XmlElement;
                if (element.GetAttributeNode("ID") == null)
                    continue;

                int id = Convert.ToInt32(element.GetAttributeNode("ID").InnerText);
                TalkData talkData = new TalkData
                {
                    ID = id
                };

                foreach (XmlElement ele in nodeList[i].ChildNodes)
                {
                    switch (ele.Name)
                    {
                        case "talk":
                            talkData.talk = ele.InnerText;
                            break;
                    }
                }

                talkDict.Add(id, talkData);
            }
        }
    }
    /// <summary>
    /// 获取每项数据
    /// </summary>
    public TalkData GetTalkData(int id)
    {
        TalkData agc = null;
        if (talkDict.TryGetValue(id, out agc))
        {
            return agc;
        }
        return null;
    }
    #endregion
}