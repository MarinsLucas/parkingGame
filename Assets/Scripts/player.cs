using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]float maxVelocity;
    [SerializeField]float acceleration;
    [SerializeField]float deceleration;

    [Header("Variáveis das rodas")]
    public float rotation;
    public float rotationSpeed;
    GameObject FR, FL;

    float carRotation;
    float wheelRotation;

    // Start is called before the first frame update
    void Start()
    {
        Manager.setPlayer(this.gameObject);

        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).gameObject.name == "FL")
                FL = transform.GetChild(i).gameObject;
            if(transform.GetChild(i).gameObject.name == "FR")
                FR = transform.GetChild(i).gameObject;
        }
        if(FL == null || FR == null) Debug.Log("ERRO! Pneus não encontrados"); //depuração para testar diferentes tipos de veiculos
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        float norma = Mathf.Sqrt(Mathf.Pow(this.gameObject.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(this.gameObject.GetComponent<Rigidbody>().velocity.z, 2));

        if(x!=0)
        {   
            wheelRotation = Mathf.Lerp(wheelRotation, x > 0 ? rotation : -rotation, Time.deltaTime*rotationSpeed);
        }
        else if(y != 0)
        {
            wheelRotation = Mathf.Lerp(wheelRotation, 0, Time.deltaTime*rotationSpeed*2f);
        }

        carRotation = this.gameObject.transform.eulerAngles.y;

        FL.transform.eulerAngles = new Vector3(0,wheelRotation+90f + carRotation,0);
        FR.transform.eulerAngles = new Vector3(0,wheelRotation+90f + carRotation,0);

        if(y>0)
        {
            float cos = Mathf.Cos((wheelRotation+90f + carRotation)*Mathf.Deg2Rad);
            y = y*acceleration*Time.deltaTime;
            x = y*acceleration*Time.deltaTime*Mathf.Sin((wheelRotation+90f)*Mathf.Deg2Rad);
        }
        else if(y<0)
        {
            float cos = Mathf.Sin((wheelRotation+90f + carRotation)*Mathf.Deg2Rad);
            y = y*deceleration*Time.deltaTime*cos;
            x = y*deceleration*Time.deltaTime*Mathf.Cos((wheelRotation+90f + carRotation)*Mathf.Deg2Rad);

        }
        else
        {
            float cos = Mathf.Cos((wheelRotation+90f + carRotation)*Mathf.Deg2Rad);
            y = 0;
            x = 0;
        }

        this.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(x,0,y);
        
    }
}
