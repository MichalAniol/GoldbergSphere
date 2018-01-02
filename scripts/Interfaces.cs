using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


interface IMap
{
    int enter { get; }
    string neighbourOne { get; }
    string neighbourTwo { get; }
    string neighbourThree { get; }
    string neighbourFour { get; }
    string neighbourFive { get; }
    string neighbourSix { get; }

    String GetEnterFieldAndFindNearestNeighbours(String curentField, int direction);
    String GetEnterFieldAndFindLeftNeighbour(String curentField, int direction);
    String GetEnterField(String curentField, int direction);
    void FindNearestNeighbours(String curentField, int direction);
    void FindAllNeighbours(String curentField);

    List<String> ZnajdzDroge(String one, String two);
}

interface ISphera
{
    int size { get; }
    Vector3 radius { get; }
}

interface IDrawWay
{
    void DrawTheWay(String one, String two);
}

