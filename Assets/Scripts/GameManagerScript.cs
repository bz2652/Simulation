using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //this is secretly private
    InputAction leftMouse;

    public GameObject foodObj;

    //this is also secretly private
    public List<GameObject> allFood = new List<GameObject>();
    //list is a collection of your gameobjects
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftMouse = InputSystem.actions.FindAction("MouseClick");
    }

    // Update is called once per frame
    void Update()
    {
        if (leftMouse.WasReleasedThisFrame())
        {
            //create a food
            MakeFood();
        }
    }
    
    void MakeFood()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.z = 0;
        // object first, position of the game object, no rotation.. just the rotation it exists as (quaternion = rotation)
        allFood.Add(Instantiate(foodObj, newPos, Quaternion.identity));
    }
}