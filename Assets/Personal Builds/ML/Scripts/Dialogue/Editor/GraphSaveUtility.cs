using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class GraphSaveUtility : MonoBehaviour
{

   private List<Edge> edges => _targetGraphView.edges.ToList();
   private List<DialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
   private DialogueGraphView _targetGraphView;
   
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
      
      AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
      AssetDatabase.SaveAssets();
   }
   
   public void LoadGraph(string fileName)
   {
      
   }
}
