/****************************************************
    文件：MapManager.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 12:37:52
    功能：地图管理器
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour 
{
	public static MapManager Instance;

	public List<ShootArrow> shootArrowList = new List<ShootArrow>();

	private void Start(){
		Instance = this;
	}
	private void Update(){
		
	}

	public void CheckIfPassGame()
	{
		if (shootArrowList.Count > 0)
		{
			bool isPass = true;
			foreach (var item in shootArrowList)
			{
				if (item.isCanShoot == true)
				{
					isPass = false;
					break;
				}
			}

			if (isPass)
			{
				BattleSystem.Instance.ShowNextTalk();
			}
		}
	}

	public void CloseAllArrows()
	{
		if (shootArrowList.Count > 0)
		{
			foreach (var item in shootArrowList)
			{
				item.CloseArrow();
			}
		}
	}
	public void OpenAllArrows()
	{
		if (shootArrowList.Count > 0)
		{
			foreach (var item in shootArrowList)
			{
				item.OpenArrow();
			}
		}
	}

	public void InitMap(MapData mapData)
	{
		
	}
}