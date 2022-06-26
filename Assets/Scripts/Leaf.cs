using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
    int _xpos;
    int _zpos;
    int _width;
    int _depth;
    int _scale;
    int _roomMin = 5;

    public Leaf leftChild;
    public Leaf rightChild;

    public Leaf(int xpos, int zpos, int width, int depth, int scale)
    {
        _xpos = xpos;
        _zpos = zpos;
        _width = width;
        _depth = depth;
        _scale = scale;
    }

    public bool Split()
    {
        if (_width <= _roomMin || _depth <= _roomMin) return false;

        bool splitHorizontal = Random.Range(0, 100) > 50;
        if (_width > _depth && _width/_depth >= 1.2)
        {
            splitHorizontal = false;
        }
        else if (_depth > _width && _depth/_width >= 1.2)
        {
            splitHorizontal = true;
        }

        int max = (splitHorizontal ? _depth : _width) - _roomMin;
        if (max <= _roomMin)
        {
            return false;
        }

        if (splitHorizontal)
        {
            int l1Depth = Random.Range(_roomMin, max);
            leftChild = new Leaf(_xpos, _zpos, _width, l1Depth, _scale);
            rightChild = new Leaf(_xpos, _zpos + l1Depth, _width, _depth - l1Depth, _scale);
        }
        else
        {
            int l1Width = Random.Range(_roomMin, max);
            leftChild = new Leaf(_xpos, _zpos, l1Width, _depth, _scale);
            rightChild = new Leaf(_xpos + l1Width, _zpos, _width - l1Width, _depth, _scale);
        }

        return true;
    }

    public void Draw(byte[,] map)
    {
        Color c = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
        for (int x = _xpos; x < _width + _xpos; x++)
        {
            for (int z = _zpos; z < _depth + _zpos; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x * _scale, 0, z * _scale);
                cube.transform.localScale = new Vector3(_scale, _scale, _scale);
                cube.GetComponent<Renderer>().material.SetColor("_Color", c);
            }
        }

        for (int x = _xpos + 1; x < _width + _xpos - 1; x++)
        {
            for (int z = _zpos + 1; z < _depth + _zpos - 1; z++)
            {
                map[x, z] = 0;
            }
        }
    }
}
