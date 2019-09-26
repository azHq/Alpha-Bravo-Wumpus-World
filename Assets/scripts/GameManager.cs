using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public GameObject wumpus, gold,pit;
    private float startPointX = -3.66f, startPointY = 4.52f;
    private float differenceX = 1.23f, differenceY = 1f;
    float[,,] board = new float[10,10,3];
    public int numberOfWumpus = 3, numberOfPit=5, numberOfGold=2;
    GameObject[,] gameObjects = new GameObject[10,10];
    private float source = 0.0f, destination=0.0f;
    private int[] player_currentPosition = new int[2];
    private float speed = .02f;
    void Start()
    {
        boardPointInit();
        boardContentInit();

    }

    // Update is called once per frame
    void Update()
    {
        if(anim!=null) run();
    }

    private void boardContentInit()
    {
        while (true)
        {
            int randI = Random.Range(0, 10);
            int randJ = Random.Range(0, 10);
            if (board[randI, randJ, 0] == 0)
            {
                player_currentPosition[0] = randI;
                player_currentPosition[1] = randJ;
                player = Instantiate(player, new Vector3(board[randI, randJ, 1], board[randI, randJ, 2], -1f), wumpus.transform.rotation);
                anim = player.GetComponent<Animator>();
                break;
            }
        }
        int i = 0;
        while (i< numberOfWumpus)
        {
            int randI = Random.Range(0,10);
            int randJ = Random.Range(0, 10);
            if (board[randI, randJ, 0]==0)
            {
                gameObjects[randI, randJ] =Instantiate(wumpus, new Vector3(board[randI, randJ, 1], board[randI, randJ, 2], -1f), wumpus.transform.rotation);
                board[randI, randJ, 0] = Constant.WUMPUS;
                i++;
            }
        }
        i = 0;
        while (i < numberOfPit)
        {
            int randI = Random.Range(0, 10);
            int randJ = Random.Range(0, 10);
            if (board[randI, randJ, 0] == 0)
            {
                gameObjects[randI, randJ]=Instantiate(pit, new Vector3(board[randI, randJ, 1], board[randI, randJ, 2], -1f), wumpus.transform.rotation);
                board[randI, randJ, 0] = Constant.PIT;
                i++;
            }
        }
        i = 0;
        while (i < numberOfGold)
        {
            int randI = Random.Range(0, 10);
            int randJ = Random.Range(0, 10);
            if (board[randI, randJ, 0] == 0)
            {
                gameObjects[randI, randJ] = Instantiate(gold, new Vector3(board[randI, randJ, 1], board[randI, randJ, 2], -1f), wumpus.transform.rotation);
                board[randI, randJ, 0] = Constant.GOLD;
                i++;
            }
        }
    }
    private void boardPointInit()
    {

        float pointX = startPointX, pointY = startPointY;
        for (int i = 0; i < 10; i++)
        {
            pointX = startPointX;
            for (int j = 0; j < 10; j++)
            {
                board[i, j, 0] = 0;
                board[i, j, 1] = pointX;
                board[i, j, 2] = pointY;
                pointX += differenceX;
            }
            pointY -= differenceY;
        }
    }

    public void restart()
    {
        for (int i=0; i<10;i++)
        {
            for (int j=0;j<10; j++)
            {
               if(board[i,j,0]!=0) Destroy(gameObjects[i,j]);
            }

        }
        
        boardContentInit();
    }
    public void up_turn()
    {

        if (!anim.GetBool("up_turn"))
        {
            anim.SetBool("up", false);
            anim.SetBool("up_turn", true);
        }
        else
        {
            if (player_currentPosition[0] - 1>0&&board[player_currentPosition[0] - 1, player_currentPosition[1], 0] ==0) {
                board[player_currentPosition[0] - 1, player_currentPosition[1], 0] = Constant.PLAYER;
                board[player_currentPosition[0], player_currentPosition[1], 0] =0;
                source = board[player_currentPosition[0], player_currentPosition[1], 2];
                destination = board[player_currentPosition[0]-1, player_currentPosition[1], 2];
                player_currentPosition[0] -= 1;
            }
            anim.SetBool("up_turn", false);
            anim.SetBool("up", true);
        }
    }

    public void up_move()
    {
        
        anim.SetBool("up",true);
    }

    public void down_turn()
    {
        if (!anim.GetBool("down_turn"))
        {
            anim.SetBool("down", false);
            anim.SetBool("down_turn", true);
        }
        else
        {
            if (player_currentPosition[0] + 1 <10 && board[player_currentPosition[0] + 1, player_currentPosition[1], 0] == 0)
            {
                board[player_currentPosition[0] + 1, player_currentPosition[1], 0] = Constant.PLAYER;
                board[player_currentPosition[0], player_currentPosition[1], 0] = 0;
                source = board[player_currentPosition[0], player_currentPosition[1], 2];
                destination = board[player_currentPosition[0] + 1, player_currentPosition[1], 2];
                player_currentPosition[0] += 1;
            }
            anim.SetBool("down_turn", false);
            anim.SetBool("down", true);
        }
    }

    public void down_move()
    {

    }

    public void left_turn()
    {
        if (!anim.GetBool("left_turn"))
        {
            anim.SetBool("left_move", false);
            anim.SetBool("left_turn", true);
        }
        else
        {
            if (player_currentPosition[1]-1  >0 && board[player_currentPosition[0], player_currentPosition[1]-1, 0] == 0)
            {
                board[player_currentPosition[0], player_currentPosition[1]-1, 0] = Constant.PLAYER;
                board[player_currentPosition[0], player_currentPosition[1], 0] = 0;
                source = board[player_currentPosition[0], player_currentPosition[1], 1];
                destination = board[player_currentPosition[0], player_currentPosition[1]-1, 1];
                player_currentPosition[1] -= 1;
            }
            anim.SetBool("left_turn", false);
            anim.SetBool("left_move", true);
        }
    }

    public void left_move()
    {

    }

    public void right_turn()
    {
        if (!anim.GetBool("right_turn"))
        {
            anim.SetBool("right_move", false);
            anim.SetBool("right_turn", true);
        }
        else
        {
            if (player_currentPosition[1] + 1 < 10 && board[player_currentPosition[0], player_currentPosition[1] + 1, 0] == 0)
            {
                board[player_currentPosition[0], player_currentPosition[1] + 1, 0] = Constant.PLAYER;
                board[player_currentPosition[0], player_currentPosition[1], 0] = 0;
                source = board[player_currentPosition[0], player_currentPosition[1], 1];
                destination = board[player_currentPosition[0], player_currentPosition[1] + 1, 1];
                player_currentPosition[1]+=1;

            }
            anim.SetBool("right_turn", false);
            anim.SetBool("right_move", true);
        }

    }

    public void right_move()
    {

    }

    public void run()
    {
       
        if (anim.GetBool("right_move"))
        {
            if (source <= destination)
            {
                source += speed;
                player.transform.position = new Vector3(source, player.transform.position.y,-1f);
            }
            else
            {
                anim.SetBool("right_move", false);
            }
        }
        else if (anim.GetBool("left_move"))
        {
            if (source >= destination)
            {
                source -= speed;
                player.transform.position = new Vector3(source, player.transform.position.y, -1f);
            }
            else
            {
                anim.SetBool("left_move", false);
            }
        }
        else if (anim.GetBool("up"))
        {
            if (source <= destination)
            {
                source += speed;
                player.transform.position = new Vector3(player.transform.position.x, source, -1f);
            }
            else
            {
                anim.SetBool("up", false);
            }

        }
        else if (anim.GetBool("down"))
        {
            if (source >= destination)
            {
                source -= speed;
                player.transform.position = new Vector3(player.transform.position.x, source, -1f);
            }
            else
            {
                anim.SetBool("down", false);
            }
        }
    }


}
