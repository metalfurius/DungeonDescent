using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    public Vector2 InputVector{get;private set;}
    public Vector3 MousePosition{get;private set;}
    [Header ("Jump")]
    public float playerHeight;
    public bool grounded;
    public bool Jump;
    
    private void Update() {
        if(Time.timeScale!=0){
            GetInputs();
            GetMouse();
            GroundCheck();
        }
    }
    private void GetInputs(){
        var h =Input.GetAxisRaw("Horizontal");
        var v =Input.GetAxisRaw("Vertical");
        Jump=Input.GetKey(KeyCode.Space);
        InputVector=new Vector2(h,v);
    }
    private void GetMouse(){
        MousePosition=Input.mousePosition;
    }
    public void GroundCheck(){
        grounded=Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f);
    }

}
