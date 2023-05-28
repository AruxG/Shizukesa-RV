using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushScript : MonoBehaviour
{
    // Start is called before the first frame update
    //Texture2D paper;
    public bool pinta = false;
    public bool pintando = false;
    public GameObject linePrefab;
    GameObject currentLine;
    public LineRenderer lineRenderer;
    List<Vector3> hitPoints = new List<Vector3>();
    private RaycastHit hit;
    void Start()
    {
       // paper = new Texture2D(520,520);
    }

    public void pintarActivo()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        pinta = true;
    }
    public void despintarActivo()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        pinta = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!pintando && pinta && Physics.Raycast(transform.position, -transform.up, out hit, .5f))
        {
            CreateLine(hit.transform.position);
        }
        else if(pintando)
        {
            if (Vector3.Distance(hit.transform.position, hitPoints[hitPoints.Count - 1]) > .1f)
            {
                UpdateLine(hit.transform.position);
            }
            
        }
        Debug.DrawRay(transform.position, -transform.up*.5f, Color.green);
        

    }
    void CreateLine(Vector3 hit)
    {
        pintando = true;
        currentLine = Instantiate(linePrefab, hit, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        hitPoints.Clear();
        hitPoints.Add(hit);
        hitPoints.Add(hit);
        lineRenderer.SetPosition(0, hitPoints[0]);
        lineRenderer.SetPosition(1, hitPoints[1]);
        
    }
    void UpdateLine(Vector3 newPos)
    {
        hitPoints.Add(newPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newPos);
        Physics.Raycast(transform.position, -transform.up, out hit, .5f);
        Debug.Log(hit.transform.gameObject.name);
        if (!Physics.Raycast(transform.position, -transform.up, out hit, .5f) || hit.transform.gameObject.name!="Paper")
        {
            pintando = false;
        }
    }

    /*MeshCollider meshCollider = hit.collider as MeshCollider;

    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        return;

    Texture2D tex = rend.material.mainTexture as Texture2D;

    Vector2 pixelUV = hit.textureCoord;
    pixelUV.x *= tex.width;
    pixelUV.y *= tex.height;

    tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
    tex.Apply();*/
}

