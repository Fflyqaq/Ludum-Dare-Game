/****************************************************
    文件：ShootArrow.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 12:30:15
    功能：射箭
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootArrow : TrapRoot
{
	public Vector3 arrowtDir;

	//暂停时候的关闭  和  触发开关时候的改变
	private bool isClose = false;
	public bool isCanShoot = true;

	public Vector2 shootDir;
	public float shootSpeed;

	public float shootTime = 3;
	private float shootTimer = 0;

	private void Update(){
		if (isCanShoot && !isClose)
		{
			shootTimer += Time.deltaTime;
			if (shootTimer >= shootTime)
			{
				Shoot();
				shootTimer = 0;
			}
		}
	}

	private GameObject InitArrow()
	{
		GameObject arrow = Resources.Load<GameObject>(ConstAttribute.arrowPrefabPath);
		arrow = Instantiate(arrow);
		
		arrow.transform.parent = transform;
		arrow.transform.localPosition = Vector3.zero;
		arrow.transform.localEulerAngles = new Vector3(0, 0, arrowtDir.z);
		arrow.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

		return arrow;
	}

	private void Shoot()
	{
		GameObject arrow = InitArrow();
		arrow.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
	}

	public override void TriggerSwitch()
	{
		isCanShoot = !isCanShoot;

		StartCoroutine(DelayCheck());
	}
	IEnumerator DelayCheck()
	{
		yield return new WaitForSeconds(0.01f);
		MapManager.Instance.CheckIfPassGame();
	}

	public void CloseArrow()
	{
		//人物停止移动时，关闭陷阱
		isClose = true;
		Debug.Log(isClose);
	}
	public void OpenArrow()
	{
		isClose = false;
		Debug.Log(isClose);
	}
}