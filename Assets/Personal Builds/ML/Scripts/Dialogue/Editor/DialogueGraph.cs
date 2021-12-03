using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView graphView;

    private string fileName = "New Narrative";

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateBlackBoard();
    }

    
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void GenerateBlackBoard()
    {
        var blackBoard = new Blackboard(graphView);
        blackBoard.Add(new BlackboardSection{title = "Exposed Properties"});
        blackBoard.addItemRequested = blackBoard1 =>
        {
            graphView.AddPropertyToBlackBoard(new ExposedProperty());
        };
        blackBoard.SetPosition(new Rect(10,30, 200, 300));
        graphView.Add(blackBoard);
        graphView.blackBoard = blackBoard;
    }
    
    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Window");
    }


    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph"
        };
        
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }


    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("FileName");
        fileNameTextField.SetValueWithoutNotify(fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
        toolbar.Add(fileNameTextField);
        
        toolbar.Add(new Button(clickEvent: ()=> RequestDataOperation(true)){text ="Save Data"});
        toolbar.Add(new Button(clickEvent: ()=> RequestDataOperation(false)){text ="Load Data"});
        
        rootVisualElement.Add(toolbar);
    }

    public void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name", "please enter valid file name", "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(graphView);

        if (save)
        {
            saveUtility.SaveGraph(fileName);
        }
        else
        {
            saveUtility.LoadGraph(fileName);
        }
    }


    private void GenerateMiniMap()
    {
        //  var miniMap = new MiniMap();
    }
}
