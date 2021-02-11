using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3 : MonoBehaviour
{

    public ArrayLayout boardLayout;
    public Sprite[] pieces;
    int width = 9;
    int height = 14;
    Node[,] board;

    System.Random random;

    void StartGame()
    {
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());

        InitializeBoard();
    }

    void InitializeBoard()
    {
        board = new Node[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = new Node((boardLayout.rows[y].row[x]) ? -1 : fillPiece(), new Point(x, y));
            }
        }
    }

    // Verifies no matches are on initial board
    void VerifyBoard()
    {
        // List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x, y);
                int val = getValueAtPoint(p);
                if (val <= 0) continue;

                //         remove = new List<int>();
                //         while (isConnected(p, true).Count > 0)
                //         {
                //             val = getValueAtPoint(p);
                //             if (!remove.Contains(val))
                //                 remove.Add(val);
                //             setValueAtPoint(p, newValue(ref remove));
                //         }
            }
        }
    }

    List<Point> isConnected(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = getValueAtPoint(p);
        Point[] directions =
        {
            Point.up,
            Point.right,
            Point.down,
            Point.left
        };

        foreach (Point dir in directions) //Checking if there is 2 or more same shapes in the directions
        {
            List<Point> line = new List<Point>();

            int same = 0;
            for (int i = 1; i < 3; i++)
            {
                Point check = Point.add(p, Point.mult(dir, i));
                if (getValueAtPoint(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }

            if (same > 1) //If there are more than 1 of the same shape in the direction then we know it is a match
                AddPoints(ref connected, line); //Add these points to the overarching connected list
        }

        for (int i = 0; i < 2; i++) //Checking if we are in the middle of two of the same shapes
        {
            List<Point> line = new List<Point>();

            int same = 0;
            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[i + 2]) };
            foreach (Point next in check) //Check both sides of the piece, if they are the same value, add them to the list
            {
                if (getValueAtPoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }

            if (same > 1)
                AddPoints(ref connected, line);
        }

        for (int i = 0; i < 4; i++) //Check for a 2x2
        {
            List<Point> square = new List<Point>();

            int same = 0;
            int next = i + 1;
            if (next >= 4)
                next -= 4;

            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[next]), Point.add(p, Point.add(directions[i], directions[next])) };
            foreach (Point pnt in check) //Check all sides of the piece, if they are the same value, add them to the list
            {
                if (getValueAtPoint(pnt) == val)
                {
                    square.Add(pnt);
                    same++;
                }
            }

            if (same > 2)
                AddPoints(ref connected, square);
        }

        if (main) //Checks for other matches along the current match
        {
            for (int i = 0; i < connected.Count; i++)
                AddPoints(ref connected, isConnected(connected[i], false));
        }


        return connected;
    }

    void AddPoints(ref List<Point> points, List<Point> add)
    {

    }

    // Add a piece in the board
    int fillPiece()
    {
        int val = 1;
        val = (random.Next(0, 100) / (100 / pieces.Length)) + 1;

        return val;
    }

    int getValueAtPoint(Point p)
    {
        return board[p.x, p.y].value;
    }



    string getRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        return seed;
    }


}



[System.Serializable]
public class Node
{
    public int value; // 0 = blank, 1 = cube/yellow,  2 = sphere/red, 3 = cylinder/green, 4 = pyramid/blue, 5 = diamond/Orange, -1 = hole
    public Point index; // Where the node is

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }
}