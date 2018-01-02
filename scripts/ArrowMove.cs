using System;
using UnityEngine;
using UnityEngine.UI;


public class ArrowMove : MonoBehaviour
{
    Map map;

    public Text fieldOneTXT, fieldTwoTXT, neighbourLeftTXT, neighbourRightTXT, directionTXT, enterTXT;

    String curentField;
    int direction;
    LineRenderer line;
    public GameObject one, two;
    public Material change;

    Color memOneColor;
    Vector3 memOneScale;
    String wayStart, wayEnd;
    bool nextTarget;

    void Start()
    {
        map = gameObject.AddComponent<Map>();

        curentField = "p02"; direction = 1;

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.1f;
        line.positionCount = 2;
        line.startColor = Color.green;
        line.endColor = Color.yellow;
        line.startWidth = .6f;
        line.endWidth = .1f;

        one = GameObject.Find(curentField);
        two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));

        line.SetPosition(0, one.transform.position);
        line.SetPosition(1, two.transform.position);
    }

    public void LeftButton()
    {
        direction--;
        if (curentField[0] == 'p' && direction == 0) direction = 5;
        if (direction == 0) direction = 6;
        two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
     //   line.SetPosition(1, two.transform.position);
    }

    public void RightButton()
    {
        direction++;
        if (curentField[0] == 'p' && direction == 6) direction = 1;
        if (direction == 7) direction = 1;
        two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
     //   line.SetPosition(1, two.transform.position);
    }

    public void FowardButton()
    {
        direction = map.enter;

        if (two.name[0] == 'p')
        {
            if (direction == 6) direction = 5;
            direction += 2;
            if (direction > 5) direction -= 5;
        }
        else
        {
            direction += 3;
            if (direction > 6) direction -= 6;
        }

        if (one.GetComponent<Renderer>().material.color != Color.magenta)
            one.GetComponent<Renderer>().material = change;

        if (one.transform.localScale.x < .3f) // && one.name[0] != 'p')
            one.transform.localScale = new Vector3(one.transform.localScale.x * 1.4f, one.transform.localScale.y * .7f, one.transform.localScale.z * 1.4f);

        one = two;
        curentField = one.name;
        two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
      //  line.SetPosition(0, one.transform.position);
      //  line.SetPosition(1, two.transform.position);
        one.GetComponent<Renderer>().material = change;
    }

    public void WayBotton()
    {
        if (one.GetComponent<Renderer>().material.color != Color.magenta)
        {
            memOneColor = one.GetComponent<Renderer>().material.color;
            one.GetComponent<Renderer>().material.color = Color.magenta;

            memOneScale = one.transform.localScale;
            one.transform.localScale = new Vector3(.3f, 2f, .3f);

            if (nextTarget) { GetComponent<DrawWay>().DrawTheWay(wayStart, one.name); nextTarget = false; }
            else { wayStart = one.name; nextTarget = true; }
        }
        else
        {
            one.GetComponent<Renderer>().material.color = memOneColor;
            one.transform.localScale = memOneScale;
            nextTarget = false;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction--;
            if (curentField[0] == 'p' && direction == 0) direction = 5;
            if (direction == 0) direction = 6;
            two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
         //   line.SetPosition(1, two.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction++;
            if (curentField[0] == 'p' && direction == 6) direction = 1;
            if (direction == 7) direction = 1;
            two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
         //   line.SetPosition(1, two.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = map.enter;

            if (two.name[0] == 'p')
            {
                if (direction == 6) direction = 5;
                direction += 2;
                if (direction > 5) direction -= 5;
            }
            else
            {
                direction += 3;
                if (direction > 6) direction -= 6;
            }

            if (one.GetComponent<Renderer>().material.color != Color.magenta)
            one.GetComponent<Renderer>().material = change;

            if (one.transform.localScale.x < .3f) // && one.name[0] != 'p')
                one.transform.localScale = new Vector3(one.transform.localScale.x * 1.4f, one.transform.localScale.y * .7f, one.transform.localScale.z * 1.4f);

            one = two;
            curentField = one.name;
            two = GameObject.Find(map.GetEnterFieldAndFindNearestNeighbours(curentField, direction));
          //  line.SetPosition(0, one.transform.position);
          //  line.SetPosition(1, two.transform.position);
            one.GetComponent<Renderer>().material = change;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (one.GetComponent<Renderer>().material.color != Color.magenta)
            {
                memOneColor = one.GetComponent<Renderer>().material.color;
                one.GetComponent<Renderer>().material.color = Color.magenta;

                memOneScale = one.transform.localScale;
                one.transform.localScale = new Vector3(.3f, 2f, .3f);

                if (nextTarget) { GetComponent<DrawWay>().DrawTheWay(wayStart, one.name); nextTarget = false; }
                else { wayStart = one.name; nextTarget = true; }
            }
            else
            {
                one.GetComponent<Renderer>().material.color = memOneColor;
                one.transform.localScale = memOneScale;
                nextTarget = false;
            }
        }



        line.SetPosition(0, Vector3.Lerp(line.GetPosition(0), one.transform.position, .2f));
        line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), two.transform.position, .2f));


        directionTXT.text = "direction: " + direction.ToString();
        enterTXT.text = "enter: " + map.enter.ToString();
        neighbourLeftTXT.text = map.neighbourOne.ToString();
        neighbourRightTXT.text = map.neighbourTwo.ToString();
        fieldOneTXT.text = one.name.ToString();
        fieldTwoTXT.text = two.name.ToString();
    }
}
