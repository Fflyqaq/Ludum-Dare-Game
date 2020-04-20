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
	public List<ShootArrow> shootArrowList = new List<ShootArrow>();

	
	private void Start(){
		
	}
	private void Update(){
		
	}

	public void InitMap(MapData mapData)
	{
		
	}

	public void CheckArrowIsCanShoot()
	{
		foreach (ShootArrow shootArrow in shootArrowList)
		{
			if (shootArrow != null)
			{
				shootArrow.ReduceCountDown();
			}
		}
	}
}