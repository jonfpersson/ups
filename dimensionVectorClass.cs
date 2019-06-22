using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class dimensionsVector {
    //Own invented vector3
    public float[,] pos;

    //Own invented Quaternion
    public float[,] rotations;

    public dimensionsVector(int totalObj)
    {
        rotations = new float[totalObj, 4];
        pos = new float[totalObj, 3];
    }
}

