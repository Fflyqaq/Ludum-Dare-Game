/****************************************************
    文件：Rabbit.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 17:27:37
    功能：兔子
*****************************************************/

using UnityEngine;

public class Rabbit : MonoBehaviour 
{
	private float moveDistance = 1;

	private int xDir;
	private int yDir;

	private LayerMask layerMask = 1 << 8;

	private bool isWindBlow = false;
	private float windStrength;
	private Vector3 targetPos;

	private void Start()
	{
		SetMoveDir(-1, 0);
	}

	private void Update()
	{
        RaycastHit2D forwardRay = Tools.RayCheck(transform.position + new Vector3(xDir, yDir,0), new Vector2(xDir,yDir), 0.2f, layerMask);
		if (forwardRay)
		{
			//Debug.Log(forwardRay.collider.gameObject.name);
			SetMoveDir(-xDir, -yDir);
		}

		if (isWindBlow)
		{
			WindBlow(targetPos);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Trap")
		{
			Debug.Log("你死了");
			BattleSystem.Instance.IsLose = true;
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		transform.position += new Vector3(xDir,yDir,0) * moveDistance;
	}

	public void WindBlowed(float strength)
	{
		isWindBlow = true;
		windStrength = strength;

		targetPos = transform.position + new Vector3(xDir, yDir, 0) * windStrength;
	}

	private void WindBlow(Vector3 targetPos)
	{
		transform.position = Vector3.Lerp(transform.position, targetPos, 4f);
		if (Mathf.Abs(transform.position.x-targetPos.x)<=0.001 && Mathf.Abs(transform.position.y - targetPos.y) <= 0.001)
		{
			transform.position = targetPos;
			isWindBlow = false;
		}
	}

	public void SetMoveDir(int x,int y)
	{
		xDir = x;
		yDir = y;
		if (x > 0)
		{
			transform.localRotation = new Quaternion(0, 180, 0, 0);
		}
		else
		{
			transform.localRotation = new Quaternion(0, 0, 0, 0);
		}

	}
}