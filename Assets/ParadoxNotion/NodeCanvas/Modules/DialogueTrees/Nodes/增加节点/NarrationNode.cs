/*using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.DialogueTrees
{

    [Name("�԰�")]
    [Description("�԰�")]
    public class NarrationNode : DTNode
    {
        [SerializeField]
        public Statement statement = new Statement("�����԰�");

        public override bool requireActorSelection { get { return false; } }

        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            var tempStatement = statement.BlackboardReplace(bb);
            //DialogueTree.RequestSubtitles(new SubtitlesRequestInfo(finalActor, tempStatement, OnStatementFinish));
            //д���԰�UGUI�ؼ��Ŀ��ƣ��ص�OnStatementFinish������
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