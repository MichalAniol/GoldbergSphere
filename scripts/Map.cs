using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Map : MonoBehaviour, IMap
{
    public static int size { get; set; }

    public int enter { get; private set; }
    public string neighbourOne { get; private set; }
    public string neighbourTwo { get; private set; }
    public string neighbourThree { get; private set; }
    public string neighbourFour { get; private set; }
    public string neighbourFive { get; private set; }
    public string neighbourSix { get; private set; }

    public int surface, fieldOne, fieldTwo;


    public String GetEnterFieldAndFindNearestNeighbours(String curentField, int direction)
    {
        int neighbourDirection = direction - 1;
        if (curentField[0] == 'p' && neighbourDirection == 0) neighbourDirection = 5;

        if (neighbourDirection == 0) neighbourDirection = 6;

        neighbourOne = GetPositionWithEnter(curentField, neighbourDirection);

        neighbourDirection = direction + 1;
        if (curentField[0] == 'p' && neighbourDirection == 6) neighbourDirection = 1;

        if (neighbourDirection == 7) neighbourDirection = 1;
        neighbourTwo = GetPositionWithEnter(curentField, neighbourDirection);

        return GetPositionWithEnter(curentField, direction);
    }

    public String GetEnterFieldAndFindLeftNeighbour(String curentField, int direction)
    {
        int neighbourDirection = direction - 1;
        if (neighbourDirection == 0) neighbourDirection = 6;
        neighbourOne = GetPositionWithEnter(curentField, neighbourDirection);
        neighbourTwo = "null!";

        return GetPositionWithEnter(curentField, direction);
    }

    public String GetEnterField(String curentField, int direction)
    {
        return GetPositionWithEnter(curentField, direction);
    }

    public void FindNearestNeighbours(String curentField, int direction)
    {
        int neighbourDirection = direction - 1;
        if (curentField[0] == 'p' && neighbourDirection == 0) neighbourDirection = 5;

        if (neighbourDirection == 0) neighbourDirection = 6;

        neighbourOne = GetPositionWithEnter(curentField, neighbourDirection);

        neighbourDirection = direction + 1;
        if (curentField[0] == 'p' && neighbourDirection == 6) neighbourDirection = 1;

        if (neighbourDirection == 7) neighbourDirection = 1;
        neighbourTwo = GetPositionWithEnter(curentField, neighbourDirection);
    }

    public void FindAllNeighbours(String curentField)
    {
        neighbourOne = GetPosition(curentField, 1);
        neighbourTwo = GetPosition(curentField, 2);
        neighbourThree = GetPosition(curentField, 3);
        neighbourFour = GetPosition(curentField, 4);
        neighbourFive = GetPosition(curentField, 5);

        if (curentField[0] == 'p') neighbourSix = null;
        else neighbourSix = GetPosition(curentField, 6);
    }

    public List<String> ZnajdzDroge(String one, String two)
    {
        List<String> droga = new List<String>();
        String nearestWayPoint = one;

        GameObject goal = GameObject.Find(two);
        Vector3 goalVec = goal.transform.position;

        GameObject cureField = GameObject.Find(one);
        List<float> sasiedzi = new List<float>();
         do
         {
            FindAllNeighbours(nearestWayPoint);
            float nearDist1 = Vector3.Magnitude(GameObject.Find(neighbourOne).transform.position - goalVec); sasiedzi.Add(nearDist1);
            float nearDist2 = Vector3.Magnitude(GameObject.Find(neighbourTwo).transform.position - goalVec); sasiedzi.Add(nearDist2);
            float nearDist3 = Vector3.Magnitude(GameObject.Find(neighbourThree).transform.position - goalVec); sasiedzi.Add(nearDist3);
            float nearDist4 = Vector3.Magnitude(GameObject.Find(neighbourFour).transform.position - goalVec); sasiedzi.Add(nearDist4);
            float nearDist5 = Vector3.Magnitude(GameObject.Find(neighbourFive).transform.position - goalVec); sasiedzi.Add(nearDist5);
            float nearDist6;
            if (neighbourSix != null) nearDist6 = Vector3.Magnitude(GameObject.Find(neighbourSix).transform.position - goalVec); else nearDist6 = 999999f; sasiedzi.Add(nearDist6);

            switch (sasiedzi.IndexOf(sasiedzi.Min()))
            {
                case 0: nearestWayPoint = neighbourOne; break;
                case 1: nearestWayPoint = neighbourTwo; break;
                case 2: nearestWayPoint = neighbourThree; break;
                case 3: nearestWayPoint = neighbourFour; break;
                case 4: nearestWayPoint = neighbourFive; break;
                case 5: nearestWayPoint = neighbourSix; break;
            }
            droga.Add(nearestWayPoint);
            sasiedzi.Clear();

        } while (nearestWayPoint != two);

        return droga;
    }

    public List<String> ZaznaczKoło(String one, float angle)
    {
        List<String> aaa = new List<string>();
        return aaa;
    }

    String GetPositionWithEnter(String curentField, int direction)
    {
        String enterField = "";
        String numOne = "", numTwo = "";
        bool nextNum = false;
        int num = 0;

        if (curentField[0] == 'p')
        {
            surface = int.Parse(("" + curentField[1] + curentField[2]).ToString());

            switch (surface)
            {
                case 1:
                    num = ((direction * 4) - 21) * -1; if (num < 10) enterField = "0" + num.ToString() + "s1.1"; else enterField = num.ToString() + "s1.1";
                    enter = 1; break;
                case 12:
                    num = direction * 4; if (num < 10) enterField = "0" + num.ToString() + "s1.1"; else enterField = num.ToString() + "s1.1";
                    enter = 1; break;
                default:
                    if (surface % 2 == 0)
                    {
                        num = direction - 5 * ((direction - 1) / 5);
                        if (num > 2) num = (num - 2) * -1;
                        num += ((surface / 2) - 1) * 4;
                        if (num < 0) num += 20;
                    }
                    else
                    {
                        num = (direction - 5 * ((direction - 1) / 5)) - 1;
                        if (num == 1) num = -1;
                        num += ((surface / 2) - 1) * 4;
                        if (num < 1) num += 20;
                    }
                    if (num < 10) enterField = "0" + num.ToString() + "s"; else enterField = num.ToString() + "s";

                    switch (direction)
                    {
                        case 1: enterField += size.ToString() + ".1"; break;
                        case 2: enterField += size.ToString() + "." + size.ToString(); break;
                        case 3: enterField += "1.1"; break;
                        case 4: enterField += size.ToString() + ".1"; break;
                        case 5: enterField += size.ToString() + "." + size.ToString(); break;
                    }

                    num = (direction - 3 * ((direction - 1) / 3));
                    switch (num)
                    {
                        case 1: enter = 5; break;
                        case 2: enter = 3; break;
                        default: enter = 1; break;
                    }
                    break;
            }
            return enterField;
        }



        surface = int.Parse(("" + curentField[0] + curentField[1]).ToString());
        for (int i = 3; i < curentField.Length; i++)
        {
            if (curentField[i] == '.') { nextNum = true; continue; }
            if (!nextNum) numOne += curentField[i];
            if (nextNum) numTwo += curentField[i];
        }
        fieldOne = int.Parse(numOne.ToString());
        fieldTwo = int.Parse(numTwo.ToString());

        bool upField = false;
        if (surface % 2 != 0) upField = true;

        if (fieldOne == 1 && fieldTwo == 1)
        {
            switch (direction)
            {
                case 1:
                    if ((surface + 3) % 4 == 0) { num = 1; enter = (((surface - 1) / 4) - 5) * -1; }
                    if (((surface / 2) + 1) % 2 == 0)
                    {
                        num = (surface / 2) + 2 + (surface % 2); if (num == 12) num -= 10;
                        enter = 3;
                    }
                    if (surface % 4 == 0) { num = 12; enter = surface / 4; }
                    if (num < 10) enterField = "p0" + num.ToString();
                    else enterField = "p" + num.ToString(); break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s1.1"; break;
                            case 17: enterField = "1s1.1"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s1.1"; break;
                        };
                        enter = 6;
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default: if (upField) num = 3; else num = -3; enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += size.ToString() + "." + size.ToString();
                        enter = 2;
                    }
                    break;
                case 3: enterField = surface.ToString() + "s2.2"; enter = 6; break;
                case 4: enterField = surface.ToString() + "s2.1"; enter = 1; break;
                case 5:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s2.2"; break;
                            case 20: enterField = "4s2.2"; break;
                            default:
                                if (upField) enterField = (surface - 4).ToString() + "s2.2";
                                else enterField = (surface + 4).ToString() + "s2.2"; break;
                        }; enter = 1;
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - 1).ToString() + ".1";
                        enter = 5;
                    }
                    break;
                case 6:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s1.1"; break;
                            case 20: enterField = "4s1.1"; break;
                            default:
                                if (upField) enterField = (surface - 4).ToString() + "s1.1";
                                else enterField = (surface + 4).ToString() + "s1.1"; break;
                        };
                        enter = 2;
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + size.ToString() + ".1";
                        enter = 6;
                    }
                    break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size && fieldTwo == 1)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (size - 1).ToString() + ".1"; enter = 4; break;
                case 2: enterField = surface.ToString() + "s" + size.ToString() + ".2"; enter = 5; break;
                case 3:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + "." + (size - 1).ToString(); enter = 3; break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + "." + size.ToString(); enter = 4; break;
                case 5:
                    num = (((surface - 1) % 4) + 1) + ((((surface - 1) % 4) + 1) % 3) + (((surface - 1) / 4) * 2); if (num > 11) num -= 10;
                    if (num < 10) enterField = "p0" + num; else enterField = "p" + num;

                    if (num % 2 == 0)
                    {
                        if (upField) enter = 1; else enter = 4;
                    }
                    else
                    {
                        if (upField) enter = 4; else enter = 1;
                    }
                    break;
                case 6:
                    switch (surface)
                    {
                        case 1: enterField = "17s" + size.ToString() + "." + size.ToString(); enter = 2; break;
                        case 20: enterField = "04s" + size.ToString() + "." + size.ToString(); enter = 2; break;
                        default:
                            if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                            {
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s" + size.ToString() + "." + size.ToString();
                                enter = 2;
                            }
                            else
                            {
                                if (upField) num = -1; else num = 1;
                                enterField = (surface + num).ToString() + "s1.1";
                                enter = 6;
                            }
                            break;
                    }; break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size && fieldTwo == size)
        {
            switch (direction)
            {
                case 1:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size - 1).ToString() + ".1";
                        enter = 5;
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s2.2"; break;
                            case 19: enterField = "2s2.2"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s2.2"; break;
                        }
                        enter = 1;
                    }
                    break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += size.ToString() + ".1";
                        enter = 6;
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s1.1"; break;
                            case 19: enterField = "2s1.1"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s1.1"; break;
                        }
                        enter = 2;
                    }
                    break;
                case 3:
                    num = (((surface - 1) / 2) % 2) + 2 + ((surface % 2) * 2) + (((surface - 1) / 4) * 2); if (num > 11) num -= 10;
                    if (num < 10) enterField = "p0" + num; else enterField = "p" + num;

                    if (num % 2 == 0)
                    {
                        if (surface % 2 != 0) { if (upField) enter = 5; else enter = 2; }
                        else enter = 2;
                    }
                    else
                    {
                        if (surface % 2 != 0) enter = 2;
                        else { if (upField) enter = 2; else enter = 5; };
                    }
                    break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + ".1"; enter = 4; break;
                case 5: enterField = surface.ToString() + "s" + size.ToString() + "." + (size - 1).ToString(); enter = 2; break;
                case 6: enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + (size - 1).ToString(); enter = 3; break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldTwo == 1)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (fieldOne - 1).ToString() + ".1"; enter = 4; break;
                case 2: enterField = surface.ToString() + "s" + fieldOne.ToString() + "." + (fieldTwo + 1).ToString(); enter = 5; break;
                case 3: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); enter = 6; break;
                case 4: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + ".1"; enter = 1; break;
                case 5:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s"; break;
                            case 20: enterField = "4s"; break;
                            default:
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (fieldOne + 1).ToString() + "." + (fieldOne + 1).ToString();
                        enter = 1;
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - fieldOne).ToString() + ".1";
                        enter = 5;
                    }
                    break;
                case 6:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s"; break;
                            case 20: enterField = "4s"; break;
                            default:
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += fieldOne.ToString() + "." + fieldOne.ToString();
                        enter = 2;
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - fieldOne + 1).ToString() + ".1";
                        enter = 6;
                    }
                    break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + fieldTwo.ToString(); enter = 4; break;
                case 2: enterField = surface.ToString() + "s" + size.ToString() + "." + (fieldTwo + 1).ToString(); enter = 5; break;
                case 3:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + fieldOne.ToString() + "." + (size - fieldTwo).ToString();
                    enter = 3; break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + fieldOne.ToString() + "." + (size - fieldTwo + 1).ToString();
                    enter = 4; break;
                case 5: enterField = surface.ToString() + "s" + size.ToString() + "." + (fieldTwo - 1).ToString(); enter = 2; break;
                case 6:
                    enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + (fieldTwo - 1).ToString(); enter = 3;
                    break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == fieldTwo)
        {
            switch (direction)
            {
                case 1:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (fieldOne - 1).ToString() + ".1"; enter = 5;
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size + 2 - fieldOne).ToString() + "." + (size + 2 - fieldTwo).ToString();
                        enter = 1;
                    }
                    break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += fieldOne.ToString() + ".1"; enter = 6;
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size + 1 - fieldOne).ToString() + "." + (size + 1 - fieldOne).ToString();
                        enter = 2;
                    }
                    break;
                case 3: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); enter = 6; break;
                case 4: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + fieldTwo.ToString(); enter = 1; break;
                case 5: enterField = surface.ToString() + "s" + fieldOne.ToString() + "." + (fieldTwo - 1).ToString(); enter = 2; break;
                case 6: enterField = surface.ToString() + "s" + (fieldOne - 1).ToString() + "." + (fieldTwo - 1).ToString(); enter = 3; break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        enterField = surface.ToString() + "s";
        switch (direction)
        {
            case 1: enterField += (fieldOne - 1).ToString() + "." + fieldTwo.ToString(); enter = 4; break;
            case 2: enterField += fieldOne.ToString() + "." + (fieldTwo + 1).ToString(); enter = 5; break;
            case 3: enterField += (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); enter = 6; break;
            case 4: enterField += (fieldOne + 1).ToString() + "." + fieldTwo.ToString(); enter = 1; break;
            case 5: enterField += fieldOne.ToString() + "." + (fieldTwo - 1).ToString(); enter = 2; break;
            case 6: enterField += (fieldOne - 1).ToString() + "." + (fieldTwo - 1).ToString(); enter = 3; break;
        }
        if (enterField[1] == 's') enterField = "0" + enterField;
        return enterField;
    }

    String GetPosition(String curentField, int direction)
    {
        String enterField = "";
        String numOne = "", numTwo = "";
        bool nextNum = false;
        int num = 0;

        if (curentField[0] == 'p')
        {
            surface = int.Parse(("" + curentField[1] + curentField[2]).ToString());

            switch (surface)
            {
                case 1:
                    num = ((direction * 4) - 21) * -1; if (num < 10) enterField = "0" + num.ToString() + "s1.1"; else enterField = num.ToString() + "s1.1"; break;
                case 12:
                    num = direction * 4; if (num < 10) enterField = "0" + num.ToString() + "s1.1"; else enterField = num.ToString() + "s1.1"; break;
                default:
                    if (surface % 2 == 0)
                    {
                        num = direction - 5 * ((direction - 1) / 5);
                        if (num > 2) num = (num - 2) * -1;
                        num += ((surface / 2) - 1) * 4;
                        if (num < 0) num += 20;
                    }
                    else
                    {
                        num = (direction - 5 * ((direction - 1) / 5)) - 1;
                        if (num == 1) num = -1;
                        num += ((surface / 2) - 1) * 4;
                        if (num < 1) num += 20;
                    }
                    if (num < 10) enterField = "0" + num.ToString() + "s"; else enterField = num.ToString() + "s";

                    switch (direction)
                    {
                        case 1: enterField += size.ToString() + ".1"; break;
                        case 2: enterField += size.ToString() + "." + size.ToString(); break;
                        case 3: enterField += "1.1"; break;
                        case 4: enterField += size.ToString() + ".1"; break;
                        case 5: enterField += size.ToString() + "." + size.ToString(); break;
                    }
                    break;
            }
            return enterField;
        }



        surface = int.Parse(("" + curentField[0] + curentField[1]).ToString());
        for (int i = 3; i < curentField.Length; i++)
        {
            if (curentField[i] == '.') { nextNum = true; continue; }
            if (!nextNum) numOne += curentField[i];
            if (nextNum) numTwo += curentField[i];
        }
        fieldOne = int.Parse(numOne.ToString());
        fieldTwo = int.Parse(numTwo.ToString());

        bool upField = false;
        if (surface % 2 != 0) upField = true;

        if (fieldOne == 1 && fieldTwo == 1)
        {
            switch (direction)
            {
                case 1:
                    if ((surface + 3) % 4 == 0) { num = 1; }
                    if (((surface / 2) + 1) % 2 == 0)
                    {
                        num = (surface / 2) + 2 + (surface % 2); if (num == 12) num -= 10;
                    }
                    if (surface % 4 == 0) num = 12;
                    if (num < 10) enterField = "p0" + num.ToString();
                    else enterField = "p" + num.ToString(); break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s1.1"; break;
                            case 17: enterField = "1s1.1"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s1.1"; break;
                        };
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default: if (upField) num = 3; else num = -3; enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += size.ToString() + "." + size.ToString();
                    }
                    break;
                case 3: enterField = surface.ToString() + "s2.2"; break;
                case 4: enterField = surface.ToString() + "s2.1"; break;
                case 5:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s2.2"; break;
                            case 20: enterField = "4s2.2"; break;
                            default:
                                if (upField) enterField = (surface - 4).ToString() + "s2.2";
                                else enterField = (surface + 4).ToString() + "s2.2"; break;
                        };
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - 1).ToString() + ".1";
                    }
                    break;
                case 6:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s1.1"; break;
                            case 20: enterField = "4s1.1"; break;
                            default:
                                if (upField) enterField = (surface - 4).ToString() + "s1.1";
                                else enterField = (surface + 4).ToString() + "s1.1"; break;
                        };
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + size.ToString() + ".1";
                    }
                    break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size && fieldTwo == 1)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (size - 1).ToString() + ".1"; break;
                case 2: enterField = surface.ToString() + "s" + size.ToString() + ".2"; break;
                case 3:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + "." + (size - 1).ToString(); break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + "." + size.ToString(); break;
                case 5:
                    num = (((surface - 1) % 4) + 1) + ((((surface - 1) % 4) + 1) % 3) + (((surface - 1) / 4) * 2); if (num > 11) num -= 10;
                    if (num < 10) enterField = "p0" + num; else enterField = "p" + num;
                    break;
                case 6:
                    switch (surface)
                    {
                        case 1: enterField = "17s" + size.ToString() + "." + size.ToString(); break;
                        case 20: enterField = "04s" + size.ToString() + "." + size.ToString(); break;
                        default:
                            if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                            {
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s" + size.ToString() + "." + size.ToString();
                            }
                            else
                            {
                                if (upField) num = -1; else num = 1;
                                enterField = (surface + num).ToString() + "s1.1";
                            }
                            break;
                    }; break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size && fieldTwo == size)
        {
            switch (direction)
            {
                case 1:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size - 1).ToString() + ".1";
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s2.2"; break;
                            case 19: enterField = "2s2.2"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s2.2"; break;
                        }
                    }
                    break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += size.ToString() + ".1";
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s1.1"; break;
                            case 19: enterField = "2s1.1"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s1.1"; break;
                        }
                    }
                    break;
                case 3:
                    num = (((surface - 1) / 2) % 2) + 2 + ((surface % 2) * 2) + (((surface - 1) / 4) * 2); if (num > 11) num -= 10;
                    if (num < 10) enterField = "p0" + num; else enterField = "p" + num;
                    break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + size.ToString() + ".1"; break;
                case 5: enterField = surface.ToString() + "s" + size.ToString() + "." + (size - 1).ToString(); break;
                case 6: enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + (size - 1).ToString(); break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldTwo == 1)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (fieldOne - 1).ToString() + ".1"; break;
                case 2: enterField = surface.ToString() + "s" + fieldOne.ToString() + "." + (fieldTwo + 1).ToString(); break;
                case 3: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); break;
                case 4: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + ".1"; break;
                case 5:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s"; break;
                            case 20: enterField = "4s"; break;
                            default:
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (fieldOne + 1).ToString() + "." + (fieldOne + 1).ToString();
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - fieldOne).ToString() + ".1";
                    }
                    break;
                case 6:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 1: enterField = "17s"; break;
                            case 20: enterField = "4s"; break;
                            default:
                                if (upField) num = -4; else num = 4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += fieldOne.ToString() + "." + fieldOne.ToString();
                    }
                    else
                    {
                        if (upField) num = -1; else num = 1;
                        enterField = (surface + num).ToString() + "s" + (size - fieldOne + 1).ToString() + ".1";
                    }
                    break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == size)
        {
            switch (direction)
            {
                case 1: enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + fieldTwo.ToString(); break;
                case 2: enterField = surface.ToString() + "s" + size.ToString() + "." + (fieldTwo + 1).ToString(); break;
                case 3:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + fieldOne.ToString() + "." + (size - fieldTwo).ToString(); break;
                case 4:
                    if (upField) num = 1; else num = -1;
                    enterField = (surface + num).ToString() + "s" + fieldOne.ToString() + "." + (size - fieldTwo + 1).ToString(); break;
                case 5: enterField = surface.ToString() + "s" + size.ToString() + "." + (fieldTwo - 1).ToString(); break;
                case 6:
                    enterField = surface.ToString() + "s" + (size - 1).ToString() + "." + (fieldTwo - 1).ToString(); break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        if (fieldOne == fieldTwo)
        {
            switch (direction)
            {
                case 1:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (fieldOne - 1).ToString() + ".1";
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size + 2 - fieldOne).ToString() + "." + (size + 2 - fieldTwo).ToString();
                    }
                    break;
                case 2:
                    if (surface % 4 == 0 || (surface + 3) % 4 == 0)
                    {
                        switch (surface)
                        {
                            case 4: enterField = "20s"; break;
                            case 17: enterField = "1s"; break;
                            default:
                                if (upField) num = 4; else num = -4;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += fieldOne.ToString() + ".1";
                    }
                    else
                    {
                        switch (surface)
                        {
                            case 2: enterField = "19s"; break;
                            case 19: enterField = "2s"; break;
                            default:
                                if (upField) num = 3; else num = -3;
                                enterField = (surface + num).ToString() + "s"; break;
                        }
                        enterField += (size + 1 - fieldOne).ToString() + "." + (size + 1 - fieldOne).ToString();
                    }
                    break;
                case 3: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); break;
                case 4: enterField = surface.ToString() + "s" + (fieldOne + 1).ToString() + "." + fieldTwo.ToString(); break;
                case 5: enterField = surface.ToString() + "s" + fieldOne.ToString() + "." + (fieldTwo - 1).ToString(); break;
                case 6: enterField = surface.ToString() + "s" + (fieldOne - 1).ToString() + "." + (fieldTwo - 1).ToString(); break;
            }
            if (enterField[1] == 's') enterField = "0" + enterField;
            return enterField;
        }

        enterField = surface.ToString() + "s";
        switch (direction)
        {
            case 1: enterField += (fieldOne - 1).ToString() + "." + fieldTwo.ToString(); break;
            case 2: enterField += fieldOne.ToString() + "." + (fieldTwo + 1).ToString(); break;
            case 3: enterField += (fieldOne + 1).ToString() + "." + (fieldTwo + 1).ToString(); break;
            case 4: enterField += (fieldOne + 1).ToString() + "." + fieldTwo.ToString(); break;
            case 5: enterField += fieldOne.ToString() + "." + (fieldTwo - 1).ToString(); break;
            case 6: enterField += (fieldOne - 1).ToString() + "." + (fieldTwo - 1).ToString(); break;
        }
        if (enterField[1] == 's') enterField = "0" + enterField;
        return enterField;
    }
}
