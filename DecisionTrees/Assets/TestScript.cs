using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    ShouldMocve testNode;
    DecisionTree tree;
    // Start is called before the first frame update
    void Start()
    {
        testNode = new ShouldMocve();
        tree = new DecisionTree(testNode);
        ShouldMocve testNode2 = new ShouldMocve();
        ShouldMocve testNode3 = new ShouldMocve();

        tree.addTreeNode(testNode2);
        tree.addTreeNode(testNode3);

        tree.addConnection(testNode, testNode2, true);
        tree.addConnection(testNode, testNode3, false);

        tree.generateAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
