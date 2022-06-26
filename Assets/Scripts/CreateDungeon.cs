using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    public int mapWidth = 50;
    public int mapDepth = 50;
    public int scale = 2;

    Leaf _root;

    byte[,] map;

    void Start()
    {
        map = new byte[mapWidth, mapDepth];
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, z] = 1;
            }
        }

        _root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BinarySpacePartitioning(_root, 6);
        DrawMap();
    }

    void BinarySpacePartitioning(Leaf leaf, int splitDepth)
    {
        if (leaf == null) return;
        if (splitDepth <= 0)
        {
            leaf.Draw(map);
            return;
        }

        if (leaf.Split())
        {
            BinarySpacePartitioning(leaf.leftChild, splitDepth - 1);
            BinarySpacePartitioning(leaf.rightChild, splitDepth - 1);
        }
        else
        {
            leaf.Draw(map);
        }
    }

    void DrawMap()
    {
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (map[x, z] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 10, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
        }
    }
}
