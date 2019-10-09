using System.Collections;
using UnityEngine;

public class TouchControlScript : MonoBehaviour
{
    private int i, j, tmpCount;
    private int posX, posY;
    private float x, y;
    private Vector2 touchVector, lastInputPos;       
    public GameManagerScript gms;
    public GeneretionScript genScript;
    public PlayerScript playerScript;
    public bool canMove;
    private bool firstTouch = true, upFinger = false;

    public void PlayerPositionToZero()
    {
        posX = posY = 0;
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {                        
                touchVector = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
                touchVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - touchVector;
                x = touchVector.x;
                y = touchVector.y;
                if (x >= 0 && y >= 0) //1st quarter
                {
                    if (y > x)
                        UpMove();
                    else
                        RightMove();
                }
                else if (x <= 0 && y >= 0) //2nd quarter
                {
                    if (y > -x)
                        UpMove();
                    else
                        LeftMove();
                }
                else if (x <= 0 && y <= 0) //3rd quarter
                {
                    if (-y > -x)
                        DownMove();
                    else
                        LeftMove();
                }
                else //4th quarter
                {
                    if (x > -y)
                        RightMove();
                    else
                        DownMove();
                }            
        }
#endif
        if (Input.touchCount > 0)
        {
            upFinger = true;
            lastInputPos = Input.touches[0].position;
            if (firstTouch)
            {
                touchVector = Input.touches[0].position;
                firstTouch = false;
            }            
        }
        else
        {
            if(upFinger)
            {
                firstTouch = true;
                upFinger = false;
                touchVector = lastInputPos - touchVector;
                x = touchVector.x;
                y = touchVector.y;
                if (x >= 0 && y >= 0) //1st quarter
                {
                    if (y > x)
                        UpMove();
                    else
                        RightMove();
                }
                else if (x <= 0 && y >= 0) //2nd quarter
                {
                    if (y > -x)
                        UpMove();
                    else
                        LeftMove();
                }
                else if (x <= 0 && y <= 0) //3rd quarter
                {
                    if (-y > -x)
                        DownMove();
                    else
                        LeftMove();
                }
                else //4th quarter
                {
                    if (x > -y)
                        RightMove();
                    else
                        DownMove();
                }
            }
        }
    }

    private float t = 0.15f;
    private void DownMove()
    {
        if (canMove)
        {
            bool exitFromLoop = false;
            tmpCount = 0;
            for (i = posX + 1; i < gms.x; i++)
            {
                if (gms.map[i, posY] != 'X') //is not an obstacle or smth like that
                {
                    tmpCount++;
                    StartCoroutine(UpdateNumLater(tmpCount * t, i, posY));
                    posX++;
                }
                else
                {
                    playerScript.UpdateTarget(genScript.blocks[i - 1, posY].transform);
                    exitFromLoop = true;
                    break;
                }
            }
            canMove = false;
            StartCoroutine(SetMoveAvailability(tmpCount * t, true));
            if (!exitFromLoop)
            {
                playerScript.UpdateTarget(genScript.blocks[gms.x - 1, posY].transform);
            }
            //gms.CheckForVictory();
        }
    }
    private void UpMove()
    {
        if (canMove)
        {
            bool exitFromLoop = false;
            tmpCount = 0;
            for (i = posX - 1; i >= 0; i--)
            {
                if (gms.map[i, posY] != 'X') //is not an obstacle or smth like that
                {
                    tmpCount++;
                    StartCoroutine(UpdateNumLater(tmpCount * t, i, posY));                   
                    posX--;
                }
                else
                {
                    playerScript.UpdateTarget(genScript.blocks[i + 1, posY].transform);
                    exitFromLoop = true;
                    break;
                }
            }
            canMove = false;
            StartCoroutine(SetMoveAvailability(tmpCount * t, true));
            if (!exitFromLoop)
            {
                playerScript.UpdateTarget(genScript.blocks[0, posY].transform);
            }
            //gms.CheckForVictory();
        }
    }
    private void RightMove()
    {
        if (canMove)
        {
            bool exitFromLoop = false;
            tmpCount = 0;
            for (j = posY + 1; j < gms.y; j++)
            {
                if (gms.map[posX, j] != 'X') //is not an obstacle or smth like that
                {
                    tmpCount++;
                    StartCoroutine(UpdateNumLater(tmpCount * t, posX, j));
                    posY++;
                }
                else
                {
                    playerScript.UpdateTarget(genScript.blocks[posX, j - 1].transform);
                    exitFromLoop = true;
                    break;
                }
            }
            canMove = false;
            StartCoroutine(SetMoveAvailability(tmpCount * t, true));
            if (!exitFromLoop)
            {
                playerScript.UpdateTarget(genScript.blocks[posX, gms.y - 1].transform);
            }
            //gms.CheckForVictory();
        }
    }
    private void LeftMove()
    {
        if (canMove)
        {
            bool exitFromLoop = false;
            tmpCount = 0;
            for (j = posY - 1; j >= 0; j--)
            {
                if (gms.map[posX, j] != 'X') //is not an obstacle or smth like that
                {
                    tmpCount++;
                    StartCoroutine(UpdateNumLater(tmpCount * t, posX, j));
                    posY--;
                }
                else
                {
                    playerScript.UpdateTarget(genScript.blocks[posX, j + 1].transform);
                    exitFromLoop = true;
                    break;
                }
            }
            canMove = false;
            StartCoroutine(SetMoveAvailability(tmpCount * t, true));
            if (!exitFromLoop)
            {
                playerScript.UpdateTarget(genScript.blocks[posX, 0].transform);
            }
            //gms.CheckForVictory();
        }
    }
    IEnumerator UpdateNumLater(float sec, int i, int j)
    {
        yield return new WaitForSeconds(sec);
        gms.UpdateCellsNum(i, j);
        gms.ColorSquare(i, j);
        gms.CheckForVictory();
    }
    IEnumerator SetMoveAvailability(float delay, bool value)
    {
        yield return new WaitForSeconds(delay);
        canMove = value;
    }
}
