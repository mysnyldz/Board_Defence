using System;
using Data.ValueObject;
using Enums;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace UnityTemplateProjects.Controller
{
    public class GridCreatorController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serializefield Variables

        [SerializeField] private GameObject placeableGround;
        [SerializeField] private GameObject ground;
        [SerializeField] private GameObject holder;

        #endregion

        #region Private Variables

        private int _width;
        private int _height;
        private int _cellSpaceSize;
        private GameObject[,] _gridArray;

        #endregion

        #endregion


        public void GridCreator(int width, int height, int cellPaceSize)
        {
            _width = width;
            _height = height;
            _cellSpaceSize = cellPaceSize;

            _gridArray = new GameObject[_width, _height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    Vector3 position = new Vector3(x * _cellSpaceSize, 0, z * _cellSpaceSize);
                    if (z < _height / 2)
                    {
                        _gridArray[x, z] = Instantiate(placeableGround, position,
                            quaternion.identity);
                    }
                    else
                    {
                        _gridArray[x, z] = Instantiate(ground, position,
                            quaternion.identity);
                    }

                    _gridArray[x, z].transform.parent = holder.transform;
                    _gridArray[x, z].gameObject.name = "( x: " + x.ToString() + "z: " + z.ToString() + " )";
                }
            }
        }
    }
}