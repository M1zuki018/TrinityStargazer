using UnityEngine;

/// <summary>
/// バトル報酬を管理するクラス
/// </summary>
public class RewordManager : ViewBase
{
    [SerializeField][ExpandableSO] private RewordTableSO _rewordTable;
    // 勝利数による変動 ○点以上でR以上確定みたいな感じ
}
