using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    public int mapWidth = 50;
    public int mapDepth = 50;
    public int scale = 2;

    Leaf _root;

    void Start()
    {
        _root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BinarySpacePartitioning(_root, 6);
    }

    void BinarySpacePartitioning(Leaf leaf, int splitDepth)
    {
        if (leaf == null) return;
        if (splitDepth <= 0)
        {
            leaf.Draw(0);
            return;
        }

        if (leaf.Split())
        {
            BinarySpacePartitioning(leaf.leftChild, splitDepth - 1);
            BinarySpacePartitioning(leaf.rightChild, splitDepth - 1);
        }
        else
        {
            leaf.Draw(0);
        }
    }

    void Update()
    {
        
    }
}
