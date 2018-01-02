using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sphera : MonoBehaviour, ISphera
{
    public GameObject empty;

    public GameObject goldbergPolyhedra;
    GameObject peaks;
    GameObject surface;
    public GameObject wallsMarkers;
    public GameObject ball;
    public GameObject peak;
    public GameObject tileCenter;

    public GameObject wall;
    public GameObject pillar;

    Map map;

    public Vector3 radius { get; private set; }
    public int size { get; private set; }


    Quaternion angle90 = Quaternion.Euler(90, 0, 0);
    float proportion, alfa;

    public GameObject one, two;
    public Material red;

    private void Awake()
    {
        map = gameObject.AddComponent<Map>();
        goldbergPolyhedra = Instantiate(empty, Vector3.zero, Quaternion.identity);
        goldbergPolyhedra.name = "GoldbergPolyhedra";

        GameObject setBall = Instantiate(ball, Vector3.zero, Quaternion.identity);
        setBall.transform.parent = goldbergPolyhedra.transform;
        setBall.name = "Ball";

        peaks = Instantiate(empty, Vector3.zero, Quaternion.identity);
        peaks.name = "Peaks";
        peaks.transform.parent = goldbergPolyhedra.transform;

        surface = Instantiate(empty, Vector3.zero, Quaternion.identity);
        surface.name = "Surfaces";
        surface.transform.parent = goldbergPolyhedra.transform;

        radius = new Vector3(0, 5, 0);
      //  one = Instantiate(empty, Vector3.zero, Quaternion.identity);

        size = PlayerPrefs.GetInt("size"); /// <summary> nubers of tiles betwin pentagon </summary>
        Map.size = size;

        DrawGoldbergPolyhedra();
        RotateTailsAndPeaks();

    }

    void DrawPillar(string one, string two, string three, string pillarName, GameObject paretnPillarSurface)
    {
        GameObject first = GameObject.Find(one);
        GameObject second = GameObject.Find(two);
        GameObject third = GameObject.Find(three);

        Vector3 placement = Quaternion.FromToRotation(Vector3.up, new Vector3( /// centr of 3 vectors
            (first.transform.position.x + second.transform.position.x + third.transform.position.x) / 3,
            (first.transform.position.y + second.transform.position.y + third.transform.position.y) / 3,
            (first.transform.position.z + second.transform.position.z + third.transform.position.z) / 3)) * radius;

        GameObject setPillar = Instantiate(pillar, placement, Quaternion.FromToRotation(Vector3.up, placement));
        setPillar.name = pillarName;
        setPillar.transform.parent = paretnPillarSurface.transform;
    }

    void DrawWall(string one, string two)
    {
        GameObject first = GameObject.Find(one);
        GameObject second = GameObject.Find(two);

        Vector3 betwieen = first.transform.position - second.transform.position;

        Vector3 placement = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(first.transform.position, second.transform.position, .5f)) * radius;
        Quaternion azimuth = Quaternion.LookRotation(betwieen, placement);
        Vector3 scale = new Vector3(Vector3.Magnitude(betwieen) - wall.transform.localScale.y, wall.transform.localScale.y, wall.transform.localScale.z);

        Instantiate(wall, placement, azimuth).transform.localScale = scale;
    }

    void DrawGoldbergPolyhedra()
    {
        float a = 1; /// size of ball
        float R = a * Mathf.Sqrt(10 + (2 * Mathf.Sqrt(5))) / 4; /// radius to peaks

        float sinR = (a / 2) / R; /// sinus betwien two colses radiouses
        alfa = Mathf.Asin(sinR) / (Mathf.PI / 180) * 2; /// angle betwien two colses radiouses

        float h5 = 1.6f; /// changing propotions coz better view efect         
       // float h5 = Mathf.Sqrt(50 + (10 * Mathf.Sqrt(5))) / 10; /// hight of tringule of pentagon tile
        float h6 = 1; /// hight of tringule of hexalgon tile
        proportion = (h6 + h5) / ((h6 * 2 * size) + h6 + h5); /// lenght betwien peaks and peak and centre of nearest hexalgon tile 


        GameObject drawPeak;
        drawPeak = Instantiate(peak, radius, Quaternion.identity); drawPeak.name = "p01";
        drawPeak.transform.parent = peaks.transform;
        drawPeak.GetComponent<Renderer>().material = red;

        for (int i = 2; i < 12; i++)
        {
            String peakName;
            drawPeak = Instantiate(peak, Quaternion.Euler(0, -36 * i, (180 * (i % 2)) + (alfa * (1 - ((i % 2) * 2)))) * radius, Quaternion.identity);
            if (i < 10) peakName = "p0" + i.ToString(); else peakName = "p" + i.ToString();
            drawPeak.name = peakName; drawPeak.transform.parent = peaks.transform;
        }

        drawPeak = Instantiate(peak, Quaternion.Euler(0, 0, 180) * radius, Quaternion.identity); drawPeak.name = "p12";
        drawPeak.transform.parent = peaks.transform;
        drawPeak.GetComponent<Renderer>().material = red;

        for (int i = 1; i < 21; i++)
        {
            int[] leftPent = { 2, 4, 3, 5 };
            int[] rightPent = { 4, 2, 5, 3 };
            int peakNum = 0;
            String peak1 = "", peak2 = "", peak3 = "", surNum = "";

            if (i < 10) surNum = "0" + i.ToString() + "s"; else surNum = i.ToString() + "s";

            if ((i + 3) % 4 == 0) peakNum = 1;
            if (((i / 2) + 1) % 2 == 0) { peakNum = (i / 2) + 2 + (i % 2); if (peakNum == 12) peakNum -= 10; }
            if (i % 4 == 0) peakNum = 12;
            if (peakNum < 10) peak1 = "p0" + peakNum.ToString(); else peak1 = "p" + peakNum.ToString();

            peakNum = leftPent[i - (((i - 1) / 4) * 4) - 1] + (((i - 1) / 4) * 2); if (peakNum > 11) peakNum -= 10;
            if (peakNum < 10) peak2 = "p0" + peakNum.ToString(); else peak2 = "p" + peakNum.ToString();

            peakNum = rightPent[i - (((i - 1) / 4) * 4) - 1] + (((i - 1) / 4) * 2); if (peakNum > 11) peakNum -= 10;
            if (peakNum < 10) peak3 = "p0" + peakNum.ToString(); else peak3 = "p" + peakNum.ToString();

            AlginSurfaces(GameObject.Find(peak1), GameObject.Find(peak2), GameObject.Find(peak3), surNum);
        }
    }

    void AlginSurfaces(GameObject one, GameObject two, GameObject three, string partName)
    {
        GameObject tile;
        GameObject tile1, tile2, tile3;

        GameObject surfacePart = Instantiate(empty, Vector3.zero, Quaternion.identity);
        surfacePart.name = partName + "urfaces"; surfacePart.transform.parent = surface.transform;

        for (int q = 0; q < size; q++)
        {
            for (int i = 0; i < q + 1; i++)
            {
                tile = Instantiate(tileCenter, radius, Quaternion.identity); tile.transform.parent = surfacePart.transform;
                tile.name = partName + (q + 1).ToString() + "." + (i + 1).ToString();
            }
        }

        tile1 = GameObject.Find(partName + "1.1");
        tile2 = GameObject.Find(partName + size.ToString() + ".1");
        tile3 = GameObject.Find(partName + size.ToString() + "." + size.ToString());

        for (int i = 0; i < 7; i++) /// algin surface to peaks
        {
            tile1.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(one.transform.position, tile2.transform.position, proportion)) * radius;
            tile2.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(two.transform.position, tile3.transform.position, proportion)) * radius;
            tile3.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(three.transform.position, tile1.transform.position, proportion)) * radius;
        }

        for (int i = 2; i < size; i++)
        {
            tile = GameObject.Find(partName + i.ToString() + ".1");
            tile.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(tile1.transform.position, tile2.transform.position, (1f / (size - 1f)) * (i - 1f))) * radius;
        }

        for (int i = 2; i < size; i++)
        {
            tile = GameObject.Find(partName + size.ToString() + "." + i.ToString());
            tile.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(tile3.transform.position, tile2.transform.position, 1f - (1f / (size - 1f)) * (i - 1f))) * radius;
        }

        for (int i = 2; i < size; i++)
        {
            tile = GameObject.Find(partName + i.ToString() + "." + i.ToString());
            tile.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(tile1.transform.position, tile3.transform.position, (1f / (size - 1f)) * (i - 1f))) * radius;
        }

        for (int i = 3; i < size; i++)
        {
            tile1 = GameObject.Find(partName + i.ToString() + ".1");
            tile3 = GameObject.Find(partName + i.ToString() + "." + i.ToString());

            for (int q = 2; q < i; q++)
            {
                tile2 = GameObject.Find(partName + i.ToString() + "." + q.ToString());
                tile2.transform.position = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(tile1.transform.position, tile3.transform.position, (1f / (i - 1f)) * (q - 1f))) * radius;
            }
        }
    }

    void RotateTailsAndPeaks()
    {
        GameObject tile1, tile2, tile3;
        String peakName;

        for (int i = 1; i < 21; i++)
        {
            int[] leftPent = { 2, 4, 3, 5 };
            int[] rightPent = { 4, 2, 5, 3 };
            int peakNum = 0;
            String peak1 = "", peak2 = "", peak3 = "", surNum = "";

            if (i < 10) surNum = "0" + i.ToString() + "s"; else surNum = i.ToString() + "s";

            if ((i + 3) % 4 == 0) peakNum = 1;
            if (((i / 2) + 1) % 2 == 0) { peakNum = (i / 2) + 2 + (i % 2); if (peakNum == 12) peakNum -= 10; }
            if (i % 4 == 0) peakNum = 12;
            if (peakNum < 10) peak1 = "p0" + peakNum.ToString(); else peak1 = "p" + peakNum.ToString();

            peakNum = leftPent[i - (((i - 1) / 4) * 4) - 1] + (((i - 1) / 4) * 2); if (peakNum > 11) peakNum -= 10;
            if (peakNum < 10) peak2 = "p0" + peakNum.ToString(); else peak2 = "p" + peakNum.ToString();

            peakNum = rightPent[i - (((i - 1) / 4) * 4) - 1] + (((i - 1) / 4) * 2); if (peakNum > 11) peakNum -= 10;
            if (peakNum < 10) peak3 = "p0" + peakNum.ToString(); else peak3 = "p" + peakNum.ToString();

            bool upTile = false;
            if ((int.Parse((surNum[1] + surNum[2]).ToString())) % 2 != 1) upTile = true;
            int direction;

            if (upTile) direction = 1; else direction = 4;

            for (int q = 0; q < size; q++)
            {
                for (int w = 0; w < q + 1; w++)
                {
                    String tileName = surNum + (q + 1).ToString() + "." + (w + 1).ToString();
                    String aaa = map.GetEnterFieldAndFindLeftNeighbour(tileName, direction);

                    tile1 = GameObject.Find(tileName);
                    tile2 = GameObject.Find(map.GetEnterFieldAndFindLeftNeighbour(tileName, direction));
                    tile3 = GameObject.Find(map.neighbourOne);

                    Vector3 betwieen = Quaternion.FromToRotation(Vector3.up, Vector3.Lerp(tile1.transform.position, tile2.transform.position, .5f)) * radius;
                    tile1.transform.rotation = Quaternion.LookRotation(tile1.transform.position, tile3.transform.position - betwieen) * angle90;
                }
            }
        }

        for (int i = 1; i < 13; i++)
        {
            if (i < 10) peakName = "p0" + i.ToString(); else peakName = "p" + i.ToString();

            tile1 = GameObject.Find(peakName);
            tile2 = GameObject.Find(map.GetEnterField(peakName, 1));

            tile1.transform.rotation = Quaternion.LookRotation(tile1.transform.position, tile1.transform.position - tile2.transform.position) * angle90;
        }
    }
}

