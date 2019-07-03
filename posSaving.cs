using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class posSaving : MonoBehaviour
{
    GameObject[] getCountOfObjects;
    
    public GameObject gameObject;
    public string objectName;
    public string extension;

    // Is called before Start
    void Awake() { load(); }

    // Update is called once per frame
    void Update(){ getCountOfObjects = GameObject.FindGameObjectsWithTag(objectName); }

    public void save(){ writeToFile(); }

    public void load() { loadFromFile(); }

    public void writeToFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + objectName + "." + extension);
        dimensionsVector dv = new dimensionsVector(getCountOfObjects.Length);

        for (int i = 0; i < getCountOfObjects.Length; i++)
        {
            dv.pos[i, 0] = getCountOfObjects[i].transform.localPosition.x;
            dv.pos[i, 1] = getCountOfObjects[i].transform.localPosition.y;
            dv.pos[i, 2] = getCountOfObjects[i].transform.localPosition.z;
        }

        for (int i = 0; i < getCountOfObjects.Length; i++)
        {
            dv.rotations[i, 0] = getCountOfObjects[i].transform.rotations.x;
            dv.rotations[i, 1] = getCountOfObjects[i].transform.rotations.y;
            dv.rotations[i, 2] = getCountOfObjects[i].transform.rotations.z;
            dv.rotations[i, 3] = getCountOfObjects[i].transform.rotations.w;
        }


        bf.Serialize(file, dv);
        file.Close();
        Debug.Log("Saved!");
    }

        //Loading functions
    public void loadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "/" + objectName + "." + extension))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + objectName + "." + extension, FileMode.Open);
            dimensionsVector hb = (dimensionsVector)bf.Deserialize(file);
            file.Close();

            Quaternion rotationOfObjects = new Quaternion();
            for (int i = 0; i < hb.pos.GetLength(0); i++)
            {
                rotationOfObjects.Set(hb.rotations[i, 0], hb.rotations[i, 1], hb.rotations[i, 2], hb.rotations[i, 3]);
                Instantiate(gameObject, new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]), rotationOfObjects);
                Debug.Log("pos: " + i.ToString() + ": " + new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]));
            }

        }// Debug.Log("No data found! Try saving first.");

    }
  
}