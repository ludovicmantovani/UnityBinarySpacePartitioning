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
        _root.Split();
        //_root.Draw();
    }

    void Update()
    {
        
    }
}
