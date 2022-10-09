using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Material floor;
    public Material walls;
    public Rigidbody my_rigidB;
    public AudioPlayer audioP;
    public float speedBase = 0.03f;
    public float speedMult = 3f;
    public float maxStamina = 40;
    public float currentStamina = 40;
    public float staminaDrain = 7.5f;
    public float audioCooldown;
    
    private float audioMult = 2f;
    private float audioTimer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse camera turning
        float mouseX = Input.GetAxis ("Mouse X");
        float mouseY = Input.GetAxis ("Mouse Y");
        Vector3 anglechange = new Vector3(-mouseY,mouseX,0);
        this.transform.eulerAngles += anglechange;
        //Get movement direction
        Vector3 velocity = new Vector3();
        if (Input.GetKey(KeyCode.A)){
                velocity += - this.transform.right;
            }
        if (Input.GetKey(KeyCode.W)){
                velocity += this.transform.forward;

            }
        if (Input.GetKey(KeyCode.S)){
                velocity += - this.transform.forward;
            }
        if (Input.GetKey(KeyCode.D)){
                velocity += this.transform.right;

            }
        //Get speed from direction + move button
        //Also calculate new stamina
        float speedCurrent = speedBase;
        if (velocity != new Vector3() && Input.GetKey(KeyCode.LeftShift)){
            speedCurrent *= speedMult;
            currentStamina -= staminaDrain * Time.deltaTime;
            if (currentStamina < 0) currentStamina = 0;      
        }
        else {
            currentStamina += staminaDrain * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        //Actual movement
        velocity.y = 0.0f;
        velocity.Normalize();
        my_rigidB.velocity += velocity * speedCurrent * Time.deltaTime;
         
        //Making colors
        float redness = ((maxStamina - currentStamina) / maxStamina);
        Color FOOL_ORANG_IS_A_COLOR = new Color(0.85f, 0.61f, 0.40f);
        Color darkred = new Color(0.3f,0.05f,0.05f);
        Color color = FOOL_ORANG_IS_A_COLOR * (1-redness) + darkred * redness;

        var cam = GetComponent<Camera>();
        cam.backgroundColor = color;
        
        //Send data to shaders
        walls.SetColor("Color_emptycolor", color); 
        walls.SetFloat("Vector1_shaderMaxDistance", 8f * currentStamina / maxStamina ); 
        floor.SetColor("Color_emptycolor", color); 
        floor.SetFloat("Vector1_shaderMaxDistance", 8f * currentStamina / maxStamina ); 

        //Audio timing
        if (velocity != new Vector3()){
            if (Input.GetKey(KeyCode.LeftShift)){
                //While running
                audioTimer += Time.deltaTime * audioMult;
            }
            //While walking
            else audioTimer += Time.deltaTime;
            if(audioTimer > audioCooldown){
                //Call a footstep sound
                audioP.PlayRandom();
                audioTimer = 0;     
            }
        }
        //Reset when stopped
        else audioTimer = 0;


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }  

    }
}
