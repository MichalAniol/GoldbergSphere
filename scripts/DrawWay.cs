using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawWay : MonoBehaviour
{
    Map map;
    LineRenderer wayLine;
    GameObject marks;
    int numerator;

    void Start()
    {
        map = gameObject.AddComponent<Map>();

        GameObject GoldbergPolyhedra = GetComponent<Sphera>().goldbergPolyhedra;
        GameObject empty = GetComponent<Sphera>().empty;
        marks = Instantiate(empty, Vector3.zero, Quaternion.identity);
        marks.name = "Marks";
        numerator = 1;
    }

    public void DrawTheWay(String one, String two)
    {
        List<String> droga = map.ZnajdzDroge(one, two);

        GameObject wayLine = new GameObject();
        wayLine.AddComponent<LineRenderer>();
        LineRenderer wl = wayLine.GetComponent<LineRenderer>();
        wl.material = new Material(Shader.Find("Sprites/Default"));
        wl.widthMultiplier = 0.03f;
        wl.positionCount = droga.Count + 1;
        wl.startColor = Color.white;
        wl.endColor = Color.white;
        wl.transform.parent = marks.transform;
        wl.name = "wayLine" + numerator++.ToString();

        wl.SetPosition(0, GameObject.Find(one).transform.position);

        for (int i = 0; i < droga.Count; i++)
        {
            wl.SetPosition(i + 1, GameObject.Find(droga[i]).transform.position);
        }
    }

}
