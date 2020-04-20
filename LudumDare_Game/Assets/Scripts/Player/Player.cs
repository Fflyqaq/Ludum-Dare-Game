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

    //判断多个箱子时的偏移和检测距离
    private float offect = 2f;
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
        if (PlayerWSAD())
        {
            BattleSystem.Instance.ReduceRemainNum();
            StartCoroutine(WaitPlayerMove());
        }
    }

    IEnumerator WaitPlayerMove()
    {
        yield return new WaitForSeconds(0.1f);
        BattleSystem.Instance.RabbitMove();
    }

    private bool PlayerWSAD()
    {
        if (Input.GetKeyDown(KeyCode.W) && CheckIfCanMove(1) && CheckIfCanBoxAndPlayerMove(1) && isCanMove)
        {
            //Vector3 newPos = new Vector3(transform.position.x, transform.position.y + yMoveDistance, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + yMoveDistance, 0);

            CheckIfCanMoveBox(1);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && CheckIfCanMove(2) && CheckIfCanBoxAndPlayerMove(2) && isCanMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - yMoveDistance, 0);

            CheckIfCanMoveBox(2);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && CheckIfCanMove(3) && CheckIfCanBoxAndPlayerMove(3) && isCanMove)
        {
            transform.position = new Vector3(transform.position.x - xMoveDistance, transform.position.y, 0);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            CheckIfCanMoveBox(3);
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && CheckIfCanMove(4) && CheckIfCanBoxAndPlayerMove(4) && isCanMove)
        {
            transform.position = new Vector3(transform.position.x + xMoveDistance, transform.position.y, 0);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            CheckIfCanMoveBox(4);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 射线检测是否有Collider
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMove(int checkNum)
    {
        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay = Tools.RayCheck(transform.position, Vector2.up, checkCollDistance, collLayerMask);
                return upRay?false:true;
            case 2:
                RaycastHit2D downRay = Tools.RayCheck(transform.position, Vector2.down, checkCollDistance, collLayerMask);
                return downRay ? false:true;
            case 3:
                RaycastHit2D leftRay = Tools.RayCheck(transform.position, Vector2.left, checkCollDistance, collLayerMask);
                return leftRay ? false:true;
            case 4:
                RaycastHit2D rightRay = Tools.RayCheck(transform.position, Vector2.right, checkCollDistance, collLayerMask);
                return rightRay ? false:true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 射线检测是否有箱子可以推
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMoveBox(int checkNum)
    {
        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay = Tools.RayCheck(transform.position, Vector2.up, checkBoxDistance, boxLayerMask);
                if (upRay)
                {
                    upRay.transform.GetComponent<Box>().BoxMove(0,1);
                }
                return upRay ? false : true;
            case 2:
                RaycastHit2D downRay = Tools.RayCheck(transform.position, Vector2.down, checkBoxDistance, boxLayerMask);
                if (downRay)
                {
                    downRay.transform.GetComponent<Box>().BoxMove(0, -1);
                }
                return downRay ? false : true;
            case 3:
                RaycastHit2D leftRay = Tools.RayCheck(transform.position, Vector2.left, checkBoxDistance, boxLayerMask);
                if (leftRay)
                {
                    leftRay.transform.GetComponent<Box>().BoxMove(-1, 0);
                }
                return leftRay ? false : true;
            case 4:
                RaycastHit2D rightRay = Tools.RayCheck(transform.position, Vector2.right, checkBoxDistance, boxLayerMask);
                if (rightRay)
                {
                    rightRay.transform.GetComponent<Box>().BoxMove(1, 0);
                }
                return rightRay ? false : true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 射线检测箱子后面是否不能移动
    /// </summary>
    private bool CheckIfCanBoxAndPlayerMove(int checkNum)
    {
        //只有有箱子，箱子后面还要有Coll的时候才不能移动
        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay = Tools.RayCheck(transform.position, Vector2.up, checkCollDistance, boxLayerMask);
                if (!upRay)
                    return true;
                break;
            case 2:
                RaycastHit2D downRay = Tools.RayCheck(transform.position, Vector2.down, checkCollDistance, boxLayerMask);
                if (!downRay)
                    return true;
                break;
            case 3:
                RaycastHit2D leftRay = Tools.RayCheck(transform.position, Vector2.left, checkCollDistance, boxLayerMask);
                if (!leftRay)
                    return true;
                break;
            case 4:
                RaycastHit2D rightRay = Tools.RayCheck(transform.position, Vector2.right, checkCollDistance, boxLayerMask);
                if (!rightRay)
                    return true;
                break;
        }

        //走到这就有箱子，再判断有没有Coll

        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay1 = Tools.RayCheck(transform.position, Vector2.up, checkBoxCollDistance, collLayerMask);
                RaycastHit2D upRay2 = Tools.RayCheck(transform.position + new Vector3(0, 2, 0), Vector2.up, checkBoxBoxDistance, boxLayerMask);
                if (!upRay1 && !upRay2)
                    return true;
                break;
            case 2:
                RaycastHit2D downRay1 = Tools.RayCheck(transform.position, Vector2.down, checkBoxCollDistance, collLayerMask);
                RaycastHit2D downRay2 = Tools.RayCheck(transform.position + new Vector3(0, -2, 0), Vector2.down, checkBoxBoxDistance, boxLayerMask);
                if (!downRay1 && !downRay2)
                    return true;
                break;
            case 3:
                RaycastHit2D leftRay1 = Tools.RayCheck(transform.position, Vector2.left, checkBoxCollDistance, collLayerMask);
                RaycastHit2D leftRay2 = Tools.RayCheck(transform.position + new Vector3(-2,0,0), Vector2.left, checkBoxBoxDistance, boxLayerMask);
                if (!leftRay1 && !leftRay2)
                    return true;
                break;
            case 4:
                RaycastHit2D rightRay1 = Tools.RayCheck(transform.position, Vector2.right, checkBoxCollDistance, collLayerMask);
                RaycastHit2D rightRay2 = Tools.RayCheck(transform.position + new Vector3(2, 0, 0), Vector2.right, checkBoxBoxDistance, boxLayerMask);
                if (!rightRay1 && !rightRay2)
                    return true;
                break;
        }

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
