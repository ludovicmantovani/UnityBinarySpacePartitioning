using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{
    [SerializeField] private int mapWidth = 50;
    [SerializeField] private int mapDepth = 50;
    [SerializeField] private int scale = 2;
    [SerializeField] private Material wallMaterial = null;

    private Leaf _root;

    private byte[,] map;
    private List<Vector2> corridors = null;

    void Start()
    {
        //Generate();
    }

    public void Regenerate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Generate();
    }
    private void Generate()
    {
        map = new byte[mapWidth, mapDepth];
        corridors = new List<Vector2>();
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, z] = 1;
            }
        }

        _root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BinarySpacePartitioning(_root, 6);

        AddCorridors();
        AddRandomCorridors(10);

        DrawMap();
    }

    void AddRandomCorridors(int numHalls)
    {
        for (int i = 0; i < numHalls; i++)
        {
            int startX = Random.Range(5, mapWidth - 5);
            int startZ = Random.Range(5, mapDepth - 5);
            int length = Random.Range(5, mapWidth - 5);

            if (Random.Range(0, 100) < 50)
            {
                line(startX, startZ, length, startZ);
            }
            else
            {
                line(startX, startZ, startX, length);
            }
        }
    }


    void BinarySpacePartitioning(Leaf leaf, int splitDepth)
    {
        if (leaf == null) return;
        if (splitDepth <= 0)
        {
            leaf.Draw(map);
            corridors.Add(
                new Vector2(
                    leaf.Xpos + leaf.Width / 2,
                    leaf.Zpos + leaf.Depth / 2));
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
            corridors.Add(
                new Vector2(
                    leaf.Xpos + leaf.Width / 2,
                    leaf.Zpos + leaf.Depth / 2));
        }
    }

    void AddCorridors()
    {
        for (int i = 1; i < corridors.Count; i++)
        {
            if ((int)corridors[i].x == (int)corridors[i - 1].x || (int)corridors[i].y == (int)corridors[i - 1].y)
            {
                line(
                (int)corridors[i].x, (int)corridors[i].y,
                (int)corridors[i - 1].x, (int)corridors[i - 1].y);
            }
            else
            {
                line(
                (int)corridors[i].x, (int)corridors[i].y,
                (int)corridors[i].x, (int)corridors[i - 1].y);
                line(
                (int)corridors[i].x, (int)corridors[i].y,
                (int)corridors[i - 1].x, (int)corridors[i].y);
            }
        }
    }
    private void DrawMap(bool full = false)
    {
        List<GameObject> walls = new List<GameObject>();
        List<GameObject> emptees = new List<GameObject>();

        GameObject empty = new GameObject("Empty");
        empty.transform.SetParent(transform);

        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (map[x, z] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.SetActive(false);
                    cube.transform.position = new Vector3(x * scale, 1, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                    if (wallMaterial != null)
                        cube.GetComponent<Renderer>().material = wallMaterial;
                    else
                        cube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    cube.transform.SetParent(transform);
                    walls.Add(cube);
                }
                else if (map[x, z] == 0)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.SetActive(false);
                    cube.transform.position = new Vector3(x * scale, -0.5f, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                    cube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    cube.transform.SetParent(empty.transform);
                    emptees.Add(cube);
                }
            }
        }

        foreach (GameObject go in walls) go.SetActive(true);
        //empty.transform.position.Set(0f, -1f, 0f);
        foreach (GameObject go in emptees) {
            //go.transform.position.Set(0f, -1f, 0f);
            go.SetActive(true);
        }
    }

    public void line(int x, int y, int x2, int y2)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            map[x, y] = 0;
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }


}
