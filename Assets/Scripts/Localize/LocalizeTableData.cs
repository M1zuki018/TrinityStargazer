using System;
using UnityEngine;

/// <summary>
/// 日・英のローカライズの文字列
/// </summary>
[Serializable]
public class LocalizeTableData
{
    [SerializeField] private string _jpn;
    [SerializeField] private string _eng;
    
    public string JPN => _jpn;
    public string ENG => _eng;
}
