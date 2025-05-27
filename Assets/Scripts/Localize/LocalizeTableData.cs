using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 日・英のローカライズの文字列
/// </summary>
[Serializable]
public class LocalizeTableData
{
    [SerializeField] private Text _target;
    [SerializeField] private string _jpn;
    [SerializeField] private string _eng;
    
    public Text Target => _target;
    public string JPN => _jpn;
    public string ENG => _eng;
}
