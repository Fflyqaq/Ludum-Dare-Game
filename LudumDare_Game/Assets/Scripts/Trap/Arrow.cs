/****************************************************
    文件：Arrow.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/20 9:45:42
    功能：Nothing
*****************************************************/

using UnityEngine;

public class Arrow : TrapRoot
{
	private void Start(){
		Destroy(gameObject, 4f);
	}
	private void Update(){
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //碰撞消失
        if (collision.gameObject.tag == "Switch" || collision.gameObject.tag == "Platforms")
        {
            Destroy(gameObject);
        }
    }
}