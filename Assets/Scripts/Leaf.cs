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

    Leaf _leftChild;
    Leaf _rightChild;

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
        if (Random.Range(0, 100) < 50)
        {
            int l1Depth = Random.Range((int)(_depth * 0.1f), (int)(_depth * 0.7f));
            _leftChild = new Leaf(_xpos, _zpos, _width, l1Depth, _scale);
            _rightChild = new Leaf(_xpos, _zpos + l1Depth, _width, _depth - l1Depth, _scale);
        }
        else
        {
            int l1Width = Random.Range((int)(_width * 0.1f), (int)(_width * 0.7f));
            _leftChild = new Leaf(_xpos, _zpos, l1Width, _depth, _scale);
            _rightChild = new Leaf(_xpos + l1Width, _zpos, _width - l1Width, _depth, _scale);
        }


        _leftChild.Draw();
        _rightChild.Draw();

        return true;
    }

    public void Draw()
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
    }
}
