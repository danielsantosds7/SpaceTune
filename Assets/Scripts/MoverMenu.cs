using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMenu : MonoBehaviour
{   
    public float velocidadeDoObjeto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarObjeto();
    }
    private void MovimentarObjeto()
    {
        transform.Translate(Vector3.left * velocidadeDoObjeto * Time.deltaTime);
    }
}
