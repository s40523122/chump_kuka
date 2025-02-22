using System;
using System.ComponentModel;

/// <summary>
/// KukaParm 類別 (全域設定管理)
/// 
/// 這是一個靜態類別 (static class)，用來存放關於 Kuka api 的全域參數。
/// 當參數變更時，透過 PropertyChanged 事件通知所有訂閱者 (例如 UI) 來更新顯示。
/// 
/// - 主要功能:
///   1. 當設定變更時，會觸發 PropertyChanged 事件，以便 UI 或其他物件即時更新。
///   2. 使用 static 來確保全域唯一，不需要透過 Instance 存取。
/// 
/// - 使用方式:
///   直接透過 `KukaParm.參數名稱` 存取。
/// </summary>

public static class KukaParm
{
    public static event PropertyChangedEventHandler PropertyChanged;
    
    private static string _volume;
    private static string _theme = "light";
    private static bool _isDarkMode;
    

    public static string Volume
    {
        get => _volume;
        set
        {
            if (_volume != value)
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
    }

    public static string Theme
    {
        get => _theme;
        set
        {
            if (_theme != value)
            {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }
    }

    public static bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                OnPropertyChanged(nameof(IsDarkMode));
            }
        }
    }

    private static void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
}
