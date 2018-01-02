using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LinePenHex : MonoBehaviour
{

    Map map;

    GameObject GoldbergPolyhedra, HexagonsAndPhentagons;
    
    Vector3 radius = new Vector3(0, 5, 0);

    void Start()
    {
        map = gameObject.AddComponent<Map>();

        GoldbergPolyhedra = GetComponent<Sphera>().goldbergPolyhedra;
        GameObject empty = GetComponent<Sphera>().empty;
        HexagonsAndPhentagons = Instantiate(empty, Vector3.zero, Quaternion.identity);
        HexagonsAndPhentagons.transform.parent = GoldbergPolyhedra.transform;
        HexagonsAndPhentagons.name = "HexagonsAndPhentagons";


        for (int i = 1; i < 13; i++)
        {
            String pentName = "";
            if (i < 10) pentName = "p0" + i.ToString();
            else pentName = "p" + i.ToString();

            DrawPentagon(pentName);
        }

        for (int s = 1; s < 21; s++)
        {
            for (int i = 1; i < Map.size + 1; i++)
            {
                for (int q = 1; q < i + 1; q++)
                {
                    if (s < 10) DrawHexagon("0" + s.ToString() + "s" + i.ToString() + "." + q.ToString());
                    else DrawHexagon(s.ToString() + "s" + i.ToString() + "." + q.ToString());
                }
            }
        }
    }

    void DrawHexagon(String curentField)
    {
        GameObject hexCenter;
        hexCenter = GameObject.Find(curentField);

        map.FindAllNeighbours(curentField);

        GameObject hexGO1 = GameObject.Find(map.neighbourOne);
        GameObject hexGO2 = GameObject.Find(map.neighbourTwo);
        GameObject hexGO3 = GameObject.Find(map.neighbourThree);
        GameObject hexGO4 = GameObject.Find(map.neighbourFour);
        GameObject hexGO5 = GameObject.Find(map.neighbourFive);
        GameObject hexGO6 = GameObject.Find(map.neighbourSix);

        Vector3 hexPeak1 = LerpThreeVectors(hexGO1, hexGO2, hexCenter);
        Vector3 hexPeak2 = LerpThreeVectors(hexGO2, hexGO3, hexCenter);
        Vector3 hexPeak3 = LerpThreeVectors(hexGO3, hexGO4, hexCenter);
        Vector3 hexPeak4 = LerpThreeVectors(hexGO4, hexGO5, hexCenter);
        Vector3 hexPeak5 = LerpThreeVectors(hexGO5, hexGO6, hexCenter);
        Vector3 hexPeak6 = LerpThreeVectors(hexGO6, hexGO1, hexCenter);

        GameObject hexLine = new GameObject();
        hexLine.AddComponent<LineRenderer>();
        LineRenderer hl = hexLine.GetComponent<LineRenderer>();
        hl.material = new Material(Shader.Find("Sprites/Default"));
        hl.widthMultiplier = 0.03f;
        hl.positionCount = 7;
        hl.startColor = Color.green;
        hl.endColor = Color.green;

        hl.SetPosition(0, hexPeak1);
        hl.SetPosition(1, hexPeak2);
        hl.SetPosition(2, hexPeak3);
        hl.SetPosition(3, hexPeak4);
        hl.SetPosition(4, hexPeak5);
        hl.SetPosition(5, hexPeak6);
        hl.SetPosition(6, hexPeak1);

        hl.transform.parent = HexagonsAndPhentagons.transform;
        String hexName = curentField[0].ToString() + curentField[1].ToString() + "hl_";
        for (int i = 3; i < curentField.Length; i++) hexName += curentField[i];
        hl.name = hexName;
    }

    void DrawPentagon(String curentField)
    {
        GameObject pentCenter;
        pentCenter = GameObject.Find(curentField);

        map.FindAllNeighbours(curentField);

        GameObject pentGO1 = GameObject.Find(map.neighbourOne);
        GameObject pentGO2 = GameObject.Find(map.neighbourTwo);
        GameObject pentGO3 = GameObject.Find(map.neighbourThree);
        GameObject pentGO4 = GameObject.Find(map.neighbourFour);
        GameObject pentGO5 = GameObject.Find(map.neighbourFive);

        Vector3 pentPeak1 = LerpThreeVectors(pentGO1, pentGO2, pentCenter);
        Vector3 pentPeak2 = LerpThreeVectors(pentGO2, pentGO3, pentCenter);
        Vector3 pentPeak3 = LerpThreeVectors(pentGO3, pentGO4, pentCenter);
        Vector3 pentPeak4 = LerpThreeVectors(pentGO4, pentGO5, pentCenter);
        Vector3 pentPeak5 = LerpThreeVectors(pentGO5, pentGO1, pentCenter);

        GameObject pentLine = new GameObject();
        pentLine.AddComponent<LineRenderer>();
        LineRenderer pl = pentLine.GetComponent<LineRenderer>();
        pl.material = new Material(Shader.Find("Sprites/Default"));
        pl.widthMultiplier = 0.03f;
        pl.positionCount = 6;
        pl.startColor = Color.green;
        pl.endColor = Color.green;

        pl.SetPosition(0, pentPeak1);
        pl.SetPosition(1, pentPeak2);
        pl.SetPosition(2, pentPeak3);
        pl.SetPosition(3, pentPeak4);
        pl.SetPosition(4, pentPeak5);
        pl.SetPosition(5, pentPeak1);

        pl.transform.parent = HexagonsAndPhentagons.transform;
        String hexName = "pl_" + curentField[1].ToString() + curentField[2].ToString();
        for (int i = 3; i < curentField.Length; i++) hexName += curentField[i];
        pl.name = hexName;
    }


    Vector3 LerpThreeVectors(GameObject one, GameObject two, GameObject three)
    {
        return new Vector3(
            (one.transform.position.x + two.transform.position.x + three.transform.position.x) / 3,
            (one.transform.position.y + two.transform.position.y + three.transform.position.y) / 3,
            (one.transform.position.z + two.transform.position.z + three.transform.position.z) / 3);
    }

}

