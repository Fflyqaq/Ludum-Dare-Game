/****************************************************
    文件：Wind.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 18:46:14
    功能：陷阱---风
*****************************************************/

using UnityEngine;

public class Wind : MonoBehaviour 
{
	public int xDir;
	public int yDir;

	public int maxLength;

	private void Start(){
		
	}
	private void Update(){
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "rabbit")
		{
			collision.GetComponent<Rabbit>().SetMoveDir(xDir, yDir);

			float distance = Vector3.Distance(transform.position, collision.transform.position);
			int dis = 4- Mathf.RoundToInt(distance);
			collision.GetComponent<Rabbit>().WindBlowed(dis);

		} 
	}
}