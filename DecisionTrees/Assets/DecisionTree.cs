using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;


//this needs to be serializable ie no dictinarys 
[System.Serializable]
public struct DTreeData
{
    //convert this to a list or somthing :3 i want to die but it will be so cool if this saving works
    //public Dictionary<TreeNode, List<TreeNode>> connections;
    public TreeNode root;
    public List<TreeNode> allNodes;

    public List<List<TreeNode>> connectionBranch;
    public List<TreeNode> connectionRoot;
    public Dictionary<TreeNode, List<TreeNode>> connectionDic;

    //conversion is incorrect you need to account for duplicate keys and values (i.e other layers of the tree)
    public void connectionsAsSer(Dictionary<TreeNode, List<TreeNode>> connections)
    {
        if(connectionBranch == null) 
        {
            connectionBranch = new();
        }
        if(connectionRoot == null) 
        {
            connectionRoot = new();
        }

        var keys = connections.Keys;

        //think about adding null connections
        foreach (var key in keys) 
        {
            connectionRoot.Add(key);

            if(connections.ContainsKey(key))
            {
                connectionBranch.Add(connections[key]);
            }
           

        }
        
    }
    //return as a dictionary of Dictionary<TreeNode, List<TreeNode>>

    //rebuild the Dictonary
    public Dictionary<TreeNode, List<TreeNode>> returnDictionary()
    {
        Dictionary<TreeNode,List<TreeNode>> dict = new Dictionary<TreeNode, List<TreeNode>>();

        int counter = 0;
        foreach(var connection in connectionRoot)
        {
            dict[connection] = connectionBranch[0];
            counter++;
        }

        return dict;
    }

}

public class DecisionTree
{ 

    //keep track of the root node
    private TreeNode root;

    private List<TreeNode> allNodes = new List<TreeNode>();

    //keep track of all connections this could be a action to prefrom/sequence/another decision
    private Dictionary<TreeNode, List<TreeNode>> connections = new Dictionary<TreeNode, List<TreeNode>>();

   

    public DecisionTree(TreeNode r)
    {
        root = r;
        allNodes.Add(root);

    }
    public DecisionTree(TreeNode r, List<TreeNode> nodes)
    {
        root = r;
        allNodes.Add(root);
        allNodes = nodes;
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
    public void addConnection(TreeNode from, TreeNode left, TreeNode right)
    {
        Assert.IsTrue(allNodes.Contains(from), "key not in the node collection");

        if (left != null)
        {
            Assert.IsTrue(allNodes.Contains(left), "Left not in the node collection");
        }
        else if (right != null)
        {
            Assert.IsTrue(allNodes.Contains(right), "Right not in the node collection");
        }
            

        if (!connections.ContainsKey(from))
        {
            //not in the map then add it (adding the from node)
            connections[from] = null;

        }

        //get prev connection
        List<TreeNode> newList = connections[from];

        //if the node has no connections then add it
        if (newList == null)
        {
            Debug.Log("List was Null");
            newList = new List<TreeNode>();

            //pad list for only two choises (think about using arrays?)

            newList.Insert(0, null);
            newList.Insert(1, null);
        }

        Assert.IsTrue(newList.Count == 2, "something is fucked this should just never happen");

        newList[0] = left;
        newList[1] = right;

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
        data.connectionsAsSer(connections);
        data.connectionDic = connections;
        return data;
   }

}
