using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using TreeEditor;
using static DecisionTree;

//use the asset to save and serialize custom data use yaml?
// use this for https://discussions.unity.com/t/calling-an-editor-window-from-a-monobehavior-script/703197/2 work around for testing
//taken and modified from here https://discussions.unity.com/t/simple-node-editor/508998/2
// usful for yaml decoding https://stackoverflow.com/questions/25650113/how-to-parse-a-yaml-string-in-c-sharp
public class NodeEditor : EditorWindow
{
    //contains a list of all active windows
    List<Rect> windows = new List<Rect>();
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();
    List<int> windowsToDetach = new List<int>();
    List<int> nodesToDelete = new List<int>();

    //window and Id relation
    Dictionary<Rect, int> windowToIdRelation = new Dictionary<Rect, int>();
    List<int> activeIds = new List<int>();
    Dictionary<int,int> attachMap = new Dictionary<int,int>();

    [MenuItem("Window/Node editor")]
    static void ShowEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
       
    }

    void attatchWindows()
    {

        attachedWindows.Add(windowsToAttach[0]);
        attachedWindows.Add(windowsToAttach[1]);

        //used for deleting
        attachMap[windowsToAttach[0]] = windowsToAttach[1];
        attachMap[windowsToAttach[1]] = windowsToAttach[0];
        windowsToAttach = new List<int>();
    }

    void makeCurve()
    {
        for (int i = 0; i < attachedWindows.Count; i += 2)
        {
            DrawNodeCurve(findWindowFromId(attachedWindows[i]), findWindowFromId(attachedWindows[i + 1]));
        }
    }

    void OnGUI()
    {
        if (windowsToAttach.Count == 2)
        {
            attatchWindows();
        }

        if (windowsToDetach.Count == 2)
        {
            attachedWindows.Remove(windowsToDetach[0]);
            attachedWindows.Remove(windowsToDetach[1]);
            windowsToDetach = new List<int>();
        }

        if (attachedWindows.Count >= 2)
        {
            makeCurve();
        }

        //using a window add the connections to a map

        BeginWindows();

        //create nodes
        createNode();

       
        for (int i = 0; i < windows.Count; i++)
        {
            Debug.Log("windows Count " + windows.Count );
            //save prev key
            var saveKey = windows[i];

            if(!windowToIdRelation.ContainsKey(saveKey))
            {
                Debug.Log("DOES NOT CONATIN KEY");
            }

            var saveValue = windowToIdRelation[saveKey];
            windows[i] = GUI.Window(windowToIdRelation[windows[i]], windows[i], DrawNodeWindow, "Window " + i);

            //reapply key to the map

            windowToIdRelation.Remove(saveKey);

            windowToIdRelation[windows[i]] = saveValue;
            
        }

        EndWindows();
    }
    private Rect createNode(bool isManual = false)
    {
        if (GUILayout.Button("Create Node") || isManual)
        {
            Debug.Log("creating a new node");
            //create rect
            windows.Add(new Rect(10, 10, 100, 100));

            //take that last Id and add one
            int id = generateNewId(); //calls a sort might be expensive

            //this ensures uniqe keys
            //last window

            if (windows.Count >= 2)
            {

                var rect = windows[windows.Count - 2];
                rect.x++;
                windows[windows.Count - 1] = rect;
            }

            //add to map
            windowToIdRelation[windows[windows.Count - 1]] = id;

            //var keys = windowToIdRelation.Keys;

            //Debug.Log("KeyCount " + keys.Count.ToString());
            //foreach (var key in keys)
            //{
            //    Debug.Log("key " + key.ToString());
            // }
            return windows[windows.Count - 1];
        }
        if (GUILayout.Button("load from file"))
        {
            SaveLoadManager m = new SaveLoadManager();

            DTreeData dat = m.load("Data");

            generateGraph(dat);

        }
        return Rect.zero;
    }


    void DrawNodeWindow(int id)
    {
        if (GUILayout.Button("Attach"))
        {
            Debug.Log("id to attach" +  id);
            attatch(id);
        }
        if (GUILayout.Button("Detatch"))
        { 
            //windowsToDetach.Add(id);
            detatch(id);
            
        }
        if(GUILayout.Button("delete"))
        {
            //add deleting to nodes
            //windowsToDetach.Add(id);
            //nodesToDelete.Add(id);
           // windowsToDetach.Add(id);
            //deleteWindow(id);
            deleteNode(id);
        }


        GUI.DragWindow();
    }

    private void attatch(int id)
    {
        windowsToAttach.Add(id);
    }
    private void detatch(int id)
    {
        windowsToDetach.Add(id);
    }
    private void deleteNode(int id)
    {
        windowsToDetach.Add(id);
        deleteWindow(id);
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < 3; i++)
        {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    int generateNewId()
    {
        //make this not gen inf ids 

        activeIds.Sort();
        int id;
        if (activeIds.Count > 0 ) 
        {
            id = activeIds[activeIds.Count - 1];
        }
        else
        {
            id = 0;
        }
       
        id++;

        activeIds.Add(id);

        return id;
    }
    
    void deleteWindow(int id)
    {
        //find winow in map
        Rect key = new Rect();
        int removeIndex = 0;

        for(int i = 0;i < windows.Count;i++) 
        {
            if(windowToIdRelation[windows[i]] == id)
            {
                //we found the id at x key
                key = windows[i];
                removeIndex = i;
            }

        }
        
        if(attachMap.ContainsKey(id)) 
        {
            attachedWindows.Remove(id);
            attachedWindows.Remove(attachMap[id]);
            attachMap.Remove(id);
        }
       

        //remove it form the winow list
        windows.Remove(key);
        //remove it from the map
        windowToIdRelation.Remove(key);

    }

    Rect findWindowFromId(int id)
    {
        var keys = windowToIdRelation.Keys;

        foreach(var key in keys) 
        {
            if (windowToIdRelation[key] ==  id)
            {
                //found the right window return the key
                return key;
            }
        }

        //bad id return a null rect

        return Rect.zero;
    }

    //this function generates a graph from tree data this fucks up connections
    public void generateGraph(DTreeData tree)
    {
        Dictionary<TreeNode, Rect> nodeToWindow = new Dictionary<TreeNode, Rect>();
        DTreeData data = tree;

        //place root node in the map
        Rect baseNode = createNode(true);
        TreeNode rootTreeNode = tree.root;

        nodeToWindow[rootTreeNode] = baseNode;

        var nodes = data.allNodes;

        foreach(var node in nodes)
        {
            Rect window = createNode(true);
            TreeNode treeNode = node;

            nodeToWindow[node] = window;
        }

        //at this point we should have a relation between the nodes and windows
        //next step is to add the connections 
        var dict = data.returnDictionary();
        var keys = dict.Keys;

        foreach (var key in keys)
        {
            var children = dict[key];
            //lol map in a map its so over for me 
            int id;
            if (children[0] != null)
            {
                id = windowToIdRelation[nodeToWindow[children[0]]];
                attatch(id);
            }
            if (children[1] != null)
            {
                id = windowToIdRelation[nodeToWindow[children[1]]];
                attatch(id);

            }
            attatchWindows();
            makeCurve();



        }
       
    }
}