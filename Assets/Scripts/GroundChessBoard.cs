using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChessBoard : MonoBehaviour
{
    public Material blackMaterial;
    public Material whiteMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(bool black)
    {
        GetComponent<MeshRenderer>().material = black ? blackMaterial : whiteMaterial;
    }
}
