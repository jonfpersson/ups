using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class saveHoneyCombs : MonoBehaviour
{
    GameObject[] getCountOfCombs;
    GameObject[] getCountOfFilledCombs;
    GameObject[] getCountOfHoneybees;

    public GameObject hexagon;
    public GameObject filledHoneyComb;
    public GameObject honeybeeAI;

    // Is called before Start
    void Awake()
    {
        load();
    }

    // Update is called once per frame
    void Update()
    {
        getCountOfCombs = GameObject.FindGameObjectsWithTag("honeyComb");
        getCountOfFilledCombs = GameObject.FindGameObjectsWithTag("filledHoneyComb");
        getCountOfHoneybees = GameObject.FindGameObjectsWithTag("honeybee");
        //Debug.Log("getCountOfHoneybees: "+ getCountOfHoneybees.Length);
        /*if (Input.GetKeyUp(KeyCode.U))
        {
            save();
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
           // load();
        }*/
    }

    public void save()
    {
        SaveEmptyHexagons();
        SaveFilledHexagons();
        SaveHoneybeeAI();

    }

    public void load()
    {
        LoadEmptyHexagons();
        LoadFilledHexagons();
        LoadHoneybeeAI();

    }

    public void SaveEmptyHexagons()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/honeycomb.bee");
        hexagonObject ho = new hexagonObject(getCountOfCombs.Length);

        Debug.Log(getCountOfCombs.Length);
        for (int i = 0; i < getCountOfCombs.Length; i++)
        {
            ho.pos[i, 0] = getCountOfCombs[i].transform.localPosition.x;
            ho.pos[i, 1] = getCountOfCombs[i].transform.localPosition.y;
            ho.pos[i, 2] = getCountOfCombs[i].transform.localPosition.z;
        }

        for (int i = 0; i < getCountOfCombs.Length; i++)
        {
            ho.hexaRotations[i, 0] = getCountOfCombs[i].transform.rotation.x;
            ho.hexaRotations[i, 1] = getCountOfCombs[i].transform.rotation.y;
            ho.hexaRotations[i, 2] = getCountOfCombs[i].transform.rotation.z;
            ho.hexaRotations[i, 3] = getCountOfCombs[i].transform.rotation.w;
        }


        bf.Serialize(file, ho);
        file.Close();
        Debug.Log("Saved!");
    }

    public void SaveFilledHexagons()
    {
        //Handels the saving for honeycombs filled with honey

        BinaryFormatter bformatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/data.bee");
        filledHoneycomb fh = new filledHoneycomb(getCountOfFilledCombs.Length);

        for (int i = 0; i < getCountOfFilledCombs.Length; i++)
        {
            fh.pos[i, 0] = getCountOfFilledCombs[i].transform.localPosition.x;
            fh.pos[i, 1] = getCountOfFilledCombs[i].transform.localPosition.y;
            fh.pos[i, 2] = getCountOfFilledCombs[i].transform.localPosition.z;
        }

        for (int i = 0; i < getCountOfFilledCombs.Length; i++)
        {
            fh.hexaRotations[i, 0] = getCountOfFilledCombs[i].transform.rotation.x;
            fh.hexaRotations[i, 1] = getCountOfFilledCombs[i].transform.rotation.y;
            fh.hexaRotations[i, 2] = getCountOfFilledCombs[i].transform.rotation.z;
            fh.hexaRotations[i, 3] = getCountOfFilledCombs[i].transform.rotation.w;
        }


        bformatter.Serialize(saveFile, fh);
        saveFile.Close();
        Debug.Log("Saved!");

        
    }

    public void SaveHoneybeeAI()
    {
        BinaryFormatter bformatter = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/honeybees.bee");
        honeybees hb = new honeybees(getCountOfHoneybees.Length);

        for (int i = 0; i < getCountOfHoneybees.Length; i++)
        {
            hb.pos[i, 0] = getCountOfHoneybees[i].transform.localPosition.x;
            hb.pos[i, 1] = getCountOfHoneybees[i].transform.localPosition.y;
            hb.pos[i, 2] = getCountOfHoneybees[i].transform.localPosition.z;
        }

        for (int i = 0; i < getCountOfHoneybees.Length; i++)
        {
            hb.BeeRotations[i, 0] = getCountOfHoneybees[i].transform.rotation.x;
            hb.BeeRotations[i, 1] = getCountOfHoneybees[i].transform.rotation.y;
            hb.BeeRotations[i, 2] = getCountOfHoneybees[i].transform.rotation.z;
            hb.BeeRotations[i, 3] = getCountOfHoneybees[i].transform.rotation.w;
        }

        bformatter.Serialize(saveFile, hb);
        saveFile.Close();

        Application.Quit();
    }

        //Loading functions
    public void LoadEmptyHexagons()
    {
        if (File.Exists(Application.persistentDataPath + "/honeycomb.bee"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/honeycomb.bee", FileMode.Open);
            hexagonObject hb = (hexagonObject)bf.Deserialize(file);
            file.Close();

            Quaternion rotationOfCombs = new Quaternion();
            for (int i = 0; i < hb.pos.GetLength(0); i++)
            {
                rotationOfCombs.Set(hb.hexaRotations[i, 0], hb.hexaRotations[i, 1], hb.hexaRotations[i, 2], hb.hexaRotations[i, 3]);
                Instantiate(hexagon, new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]), rotationOfCombs);
                Debug.Log("pos: " + i.ToString() + ": " + new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]));
            }

        }// Debug.Log("No data found! Try saving first.");

    }

    public void LoadFilledHexagons()
    {
        //Handels the loading for honeycombs filled with honey
        if (File.Exists(Application.persistentDataPath + "/data.bee"))
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/data.bee", FileMode.Open);
            filledHoneycomb fh = (filledHoneycomb)bformatter.Deserialize(saveFile);
            saveFile.Close();

            Quaternion rotationOfCombs = new Quaternion();
            for (int i = 0; i < fh.pos.GetLength(0); i++)
            {
                rotationOfCombs.Set(fh.hexaRotations[i, 0], fh.hexaRotations[i, 1], fh.hexaRotations[i, 2], fh.hexaRotations[i, 3]);
                Instantiate(filledHoneyComb, new Vector3(fh.pos[i, 0], fh.pos[i, 1], fh.pos[i, 2]), rotationOfCombs);
                Debug.Log("Filled pos: " + i.ToString() + ": " + new Vector3(fh.pos[i, 0], fh.pos[i, 1], fh.pos[i, 2]));
            }

        }
    }

    public void LoadHoneybeeAI()
    {
        //Handels the loading for honeubees with honey
        if (File.Exists(Application.persistentDataPath + "/honeybees.bee"))
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/honeybees.bee", FileMode.Open);
            honeybees hb = (honeybees)bformatter.Deserialize(saveFile);
            saveFile.Close();

            Quaternion rotationOfBees = new Quaternion();
            for (int i = 0; i < hb.pos.GetLength(0); i++)
            {
                rotationOfBees.Set(hb.BeeRotations[i, 0], hb.BeeRotations[i, 1], hb.BeeRotations[i, 2], hb.BeeRotations[i, 3]);
                Instantiate(honeybeeAI, new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]), rotationOfBees);
                Debug.Log("Filled pos: " + i.ToString() + ": " + new Vector3(hb.pos[i, 0], hb.pos[i, 1], hb.pos[i, 2]));
            }

        }
    }
  
}

[Serializable]
class hexagonObject {
    //Own invented vector3 that is (probably) Serializable
    public float[,] pos;

    //Own invented Quaternion that is (probably) Serializable
    public float[,] hexaRotations;

    public hexagonObject(int totalObj)
    {
        hexaRotations = new float[totalObj, 4];
        pos = new float[totalObj, 3];
    }
}

[Serializable]
class filledHoneycomb
{
    //Own invented vector3 that is (probably) Serializable
    public float[,] pos;

    //Own invented Quaternion that is (probably) Serializable
    public float[,] hexaRotations;

    public filledHoneycomb(int totalObj)
    {
        hexaRotations = new float[totalObj, 4];
        pos = new float[totalObj, 3];
    }
}

[Serializable]
class honeybees
{
    //Own invented vector3 that is (probably) Serializable
    public float[,] pos;

    //Own invented Quaternion that is (probably) Serializable
    public float[,] BeeRotations;

    public honeybees(int totalObj)
    {
        BeeRotations = new float[totalObj, 4];
        pos = new float[totalObj, 3];
    }
}

