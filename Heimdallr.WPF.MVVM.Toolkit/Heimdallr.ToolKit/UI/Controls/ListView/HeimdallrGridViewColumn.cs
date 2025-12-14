using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일에 맞춘 GridViewColumn 확장 클래스
/// - HeaderText, BindingPath를 통해 간편하게 컬럼 생성
/// - CellTemplate을 지정할 수 있어 커스텀 셀 UI 확장 가능
/// </summary>
/// <summary>
/// Heimdallr 스타일에 맞춘 GridViewColumn 확장 클래스
/// </summary>
public class HeimdallrGridViewColumn : GridViewColumn
{
  /// <summary>
  /// 헤더 텍스트
  /// </summary>
  public string? HeaderText
  {
    get => Header?.ToString();
    set
    {
      Header = value;
      Debug.WriteLine($"[HeimdallrGridViewColumn] HeaderText 설정됨 → {value}");

      TryAttachHeaderLoadedHandler();
    }
  }

  /// <summary>
  /// 바인딩 경로
  /// </summary>
  public string? BindingPath
  {
    get => (DisplayMemberBinding as Binding)?.Path?.Path;
    set
    {
      if (!string.IsNullOrEmpty(value))
      {
        DisplayMemberBinding = new Binding(value);
        Debug.WriteLine($"[HeimdallrGridViewColumn] BindingPath 설정됨 → {value}");
      }
    }
  }

  /// <summary>
  /// 데이터템플릿 속성
  /// </summary>
  public new DataTemplate? CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value;
      if (value != null)
        DisplayMemberBinding = null;
    }
  }

  /// <summary>
  /// 생성자
  /// </summary>
  public HeimdallrGridViewColumn()
  {
    Debug.WriteLine($"[HeimdallrGridViewColumn] 기본 생성자 호출됨");
  }

  /// <summary>
  /// 그리드뷰 컬럼 메서드
  /// </summary>
  /// <param name="headerText"></param>
  /// <param name="bindingPath"></param>
  /// <param name="width"></param>
  public HeimdallrGridViewColumn(string headerText, string? bindingPath = null, double width = 100)
  {
    Header = headerText;
    Width = width;

    if (!string.IsNullOrEmpty(bindingPath))
    {
      DisplayMemberBinding = new Binding(bindingPath);
    }

    TryAttachHeaderLoadedHandler();
  }

  private void TryAttachHeaderLoadedHandler()
  {
    if (Header is string headerText)
    {
      // 문자열일 경우 TextBlock으로 래핑
      var textBlock = new TextBlock
      {
        Text = headerText,
        Foreground = Brushes.Yellow,          // 기본 Foreground
        Background = Brushes.DarkSlateGray,   // 기본 Background
        HorizontalAlignment = HorizontalAlignment.Center
      };
      Header = textBlock;

      textBlock.Loaded -= OnHeaderLoaded;
      textBlock.Loaded += OnHeaderLoaded;
      Debug.WriteLine($"[HeimdallrGridViewColumn] HeaderText가 TextBlock으로 변환됨: {headerText}");
    }
    else if (Header is FrameworkElement headerElement)
    {
      headerElement.Loaded -= OnHeaderLoaded;
      headerElement.Loaded += OnHeaderLoaded;
    }
  }

  private void OnHeaderLoaded(object sender, RoutedEventArgs e)
  {
    if (sender is GridViewColumnHeader header)
    {
      if (header.Content is TextBlock tb)
      {
        Debug.WriteLine($"[HeimdallrGridViewColumn.cs]Header 로드됨: {tb.Text} | Foreground={tb.Foreground} | Background={tb.Background}");
      }
      else
      {
        Debug.WriteLine($"[HeimdallrGridViewColumn.cs]Header 로드됨: {header.Content} | Foreground={header.Foreground} | Background={header.Background}");
      }
    }
  }
  ///// <summary>
  ///// 향후 확장 가능
  ///// - HeimdallrGridViewColumnHeader와 연계하여
  /////   IsColumnHidden, SortDirection 등을 지원 가능
  ///// </summary>
}

