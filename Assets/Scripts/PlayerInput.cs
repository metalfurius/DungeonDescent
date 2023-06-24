using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public Vector2 InputVector{get;private set;}
    public Vector3 MousePosition{get;private set;}
    public bool Jump{get;private set;}
    
    private void Update() {
        if(Time.timeScale!=0){
            GetInputs();
            GetMouse();
        }
    }
    private void GetInputs(){
        var h =Input.GetAxis("Horizontal");
        var v =Input.GetAxis("Vertical");
        Jump=Input.GetKey(KeyCode.Space);
        InputVector=new Vector2(h,v);
    }
    private void GetMouse(){
        MousePosition=Input.mousePosition;
    }

}
