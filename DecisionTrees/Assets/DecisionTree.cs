using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;



public class DecisionTree
{

    //keep track of the root node
    private TreeNode root;

    private List<TreeNode> allNodes = new List<TreeNode>();

    //keep track of all connections this could be a action to prefrom/sequence/another decision
    private Dictionary<TreeNode, List<TreeNode>> connections = new Dictionary<TreeNode, List<TreeNode>>();

    public struct DTreeData
    {

        public Dictionary<TreeNode, List<TreeNode>> connections;
        public TreeNode root;
        public List<TreeNode> allNodes;
    }

    public DecisionTree(TreeNode r)
    {
        root = r;
        allNodes.Add(root);
    }
    public TreeNode generateAction()
    {
       
        TreeNode nodeToTry = root;

        //this will run the whole tree 
        while(connections.ContainsKey(nodeToTry))
        {
            //index 0 is true
            //index 1 is false

            var list = connections[nodeToTry];

            if (nodeToTry.decision() && list[0] != null)
            {
                nodeToTry = list[0];
            }
            else
            {
                nodeToTry = list[1];
            }

            Debug.Log("Made A decision");
        }
        //should return the last node in the tree
        return nodeToTry;
    }

    //adds a node to the tree
    public void addTreeNode(TreeNode node) 
    {
       allNodes.Add(node);
        
    }
    public void removeConnection(TreeNode from, TreeNode to)
    {
        Assert.IsTrue(connections.ContainsKey(from), "connections does not contain the key you are tring to use");
        Assert.IsTrue(connections[from].Contains(to), "connections does not contain the value you are tring to use");

        var list = connections[from];

        list.Remove(to);

        connections[from] = list;

    }
    public void addConnection(TreeNode from, TreeNode to, bool leftLeaf = true)
    {
        Assert.IsTrue(allNodes.Contains(from) && allNodes.Contains(to), "key or value not in the node collection");
        
        if(!connections.ContainsKey(from))
        {
            //not in the map then add it (adding the from node)
            connections[from] = null;
           
        }

        //get prev connection
        List<TreeNode> newList = connections[from];

        //if the node has no connections then add it
        if(newList == null) 
        {
            Debug.Log("List was Null");
            newList = new List<TreeNode>();

            //pad list for only two choises (think about using arrays?)

            newList.Insert(0, null); 
            newList.Insert(1, null);
        }

        Assert.IsTrue(newList.Count == 2, "something is fucked this should just never happen");

        if(leftLeaf) 
        {
            newList[0] = to;
        }
        else
        {
            newList[1] = to;
        }

        connections[from] = newList;

    }
    public void removeAllConnections(TreeNode node)
    {
        Assert.IsTrue(connections.ContainsKey(node), "connections does not contain the key you are tring to use");

        connections[node].Clear();

    }

    //we dont have to remove from other connections becasue this is a tree not a graph (hopfuly...)
    public void removeNode(TreeNode node)
    {
        allNodes.Remove(node);

        removeAllConnections(node);
    }

   public DTreeData getTreeData()
   {
        DTreeData data = new DTreeData();
        data.allNodes = allNodes;
        data.root = root;
        data.connections = connections;
        return data;
   }
}
