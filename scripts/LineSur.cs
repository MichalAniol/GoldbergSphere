using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineSur : MonoBehaviour
{
    LineRenderer divideLine;
    Vector3 radius;
    GameObject GoldbergPolyhedra, LinesDividesSphera;
    int numerator = 1;

    void Start ()
    {
        radius = GetComponent<Sphera>().radius;
        GoldbergPolyhedra = GetComponent<Sphera>().goldbergPolyhedra;
        GameObject empty = GetComponent<Sphera>().empty;
        LinesDividesSphera = Instantiate(empty, Vector3.zero, Quaternion.identity);
        LinesDividesSphera.transform.parent = GoldbergPolyhedra.transform;
        LinesDividesSphera.name = "LinesDividesSphera";

        for (int i = 2; i < 11; i += 2)
        {
            DivideLine("p01", pentName(i));
            DivideLine("p12", pentName(i + 1));
        }

        for (int i = 1; i < 11; i++)
        {
            DivideLine(pentName(i + 1), pentName(i + 3));
            DivideLine(pentName(i + 1), pentName(i + 2));
        }
    }

    void DivideLine(String one, String two)
    {
        GameObject oneGO, twoGO;
        oneGO = GameObject.Find(one);
        twoGO = GameObject.Find(two);

        GameObject divideLine = new GameObject();
        divideLine.AddComponent<LineRenderer>();
        LineRenderer dl = divideLine.GetComponent<LineRenderer>();
        dl.material = new Material(Shader.Find("Sprites/Default"));
        dl.widthMultiplier = 0.02f;
        dl.positionCount = 11;
        dl.startColor = Color.red;
        dl.endColor = Color.red;

        dl.transform.parent = LinesDividesSphera.transform;
        String dlName = "dl_";
        if (numerator < 10) dl.name = dlName + "0" + numerator++.ToString();
        else dl.name = dlName + numerator++.ToString();

        for (int i = 0; i < 11; i++)
        {
            dl.SetPosition(i, Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(oneGO.transform.position, twoGO.transform.position, .1f * i)) * radius);
        }
    }

    String pentName(int num)
    {
        String pentagonName;
        if (num > 11) num -= 10;
        if (num < 10) pentagonName = "p0" + num.ToString();
        else pentagonName = "p" + num.ToString();

        return pentagonName;
    }
}
