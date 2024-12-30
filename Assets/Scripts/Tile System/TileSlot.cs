using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
   private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
   private MeshFilter meshFilter => GetComponent<MeshFilter>();
   private Collider myCollider => GetComponent<Collider>();
   private NavMeshSurface myNavMesh => GetComponentInParent<NavMeshSurface>();

   public Mesh GetMesh() => meshFilter.sharedMesh;
   public Material GetMaterial() => meshRenderer.sharedMaterial;
   public Collider GetCollider() => myCollider;
    public void RotateTile(int dir)
    {
        transform.Rotate(0, 90 * dir, 0);
        UpdateNavMesh();
    }
    public void AdjustY(int verticalDir)
    {
        transform.position += new Vector3(0, 0.1f * verticalDir, 0);
        UpdateNavMesh();
    } 
    public void SwitchTile(GameObject referenceTile)
    {
        gameObject.name = referenceTile.name;

        TileSlot newTile = referenceTile.GetComponent<TileSlot>();

        meshFilter.mesh = newTile.GetMesh();
        meshRenderer.material = newTile.GetMaterial();

        UpdateCollider(newTile.GetCollider());
        UpdateChildren(newTile);
        UpdateLayer(referenceTile);
        UpdateNavMesh();
    }

    public List<GameObject> GetAllChildren()
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    private void UpdateNavMesh() => myNavMesh.BuildNavMesh();
    private void UpdateCollider(Collider newCollider)
    {
        DestroyImmediate(myCollider);

        if(newCollider is BoxCollider)
        {
            BoxCollider original = newCollider.GetComponent<BoxCollider>();
            BoxCollider myNewCollider = transform.AddComponent<BoxCollider>();

            myNewCollider.center = original.center;
            myNewCollider.size = original.size;
        }

        if(newCollider is MeshCollider)
        {
            MeshCollider original = newCollider.GetComponent<MeshCollider>();
            MeshCollider myNewCollider = transform.AddComponent<MeshCollider>();

            myNewCollider.sharedMesh = original.sharedMesh;
            myNewCollider.convex = original.convex;
        }
    }

    private void UpdateChildren(TileSlot newTile)
    {
        foreach (GameObject obj in GetAllChildren())
        {
            DestroyImmediate(obj);
        }

        foreach (GameObject obj in newTile.GetAllChildren())
        {
            Instantiate(obj, transform);
        }
    }
   public void UpdateLayer(GameObject referenceObj) => gameObject.layer = referenceObj.layer;
}
