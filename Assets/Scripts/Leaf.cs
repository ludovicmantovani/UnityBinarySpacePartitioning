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

    public Leaf(int xpos, int zpos, int width, int depth, int scale)
    {
        _xpos = xpos;
        _zpos = zpos;
        _width = width;
        _depth = depth;
        _scale = scale;
    }

    public void Draw()
    {
        for (int x = _xpos; x < _width; x++)
        {
            for (int z = _zpos; z < _depth; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x * _scale, 0, z * _scale);
                cube.transform.localScale = new Vector3(_scale, _scale, _scale);
            }
        }
    }
}
