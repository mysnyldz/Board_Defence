﻿using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PoolValueData
    {
        [Range(0,200)]
        public int ObjectLimit;
        public GameObject PooledObject;
    }
}