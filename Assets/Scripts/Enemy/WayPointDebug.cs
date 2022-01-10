using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WayPointDebug : MonoBehaviour
{
    
    [System.Serializable]
    public class StoreItem : System.Object
    {
        public GameObject SpawnPoint;
        public string name;
    }

    public List<GameObject> PointList = new ();
    private int numberWayPoints = 0;
    public int currentNumberPoints { get; private set; }
    [SerializeField] private GameObject PointToSpawn;
    // private bool finalizePoints = false;
    
    
    private string GetName(int index)
    {
        string outName = "";
        switch (index)
        {
            case 0:
                outName = "A";
                break;
            case 1:
                outName = "B";
                break;
            case 2:
                outName = "C";
                break;
            case 3:
                outName = "D";
                break;
            case 4:
                outName = "E";
                break;
            case 5:
                outName = "F";
                break;
            case 6:
                outName = "G";
                break;
            case 7:
                outName = "H";
                break;
        }

        return outName;
    }

    private void OnEnable()
    {
        // finalizePoints = true;
        currentNumberPoints = PointList.Count - 1;
    }


    private GameObject SearchAndReturnPointList(string criteria)
    {
        foreach (var el in PointList)
        {
            if (el.name == criteria)
            {
                return el;
            }
        }

        return null;
    }
    
    private bool SearchPointList(string criteria)
    {
        
        foreach (var el in PointList)
        {
            if (el.name == criteria)
            {
                return true;
            }
        }

        return false;
    }

    public void AddOnePoint()
    {
        string theName = GetName(PointList.Count);
        GameObject point = Instantiate(PointToSpawn);
        point.transform.parent = gameObject.transform;
        point.name = theName;
        point.transform.position = GetComponent<Transform>().position;
        PointList.Add(point);
        RenameWPs();
    }
    public void RemoveOnePoint()
    {
        DestroyImmediate(PointList[PointList.Count -1]);
        PointList.RemoveAt(PointList.Count -1);
        RenameWPs();
    }
    
    
    private void SpawnWayPoints()
    {
        for (int i = 0; i < numberWayPoints; i++)
        {
            if (!SearchPointList(GetName(i)))
            {
                string theName = GetName(i);
                GameObject point = Instantiate(PointToSpawn);
                point.transform.parent = gameObject.transform;
                point.name = theName;
                point.transform.position = GetComponent<Transform>().position;
                PointList.Add(point);
            }
        }
        RenameWPs();
    }

    private void RemovePoints()
    {
        for (int i = currentNumberPoints; i >= numberWayPoints; i--)
        {
            if (PointList.Count >= i - 1)
            {
                DestroyImmediate(PointList[i]);
                PointList.RemoveAt(i);
            }
        }
    }
    
    public Vector3 GetLocationOfPoint(int index)
    {
        GameObject temp = SearchAndReturnPointList(GetName(index));
        
        if (temp != null)
        {
            return temp.transform.position;
        //    return transform.TransformPoint(temp.transform.position);
        }
        return transform.position;
    }
    
    void RenameWPs()
    {
        foreach (var el in PointList)
        {
            if (el.GetComponentInChildren<TextMesh>())
            {
                el.GetComponentInChildren<TextMesh>().text = el.name;
            }
        }
    }

    private void FinishPoints()
    {
        foreach (var el in PointList)
        {
            Destroy(el.GetComponent<MeshRenderer>());
            Destroy(el.GetComponentInChildren<MeshRenderer>());
            Destroy(el.GetComponentInChildren<TextMesh>());
        }
    }
    
    
    void Start()
    {
        if (Application.isPlaying)
        {
            //    currentNumberPoints = PointList.Count;
            //    finalizePoints = true;
            //    FinishPoints();
        
        }
    }

    void Update()
    {
        if (Application.isEditor)
        {
          
        }
    }
}
