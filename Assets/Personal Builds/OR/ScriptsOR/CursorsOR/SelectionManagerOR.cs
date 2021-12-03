using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerOR : MonoBehaviour
{
    [SerializeField] private string selectableTagOR = "Selectable";
    [SerializeField] private Material highlightMaterialOR;
    [SerializeField] private Material defaultMaterialOR;

    private Transform _selection;

    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterialOR;
            _selection = null;
        }
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTagOR))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterialOR;
                }

                _selection = selection;
            }
        }
    }
}
