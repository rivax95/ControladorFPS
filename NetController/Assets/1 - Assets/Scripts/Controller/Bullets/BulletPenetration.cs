using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPenetration : MonoBehaviour
{
    public float velocityMIN = 0;
    public float maxObjetos;

    public void Raycasting(float velocity, Vector3 orig,Vector3 direc, LayerMask mascara)
    {
        Vector3[] posiciones = new Vector3[20];
        maxObjetos *= 2;
        Vector3 origen = orig;
        Vector3 direccion =direc;
        bool collision = false;
        RaycastHit hit;
        int contador = 0;
        int contador2 = 0;
        float Matdensity;

        RaycastHit[] results;

        results = Physics.RaycastAll(origen, direccion, Mathf.Infinity, mascara);

        foreach (RaycastHit impacto in results)
        {
            Debug.Log(string.Format("Has impactado con {0} en el punto {1}", impacto.collider.name, impacto.point));

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = impacto.point;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cube.GetComponent<Collider>().enabled = false;

            cube.name = "CubeFront";
        }

        List<RaycastHit> impactos_delanteros = results.OfType<RaycastHit>().ToList();
        impactos_delanteros = impactos_delanteros.OrderBy(o => Vector3.Distance(o.point, origen)).ToList<RaycastHit>();

        List<RaycastHit> impactos_traseros = new List<RaycastHit>();

        foreach (RaycastHit impacto in impactos_delanteros)
        {
            Debug.Log(Vector3.Distance(impacto.point, origen));
        }
        for (int i = impactos_delanteros.Count -1; i >= 0; i--)
        {
            Vector3 inverseDirection;
            if (i > 0)
            {
                inverseDirection = (impactos_delanteros[i-1].point - impactos_delanteros[i].point).normalized;
                Debug.DrawLine(impactos_delanteros[i].point, impactos_delanteros[i - 1].point, Color.blue, Mathf.Infinity);
            }
            else
            {
                inverseDirection = (origen - impactos_delanteros[0].point).normalized;
            }
          

            if (Physics.Raycast(impactos_delanteros[i].point, inverseDirection, out hit, Mathf.Infinity, mascara))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = hit.point;
                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                cube.GetComponent<Collider>().enabled = false;
                cube.name = "CubeBack";

                //LineRenderer lr = cube.AddComponent<LineRenderer>();
                //Vector3[] points = { cube.transform.position, results[i].point };

                //lr.SetPositions(points);
                //lr.startColor = Color.red;

                impactos_traseros.Add(hit);
            }

        }


        //while (velocity > velocityMIN)
        //{
        //    Debug.Log("entro al metodo");
        //    if (Physics.Raycast(origen, direccion, out hit, mascara))
        //    {

        //        collision = false ? true : false;
        //        if (collision)
        //        {
        //            if (hit.transform.GetComponent<Penetration>().TipoMaterial == Penetration.Material.Suelo)
        //            {

        //                //fuera 
        //            }
        //            //coje el componente del material 
        //            Matdensity = hit.transform.GetComponent<Penetration>().value;



        //        }
        //        else
        //        {
        //            //el material es aire 
        //            Matdensity = 1.225f;
        //        }
               
        //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //        cube.transform.position = hit.point;
        //        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        //        //Vector3 offsetDirection = -1 * hit.normal;
        //        ////offset a long way, minimum thickness of the object
        //        //origen= hit.point + offsetDirection * 100;

        //        origen = hit.point;
              
        //       Vector3 Offset= -1 *hit.normal;
        //        origen = hit.point + Offset;

                

        //        Debug.Log(hit.collider.name);

        //        posiciones[contador] = hit.point;
        //    }
          
        //    if (contador >= maxObjetos)
        //    {
        //        break;
        //    }
        //    contador++;
        //}
        //while (contador2 == posiciones.Length)
        //{
        //    Debug.Log("segundo while");
        //    if (Physics.Raycast(posiciones[posiciones.Length-contador2], posiciones[posiciones.Length -contador2+1], out hit, mascara))
        //    {

        //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //        cube.transform.position = hit.point;
        //        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //    }
        //    contador2++;
        //}
    }
}