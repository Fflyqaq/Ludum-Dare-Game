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
using UnityEngine.SceneManagement;

public class ResService : MonoBehaviour 
{
    //单例模式
    public static ResService Instance = null;

    private Action loadSceneProgress;

    public void InitSvc()
    {
        Instance = this;

        InitTalkXML(ConstAttribute.talkDataPath);
        InitTalkXML(ConstAttribute.talkChooseDataPath);
        InitMapXML(ConstAttribute.mapDataPath);
    }

    private void Update()
    {
        loadSceneProgress?.Invoke();
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">要打开的场景名</param>
    /// <param name="loaded">加载完场景后要做的事</param>
    public void AsyncLoadScene(string sceneName, Action loaded)
    {
        //异步加载场景
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        loadSceneProgress = () =>
        {
            //当前异步操作加载的进度
            float progress = sceneAsync.progress;
            //加载完成后置空并隐藏loading界面
            if (progress == 1)
            {
                loaded?.Invoke();

                loadSceneProgress = null;
                sceneAsync = null;
            }
        };
    }

    #region 加载资源
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
    #endregion

    #region 读取对话信息XML
    private Dictionary<int, TalkData> talkDict = new Dictionary<int, TalkData>();
    /// <summary>
    /// 初始化XML中数据
    /// </summary>
    private void InitTalkXML(string path)
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

    #region 读取地图配置XML
    private Dictionary<int, MapData> mapDataDict = new Dictionary<int, MapData>();
    private void InitMapXML(string path)
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
                MapData mapData = new MapData
                {
                    ID = id,
                };

                foreach (XmlElement ele in nodeList[i].ChildNodes)
                {
                    switch (ele.Name)
                    {
                        case "sceneName":
                            mapData.sceneName = ele.InnerText;
                            break;
                        case "playerBornPos":
                            {
                                string[] temp = ele.InnerText.Split(',');
                                mapData.playerBornPos = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
                            }
                            break;
                        case "rabbitBornPos":
                            {
                                string[] temp = ele.InnerText.Split(',');
                                mapData.rabbitBornPos = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
                            }
                            break;
                        case "rabbitBornRota":
                            {
                                string[] temp = ele.InnerText.Split(',');
                                mapData.rabbitBornRota = new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                            }
                            break;
                        case "winCondition":
                            {
                                mapData.winCondition = ele.InnerText;
                            }
                            break;
                    }
                }

                mapDataDict.Add(id, mapData);
            }
        }
    }
    public MapData GetMapData(int id)
    {
        MapData data = null;
        mapDataDict.TryGetValue(id, out data);
        return data;
    }

    #endregion


}