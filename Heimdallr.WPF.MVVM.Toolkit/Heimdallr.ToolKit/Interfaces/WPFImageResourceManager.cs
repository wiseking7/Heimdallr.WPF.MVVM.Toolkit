using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Heimdallr.ToolKit.Interfaces;

/// <summary>
/// 
/// </summary>
public class WPFImageResourceManager : IResourceManager
{
  private readonly Dictionary<string, ImageSource> _cache = new();

  /// <summary>
  /// 이미지 파일을 로딩하고 캐시에 저장
  /// </summary>
  public object? Load(string key)
  {
    if (_cache.ContainsKey(key))
      return _cache[key];

    if (!File.Exists(key))
      return null;

    try
    {
      var bitmap = new BitmapImage();
      bitmap.BeginInit();
      bitmap.CacheOption = BitmapCacheOption.OnLoad;
      bitmap.UriSource = new Uri(key, UriKind.RelativeOrAbsolute);
      bitmap.EndInit();
      bitmap.Freeze(); // UI 스레드 안전

      _cache[key] = bitmap;
      return bitmap;
    }
    catch
    {
      return null;
    }
  }

  /// <summary>
  /// 캐시에서 이미지 조회
  /// </summary>
  public object? Get(string key)
  {
    _cache.TryGetValue(key, out var image);
    return image;
  }

  /// <summary>
  /// 특정 리소스 해제
  /// </summary>
  public void Release(string key)
  {
    if (_cache.ContainsKey(key))
      _cache.Remove(key);
  }

  /// <summary>
  /// 모든 리소스 해제
  /// </summary>
  public void ReleaseAll()
  {
    _cache.Clear();
  }
}
