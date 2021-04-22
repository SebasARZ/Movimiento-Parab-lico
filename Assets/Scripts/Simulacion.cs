using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Simulacion : MonoBehaviour
{
    float v0;
    float[] v0xy;
    float ϴ;
    readonly float g = 9.8f;
    float t;
    float d;
    float d_;
    RecibirDatos recibirDatos;
    bool[] rDatos;
    float tolerancia1;
    float tolerancia2;
    public Animator panelAnimator;
    public Animator ResetAnimator;

    public Rigidbody2D ballRb;
    public Collider2D ballcollider;
    private void Start()
    {
        recibirDatos = GetComponent<RecibirDatos>();
        panelAnimator.SetBool("PanelBool", false);
        ResetAnimator.SetBool("ResetBool", false);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballRb.simulated = false;
    }
    private void Update()
    {
        rDatos = recibirDatos.comprobarValores;

    }
    public void ResetScene()
    {
        SceneManager.LoadScene("Simulation");
    }
    void BallSimulation()
    {
        ballRb.simulated = true;
        ballRb.velocity = new Vector2(v0xy[0], v0xy[1]).normalized*10;
        ballRb.angularVelocity = ϴ;
    }
    public void ComenzarSimulación()
    {
        if (rDatos[0] == true &&rDatos[1]==true )
        {
            panelAnimator.SetBool("PanelBool", true);
            v0 = recibirDatos.V0;
            ϴ = recibirDatos.Angulo;
            Debug.Log("Angulo Ingresado =" + ϴ);
            d_ = recibirDatos.d;
            
            v0xy = VelocidadInicialxy(v0, ϴ);
            t = Tiempo(v0xy, g);
            d = DistanciaX(v0xy, t);
            tolerancia1 = d + (d * 0.05f);
            tolerancia2 = d - (d * 0.05f);
            Debug.Log("Vx =" + v0xy[0]);
            Debug.Log("Vy =" + v0xy[1]);
            Debug.Log("Tiempo = " + t);
            Debug.Log("Distancia = " + d);
            ComprobarResultado();
            BallSimulation();
            ResetAnimator.SetBool("ResetBool", true);
        }
        else
        {
            recibirDatos.alertas.text = "Uno o mas datos ingresados no cumplen las recomendaciones";
        }
       
    void ComprobarResultado()
        {
            if(d_ >= tolerancia2 && d_ <= tolerancia1  )
            {
                recibirDatos.alertas.text = "La distancia recorrida en x es = " + d + " ¡Muy bien!";
            }else
            {
                recibirDatos.alertas.text = "La distancia recorrida en x es = " + d + " Incorrecto";
            }
        }
    }
    float[] VelocidadInicialxy(float v0, float ϴ)
    {
        float[] v0xytemp = new float[2];
        float ϴrad = (ϴ * Mathf.PI) / 180;
        v0xytemp[0] = v0 * Mathf.Cos(ϴrad);
        v0xytemp[1] = v0 * Mathf.Sin(ϴrad);

        Debug.Log("Grados x" + v0xytemp[0]);
        Debug.Log("Grados y" + v0xytemp[1]);
        v0xy = v0xytemp;
        return v0xy;
    }
    float Tiempo(float[] VelocidadInicialx,float g)
    {
        float t = ((-0 + VelocidadInicialx[1]) / g)*2;
        return t;
    }
    float DistanciaX (float[] VelocidadInicialx, float tiempo)
    {
        float d = VelocidadInicialx[0] * tiempo;
        return d;
    }
}
