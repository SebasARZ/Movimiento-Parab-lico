using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecibirDatos : MonoBehaviour
{
    public float V0;
    public float Angulo;
    public float d;

    public Text alertas;

    public bool[] comprobarValores;

    private void Start()
    {
        comprobarValores = new bool[2];
    }
    public void Readv0(string v0)
    {
        float vi = float.Parse(v0);
        if (vi >= 0)
        {
            V0 = vi;
            Debug.Log("Prueba " + V0);
            comprobarValores[0] = true;
            alertas.text = "Valor aceptado";
        }
        else
        {
            alertas.text = "Ingresa un valor positivo";
            comprobarValores[0] = false;

        }
    }
    public void LeerAngulo(string a)
    {
        float A = float.Parse(a);
            if(A >=0 && A <= 90)
        {
                Angulo = A;
            comprobarValores[1] = true;
            alertas.text = "Valor aceptado";
        }
        else
        {
            alertas.text = "Ingresa un angulo entre 0 y 90";
            comprobarValores[1] = false;
        }
        
    }
    public void LeerDistancia(string distancia)
    {
        d = float.Parse(distancia);
    }
}
