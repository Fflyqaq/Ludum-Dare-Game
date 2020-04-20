/****************************************************
    文件：Wind.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 18:46:14
    功能：陷阱---风
*****************************************************/

using UnityEngine;

public class Wind : TrapRoot 
{
	public int xDir;
	public int yDir;

	public Animator anim;

	//public int maxLength;

	public GameObject particleGO;
	private BoxCollider2D collWind;

	private void Start()
	{
		collWind = GetComponent<BoxCollider2D>();
		ParticleStateChange();
	}

	private void Update(){
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Rabbit")
		{
			collision.GetComponent<Rabbit>().SetMoveDir(xDir, yDir);
			//collision.GetComponent<Rabbit>().Move();

			//float distance = Vector3.Distance(transform.position, collision.transform.position);
			//int dis = maxLength + 1 - Mathf.RoundToInt(distance);
			//collision.GetComponent<Rabbit>().WindBlowed(dis);
		} 
	}

	public override void TriggerSwitch()
	{
		base.TriggerSwitch();

		isCanWork = !isCanWork;
		ParticleStateChange();
	}

	private void ParticleStateChange()
	{
		if (isCanWork)
		{
			collWind.enabled = true;
			particleGO.SetActive(true);
			anim.SetBool("isCanWork", true);
		}
		else
		{
			collWind.enabled = false;
			particleGO.SetActive(false);
			anim.SetBool("isCanWork", false);
		}
	}
}