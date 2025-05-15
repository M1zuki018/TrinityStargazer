using System;
using UnityEngine;

/// <summary>
/// シナリオ再生に関する設定
/// </summary>
public class ScenarioSetting : IDisposable
{
    private CanvasController_Settings_Environment _cc;
    private IGameManager _gameManager;

    public ScenarioSetting(CanvasController_Settings_Environment cc, IGameManager gameManager)
    {
        _cc = cc;
        _gameManager = gameManager;
        
        if (_cc != null)
        {
            _cc.OnScenarioSpeedChanged += CcOnOnScenarioSpeedChanged;
            _cc.OnUseAutoChanged += CcOnOnUseAutoChanged;
        }
        else
        {
            Debug.LogWarning($"[ScenarioSetting]{typeof(CanvasController_Settings_Environment)} がnullです");
        }
    }

    /// <summary>
    /// シナリオ再生速度を変更する
    /// </summary>
    private void CcOnOnScenarioSpeedChanged(int obj)
    {
        int currentSpeed = (int)_gameManager.Settings.ScenarioSpeed;
        _gameManager.Settings.ScenarioSpeed = (ScenarioSpeedEnum)currentSpeed + obj % Enum.GetValues(typeof(ScenarioSpeedEnum)).Length;
        Debug.Log($"シナリオ再生速度が変更されました: {_gameManager.Settings.ScenarioSpeed}");
    }
    
    /// <summary>
    /// シナリオをオート再生をするか変更する
    /// </summary>
    private void CcOnOnUseAutoChanged(bool value)
    {
        _gameManager.Settings.UseAuto = value;
        Debug.Log($"デフォルトオート再生設定が変更されました: {_gameManager.Settings.UseAuto}");
    }


    public void Dispose()
    {
        _cc.OnScenarioSpeedChanged -= CcOnOnScenarioSpeedChanged;
        _cc.OnUseAutoChanged -= CcOnOnUseAutoChanged;
    }
}
