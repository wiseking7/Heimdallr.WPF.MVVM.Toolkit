using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// TreeView 의 하위자식 마진 구하기
/// </summary>
public class DepthConverter : BaseValueConverter<DepthConverter>
{
  /// <summary>
  /// Convert 메서드는 뷰모델의 값(예: 계층 깊이)을 UI 요소의 속성(예: Margin)으로 변환할 때 사용됩니다.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    TreeViewItem? item = (TreeViewItem?)value;

    int level = 0;

    while (item != null)
    {
      item = ItemsControl.ItemsControlFromItemContainer(item) as TreeViewItem;

      level++;
    }

    // 16Px 단위로 들여쓰기
    return new Thickness(level * 16, 0, 0, 0);
  }

  /// <summary>
  /// ConvertBack은 일반적으로 양방향 바인딩에 필요하지만, 여기서는 단방향 변환이므로 구현하지 않고 예외를 던집니다.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    // 역변환은 사용하지 않음
    => throw new NotImplementedException();

}
