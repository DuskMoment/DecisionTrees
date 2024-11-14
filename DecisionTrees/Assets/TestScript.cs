using System.Collections;
using System.Collections.Generic;
using Unity.CodeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    TrueAction testNode;
    DecisionTree tree;
    
    // Start is called before the first frame update
    void Start()
    {
        testNode = new TrueAction(this.gameObject);
        tree = new DecisionTree(testNode);
        FalseAction false1 = new FalseAction(this.gameObject);
        TrueAction true1 = new TrueAction(this.gameObject);
        FalseAction false2 = new FalseAction(this.gameObject);
        FalseAction false3 = new FalseAction(this.gameObject);
        TestSequence sequence = new TestSequence(this.gameObject);

        List<TreeNode> actions = new List<TreeNode>();

        for(int i = 0; i<4; i++)
        {
            actions.Add(new TrueAction(this.gameObject));
        }

        sequence.addChildren(actions);

        tree.addTreeNode(true1);
        tree.addTreeNode(false1);
        tree.addTreeNode(false2);
        tree.addTreeNode(false3);
        tree.addTreeNode(sequence);

        tree.addConnection(testNode, sequence, true);
        tree.addConnection(testNode, false3 ,false);
        tree.addConnection(sequence, true1, true);
        tree.addConnection(true1, false1, true);
        tree.addConnection(true1, false2, false);

        


        var test = tree.generateAction();

        Debug.Log(test);

        //var type = test.GetType();
        //this.AddComponent<TrueAction>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
