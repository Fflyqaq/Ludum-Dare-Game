/****************************************************
    文件：test.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:12:54
    功能：玩家
*****************************************************/

using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 移动相关
    private int xDir;
    private int yDir;
    private float xMoveDistance = 1;
    private float yMoveDistance = 1;
    private bool isCanMove = true;
    //public bool IsCanMove { set { isCanMove = value; } }

    //Collider碰撞层，Box层，射线检测距离
    private LayerMask collLayerMask;
    private LayerMask boxLayerMask;
    private float checkCollDistance = 1;
    private float checkBoxDistance = 0.5f;
    private float checkBoxCollDistance = 2f;

    //判断多个箱子时的检测距离
    private float checkBoxBoxDistance = 0.3f;
    #endregion

    void Start()
    {
        //第8,9层
        collLayerMask = 1 << 8;
        boxLayerMask = 1 << 9;

    }

    void Update()
    {
        PlayerInput();
        PlayerWSAD();
    }

    IEnumerator WaitPlayerMove()
    {
        yield return new WaitForSeconds(0.3f);
        BattleSystem.Instance.RabbitMove();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            xDir = 0;
            yDir = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            xDir = 0;
            yDir = -1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            xDir = -1;
            yDir = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            xDir = 1;
            yDir = 0;
        }
    }

    private void PlayerWSAD()
    {
        if (xDir != 0 || yDir!=0)
        {
            CheckIfCatchRabbit(xDir, yDir);

            if (CheckIfCanMove(xDir, yDir) && CheckIfCanBoxAndPlayerMove(xDir, yDir) && isCanMove)
            {
                transform.position = new Vector3(transform.position.x + xDir, transform.position.y + yDir, 0);
                if (xDir !=0)
                {
                    transform.localScale = new Vector3(xDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                CheckIfCanMoveBox(xDir, yDir);

                StartCoroutine(WaitPlayerMove());
            }
            xDir = 0;
            yDir = 0;
        }
    }

    private void CheckIfCatchRabbit(int x,int y)
    {
        //只有第三关可以抓兔子
        if (BattleSystem.Instance.NowCheckPointNum == 3)
        {
            //抓住兔子，下一剧情
            RaycastHit2D ray = Tools.RayCheck(transform.position, new Vector2(x, y), checkCollDistance, collLayerMask);
            if (ray)
            {
                if (ray.transform.tag == "Rabbit")
                {
                    //这关玩过
                    if (Tools.CheckIfPlayedThisCheckPoint(BattleSystem.Instance.NowCheckPointNum))
                    {
                        BattleSystem.Instance.IsWin = true;
                    }
                    else
                    {
                        BattleSystem.Instance.ShowNextTalk();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 射线检测是否有Collider
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMove(int x,int y)
    {
        RaycastHit2D ray = Tools.RayCheck(transform.position, new Vector2(x, y), checkCollDistance, collLayerMask);
        return ray ? false : true;
    }

    /// <summary>
    /// 射线检测是否有箱子可以推
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMoveBox(int x, int y)
    {
        RaycastHit2D ray = Tools.RayCheck(transform.position, new Vector2(x, y), checkBoxDistance, boxLayerMask);
        if (ray)
        {
            ray.transform.GetComponent<Box>().BoxMove(x, y);
        }
        return ray ? false : true;
    }

    /// <summary>
    /// 射线检测箱子后面是否不能移动
    /// </summary>
    private bool CheckIfCanBoxAndPlayerMove(int x,int y)
    {
        RaycastHit2D ray = Tools.RayCheck(transform.position, new Vector2(x, y), checkCollDistance, boxLayerMask);
        if (!ray)
            return true;

        //走到这就有箱子，再判断有没有Coll

        RaycastHit2D ray1 = Tools.RayCheck(transform.position, new Vector2(x, y), checkBoxCollDistance, collLayerMask);
        RaycastHit2D ray2 = Tools.RayCheck(transform.position + new Vector3(x * 2, y * 2, 0), new Vector2(x, y), checkBoxBoxDistance, boxLayerMask);
        if (!ray1 && !ray2)
            return true;


        //到这表示有箱子有Coll，不能移动
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //陷阱
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("你死了");
            Destroy(gameObject);

            BattleSystem.Instance.IsLose = true;
        }




        ////开关
        //if (collision.gameObject.tag == "Switch")
        //{
        //    Debug.Log("开关");
        //    collision.gameObject.GetComponent<TrapSwitch>().OpenSwitch();
        //}
    }

    public void StopPlayerMove()
    {
        isCanMove = false;
    }
    public void ContinuePlayerMove()
    {
        isCanMove = true;
    }
}
