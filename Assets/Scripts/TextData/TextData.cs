using System;

/// <summary>
/// ランダム会話のテキストデータを保存する構造体
/// </summary>
[Serializable]
public struct TextData
{
    public string JpnText; // 日本語テキスト
    public string EngText; // 英語テキスト
}