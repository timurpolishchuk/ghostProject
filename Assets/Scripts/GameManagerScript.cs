using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private char[][,] allMaps = new char[50][,];
    public char[,] map = new char[50, 50];
    public int[] coinsFromLevel;                

    [HideInInspector]
    public int x;
    [HideInInspector]
    public int y;

    private int Level
    {
        get
        {
            return level;
        }
        set
        {
            countLvl += value - level;
            if (value > maxLvl)
            {                
                level = Random.Range(1, maxLvl + 1);
            }
            else
            {
                level = value;
            }            
            lvlText.text = "Level: " + countLvl;
        }
    }
    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            coinsText.text = coins.ToString();
        }
    }
    private int level = 1, coins = 0;
    private int countLvl = 1;
    private int maxLvl;

    public GeneretionScript genScript;
    public TouchControlScript tcs;
    public GameObject winPanel, losePanel;
    public Text lvlText, coinsText;    


    private void Awake()
    {        
        InitializeMaps();
        if(PlayerPrefs.HasKey("level"))
        {
            Level = PlayerPrefs.GetInt("level");
            Coins = PlayerPrefs.GetInt("coins");
        }        
        else
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("coins", 0);
        }
        genScript.GetBlockSize();
        GenerateBlocks();
    }
    private void GenerateBlocks()
    {
        genScript.ClearField();
        tcs.PlayerPositionToZero();        
        x = allMaps[level - 1].GetLength(0);
        y = allMaps[level - 1].GetLength(1);
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                map[i, j] = allMaps[level - 1][i, j]; //without reference to allMaps[...]
            }
        }
        genScript.FeelingBlocks(x, y);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                ColorSquare(i, j);
            }
        }
    }
    public void ColorSquare(int i, int j)
    {
        switch(map[i, j])
        {
            case 'X':
                genScript.blocks[i, j].GetComponent<Image>().color = Color.black;
                break;
            default:
                float tmpColor = 1.0f - (map[i, j] - '0') / 10.0f;
                genScript.blocks[i, j].GetComponent<Image>().color = new Color(tmpColor, tmpColor, tmpColor, 1.0f);
                break;
        }
    }
    public void UpdateCellsNum(int i, int j)
    {
        if(map[i, j] == '0')
        {
            LoseGame();
        }
        else
        {
            map[i, j]--;
            genScript.blockTexts[i, j].text = map[i, j].ToString();
        }        
    }
    public void CheckForVictory()
    {        

        bool victory = true;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if(map[i, j] != 'X' && map[i, j] != '0')
                {
                    victory = false;
                    break;
                }
            }
        }
        if(victory)
        {                        
            StartCoroutine(CheckForVictoryLater());            
        }
    }
    IEnumerator CheckForVictoryLater()
    {
        yield return new WaitForSeconds(0.255f);
        if (losePanel.activeSelf == false)
        {
            Coins += coinsFromLevel[level - 1]; //Get Coins for victory
            tcs.canMove = false;
            winPanel.SetActive(true);
            winPanel.GetComponent<Animation>().Play();
        }
    }
    private void InitializeMaps()
    {
        allMaps[0] = new char[2, 2] {  {'0', '1'},
                                       {'X', '1'},
                                    };
        allMaps[1] = new char[2, 2] {  {'0', '1'},
                                       {'1', '1'},
                                    };
        allMaps[2] = new char[1, 4] {  {'0', '1', '1', '1'},
                                    };
        allMaps[3] = new char[3, 2] {  {'0', '1'},
                                       {'X', '1'},
                                       {'1', '1'},
                                    };
        allMaps[4] = new char[2, 4] {  {'0', '1', 'X', '1'},
                                       {'X', '1', '1', '1'},
                                    };
        allMaps[5] = new char[2, 4] {  {'0', '1', '1', '1'},
                                       {'X', '1', '1', '1'},
                                    };
        allMaps[6] = new char[3, 4] {  {'0', '1', '1', 'X'},
                                       {'X', '1', '1', 'X'},
                                       {'X', 'X', 'X', 'X'},
                                    };
        allMaps[7] = new char[2, 4] {  {'1', '1', 'X', 'X'},
                                       {'1', 'X', 'X', 'X'},
                                    };
        allMaps[8] = new char[4, 4] {  {'0', '1', 'X', 'X'},
                                       {'X', '1', '1', '1'},
                                       {'X', 'X', '1', '1'},
                                       {'X', 'X', 'X', 'X'},
                                    };
        allMaps[9] = new char[3, 5] {  {'0', '1', 'X', 'X', '1'},
                                       {'X', '1', '1', 'X', '1'},
                                       {'X', 'X', '1', '1', '1'},
                                    };
        allMaps[10] = new char[3, 5] {  {'0', '1', 'X', 'X', '1'},
                                        {'X', '1', '1', '1', '2'},
                                        {'X', 'X', '1', '1', '1'},
                                    };
        allMaps[11] = new char[4, 4] {  {'0', '1', 'X', 'X'},
                                        {'X', '1', '1', 'X'},
                                        {'X', 'X', '1', '0'},
                                        {'1', '1', '1', 'X'},
                                    };
        allMaps[12] = new char[4, 4] {  {'0', '1', 'X', 'X'},
                                        {'X', '1', '2', '1'},
                                        {'X', 'X', '1', '1'},
                                        {'X', 'X', '1', '1'},
                                    };
        allMaps[13] = new char[4, 4] {  {'0', '1', 'X', 'X'},
                                        {'X', '1', '0', '0'},
                                        {'1', '2', '1', '1'},
                                        {'X', 'X', '1', '1'},
                                    };
        allMaps[14] = new char[4, 4] {  {'0', 'X', 'X', 'X'},
                                        {'2', '1', '1', '1'},
                                        {'1', '1', '1', '2'},
                                        {'X', 'X', '0', '1'},
                                    };
        allMaps[15] = new char[5, 4] {  {'1', 'X', '1', 'X'},
                                        {'3', 'X', '2', 'X'},
                                        {'2', '1', '2', 'X'},
                                        {'X', 'X', '1', 'X'},
                                        {'X', 'X', '1', '1'},
                                    }; //DUDRUDR
        allMaps[16]= new char[5, 4] {  {'1', '1', '2', '1'},
                                        {'1', 'X', '1', 'X'},
                                        {'1', '0', '1', 'X'},
                                        {'1', 'X', '1', 'X'},
                                        {'1', '1', '1', 'X'},
                                    };
        allMaps[17] = new char[5, 4] {  {'0', '1', '1', 'X'},
                                        {'X', '1', '1', 'X'},
                                        {'X', '1', 'X', 'X'},
                                        {'1', '2', '1', '1'},
                                        {'1', '2', '2', '2'},
                                    };
        allMaps[18] = new char[4, 7] {  {'0', '1', '1', 'X', '1', '2', 'X' },
                                        {'1', '1', '1', '1', 'X', '2', '1' },
                                        {'1', '1', 'X', '1', 'X', '1', '1' },
                                        {'1', '1', 'X', '1', '1', '2', '1' },
                                    };
        allMaps[19] = new char[6, 7] {  {'0', '1', '1', 'X', '1', '1', 'X' },
                                        {'X', '1', '1', 'X', 'X', '2', '1' },
                                        {'X', '1', 'X', 'X', 'X', '1', '1' },
                                        {'1', '2', '2', '2', 'X', 'X', '1' },
                                        {'2', '2', '2', '2', '2', '2', '2' },
                                        {'2', '2', '1', 'X', '1', '1', '1' },
                                    };
        allMaps[20] = new char[2, 3] {  {'1', '3', '2'},
                                        {'X', '1', '2'},
                                    };
        allMaps[21] = new char[3, 3] {  {'2', '3', '3'},
                                        {'1', 'X', '3'},
                                        {'X', '6', '8'}
                                    };
        allMaps[22] = new char[4, 3] {  {'1', 'X', '1'},
                                        {'2', 'X', '2'},
                                        {'2', '2', '3'},
                                        {'X', '1', '2'}
                                    };
        maxLvl = 23;
    }
    public void NextLevelButton()
    {
        winPanel.SetActive(false);
        Level++;
        GenerateBlocks();
        StartCoroutine(SwitchOnAvailabilityToMove());        
    }
    private void LoseGame()
    {
        losePanel.SetActive(true);
        losePanel.GetComponent<Animation>().Play();
        tcs.canMove = false;
    }
    public void ReplayLevelAfterLose()
    {
        losePanel.SetActive(false);        
        GenerateBlocks();
        StartCoroutine(SwitchOnAvailabilityToMove());
    }
    IEnumerator SwitchOnAvailabilityToMove()
    {
        yield return new WaitForSeconds(0.05f);
        tcs.canMove = true;
    }
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("coins", coins);
    }
}
