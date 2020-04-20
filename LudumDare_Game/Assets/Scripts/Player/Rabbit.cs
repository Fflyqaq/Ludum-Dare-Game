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
	//取消被风吹后兔子移动

	private float moveDistance = 1;

	private int xDir;
	private int yDir;

	private LayerMask collLayerMask = 1 << 8;
	private LayerMask boxLayerMask = 1 << 9;
	private float collLayerMaskDistance = 0.2f;
	private float boxLayerMaskDistance = 0.7f;

	private Animator anim;


	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
        RaycastHit2D forwardCollRay = Tools.RayCheck(transform.position + new Vector3(xDir, yDir,0), new Vector2(xDir,yDir), collLayerMaskDistance, collLayerMask);

		//遇见coll，改变方向
		if (forwardCollRay)
		{
			SetMoveDir(-xDir, -yDir);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Trap")
		{
			collision.GetComponent<TrapRoot>().TriggerSwitch();
			BattleSystem.Instance.IsLose = true;

			anim.SetBool("Die",true);
			Destroy(gameObject, 2f);
		}
	}
	/// <summary>
	/// player移动 rabbit移动
	/// </summary>
	public void Move()
	{
		bool isCanMove = true;

		//前面是否有箱子,箱子是否能移动
		RaycastHit2D forwardBoxRay = Tools.RayCheck(transform.position, new Vector2(xDir, yDir), boxLayerMaskDistance, boxLayerMask);
		if (forwardBoxRay)
		{
			isCanMove = forwardBoxRay.transform.GetComponent<Box>().BoxMove(xDir, yDir);
		}

		if (isCanMove)
		{
			transform.position += new Vector3(xDir, yDir, 0) * moveDistance;
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