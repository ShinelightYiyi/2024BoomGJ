/*using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.DialogueTrees
{

    [Name("旁白")]
    [Description("旁白")]
    public class NarrationNode : DTNode
    {
        [SerializeField]
        public Statement statement = new Statement("这是旁白");

        public override bool requireActorSelection { get { return false; } }

        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            var tempStatement = statement.BlackboardReplace(bb);
            //DialogueTree.RequestSubtitles(new SubtitlesRequestInfo(finalActor, tempStatement, OnStatementFinish));
            //写入旁白UGUI控件的控制（回调OnStatementFinish（））
            Debug.Log(tempStatement);
            return Status.Running;
        }

        void OnStatementFinish()
        {
            status = Status.Success;
            DLGTree.Continue();
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.BeginVertical(Styles.roundedBox);
            GUILayout.Label("\"<i> " + statement.text.CapLength(30) + "</i> \"");
            GUILayout.EndVertical();
        }
#endif

    }
}*/