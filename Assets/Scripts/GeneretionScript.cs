using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GeneretionScript : MonoBehaviour
{
    public GameObject[,] blocks = new GameObject[50, 50];
    public Text[,] blockTexts = new Text[50, 50];
    public List<List<Block>> block = new List<List<Block>>();
    public GameObject blockObject, parentObj;
    public GameManagerScript gms;
    public PlayerScript playerScript;
    //private int width, heigth;
    private int i, j;
    public float offset;
    private float sizeOfBlockObj;
    private int x, y;

    public void GetBlockSize()
    {
        sizeOfBlockObj = blockObject.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void FeelingBlocks(int x, int y)
    {
        this.x = x;
        this.y = y;
        i = j = -1;
        //blocks = new GameObject[x, y];
        for (float n = (1.0f - x) / 2; n <= (x - 1.0f) / 2; n++)
        {
            i++;
            j = -1;
            for (float m = (1.0f - y) / 2; m <= (y - 1.0f) / 2; m++)
            {
                j++;
                Vector3 pos = new Vector3(m, -n);
                if (blocks[i, j] == null)
                {
                    blocks[i, j] = Instantiate(blockObject, pos, Quaternion.identity, parentObj.transform);
                    blockTexts[i, j] = blocks[i, j].GetComponentInChildren<Text>();
                }
                else
                {
                    blocks[i, j].SetActive(true);
                }
                //if(gms.map[i, j] != 'X')
                //blockTexts[i, j].text = gms.map[i, j].ToString();
                //else
                //blockTexts[i, j].text = "";
                blockTexts[i, j].text = gms.map[i, j].ToString();
                blocks[i, j].transform.localPosition = pos * sizeOfBlockObj;
            }
        }
        playerScript.gameObject.transform.position = blocks[0, 0].transform.position;
        playerScript.UpdateTarget(blocks[0, 0].transform);
    }
    public void ClearField()
    {
        //block.Clear();
        for(i = 0; i < x; i++)
        {
            for(j = 0; j < y; j++)
            {
                blocks[i, j].SetActive(false);
            }
        }
    }
}
[System.Serializable]
public class Block
{
    public bool bomb = false;

}