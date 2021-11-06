using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.EventSystems;

[Hotfix]
public class GameScript : MonoBehaviour
{
    public int num1 = 100;
    public int num2 = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("点击到UI界面");
            }
            else
            {
                GameObject cubeGo = Resources.Load("Cube") as GameObject;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.point);
                    GameObject cube = GameObject.Instantiate(cubeGo, hit.point + new Vector3(0, 1, 0), transform.rotation) as GameObject;
                }
            }
        }
    }

    public static bool RayFunction(Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit);
    }
}
