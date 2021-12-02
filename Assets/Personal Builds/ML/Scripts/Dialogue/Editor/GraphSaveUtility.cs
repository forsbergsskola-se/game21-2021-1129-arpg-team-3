using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class GraphSaveUtility 
{
   private DialogueGraphView _targetGraphView;
   private DialogueContainer containerChache;
   
   private List<Edge> edges => _targetGraphView.edges.ToList();
   private List<DialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();


   public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
   {
      return new GraphSaveUtility
      {
         _targetGraphView = targetGraphView
      };
   }

   public void SaveGraph(string fileName)
   {
      if (!edges.Any())
      {
         return;
      }

      var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

      var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

      for (int i = 0; i < connectedPorts.Length; i++)
      {
         var outputNode = connectedPorts[i].output.node as DialogueNode;
         var inputNode = connectedPorts[i].input.node as DialogueNode;
         
         dialogueContainer.NodeLinks.Add(new NodeLinkData
         {
            BaseNodeGUID = outputNode.GUID,
            PortName = connectedPorts[i].output.portName,
            TargetNodeGUID = inputNode.GUID
         });
      }

      foreach (var dialogueNode in nodes.Where(node=>!node.EntryPoint))
      {
         dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
         {
            NodeGUID = dialogueNode.GUID,
            DialogueText = dialogueNode.DialogueText,
            position = dialogueNode.GetPosition().position
         });
      }

      if (!AssetDatabase.IsValidFolder("Assets/Resources"))
      {
         AssetDatabase.CreateFolder("Assets", "Resources");
      }
      
      AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
      AssetDatabase.SaveAssets();
   }
   
   public void LoadGraph(string fileName)
   {
      containerChache = Resources.Load<DialogueContainer>(fileName);

      if (containerChache == null)
      {
         EditorUtility.DisplayDialog("File not found", "Target dialogue graph file does not exist!", "OK");
         return;
      }

      ClearGraph();
      CreateNodes();
      ConnectNodes();
   }

   private void ConnectNodes()
   {
      for (int i = 0; i < nodes.Count; i++)
      {
         var connections = containerChache.NodeLinks.Where(x => x.BaseNodeGUID == nodes[i].GUID).ToList();
         for (int j = 0; j < connections.Count; j++)
         {
            var targetNodeGuid = connections[j].TargetNodeGUID;
            var targetNode = nodes.First(x => x.GUID == targetNodeGuid);
            LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);
            
            targetNode.SetPosition(new Rect(containerChache.DialogueNodeData.First(x => x.NodeGUID == targetNodeGuid).position, _targetGraphView.nodeScale));
         }
      }
   }

   private void LinkNodes(Port output, Port input)
   {
      var tempEdge = new Edge
      {
         output = output,
         input = input
      };
      
      tempEdge.input.Connect(tempEdge);
      tempEdge.output.Connect(tempEdge);
      _targetGraphView.Add(tempEdge);
   }
   
   private void CreateNodes()
   {
      foreach (var nodeData in containerChache.DialogueNodeData)
      {
         var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText);
         tempNode.GUID = nodeData.NodeGUID;
         _targetGraphView.AddElement(tempNode);

         var nodePorts = containerChache.NodeLinks.Where(x => x.BaseNodeGUID == nodeData.NodeGUID).ToList();
         nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
      }
   }
   
   private void ClearGraph()
   {
      //set entry points guid back from the save. Discard existing guid.
      nodes.Find(x => x.EntryPoint).GUID = containerChache.NodeLinks[0].BaseNodeGUID;

      foreach (var node in nodes)
      {
         if (node.EntryPoint) continue;
         
         //remove the edges that connected this node
         edges.Where(x =>x.input.node == node).ToList().ForEach(edge=> _targetGraphView.RemoveElement(edge));
         
         //remove the node from the graph
         _targetGraphView.RemoveElement(node);
      }
   }
}
