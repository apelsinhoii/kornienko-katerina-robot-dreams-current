using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Lection3Controller : MonoBehaviour
{ 
   [SerializeField] private float _value;
   [SerializeField] private List<float> _list;

    [ContextMenu("Print")]
    private void Print()
    {
        string msg = "List: ";
        if ( _list.Count == 0 )
        {
        Debug.Log("The list is empty!");
        }
        else
        {
          for (int i = 0; i < _list.Count; ++i)
           {
            msg += $"\n{_list[i]}";
           }
            Debug.Log(msg);
        }
        
    }

    [ContextMenu("Add Element")]
    private void AddElement()
    {
           _list.Add( _value);
           Print();
    }

    [ContextMenu("Remove Element")]
    private void RemoveElement()
    {
       _list.Remove( _value);
           Print();
    }

    [ContextMenu("Clear List")]
    private void ClearList()
    {
        if (_list.Count == 0)
        {
            Debug.Log("The list is already empty.");
        }
        else
        {
            _list.Clear();
            Print();
        }
    }
      
    [ContextMenu("Sort List")]
    private void SortList()
    {
        if (_list.Count == 0)
        {
            Debug.Log("The list is empty.");
        }
        else
        {
            _list.Sort();
             Print();
        }
    }
}

    


