using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    TrueAction testNode;
    DecisionTree tree;
    // Start is called before the first frame update
    void Start()
    {
        testNode = new TrueAction();
        tree = new DecisionTree(testNode);
        FalseAction false1 = new FalseAction();
        TrueAction true1 = new TrueAction();
        FalseAction false2 = new FalseAction();
        FalseAction false3 = new FalseAction();

        tree.addTreeNode(true1);
        tree.addTreeNode(false1);
        tree.addTreeNode(false2);
        tree.addTreeNode(false3);

        tree.addConnection(testNode, true1, true);
        tree.addConnection(testNode, false3 ,false);
        tree.addConnection(true1, false1, true);
        tree.addConnection(true1, false2, false);

        var test = tree.generateAction();

        Debug.Log(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
