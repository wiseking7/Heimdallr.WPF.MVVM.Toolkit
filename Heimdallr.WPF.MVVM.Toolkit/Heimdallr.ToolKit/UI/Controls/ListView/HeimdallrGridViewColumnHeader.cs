using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일의 GridViewColumnHeader 커스텀 컨트롤
/// - 컬럼 숨김 기능(IsColumnHidden)
/// - 정렬 아이콘 표시 가능 (추후 확장 가능)
/// - DefaultStyleKey로 Generic.xaml 스타일 적용
/// </summary>
public class HeimdallrGridViewColumnHeader : GridViewColumnHeader
{
  static HeimdallrGridViewColumnHeader()
  {
    // 기본 스타일 키를 지정하여 Themes/Generic.xaml에서 스타일 적용 가능
    DefaultStyleKeyProperty.OverrideMetadata(
        typeof(HeimdallrGridViewColumnHeader),
        new FrameworkPropertyMetadata(typeof(HeimdallrGridViewColumnHeader))
    );
  }

  #region IsColumnHidden 의존성 속성

  /// <summary>
  /// 열 숨김 여부를 나타내는 의존성 속성
  /// true로 설정하면 실제 GridViewColumn.Width를 0으로 변경하여 숨김 처리
  /// </summary>
  public bool IsColumnHidden
  {
    get => (bool)GetValue(IsColumnHiddenProperty);
    set => SetValue(IsColumnHiddenProperty, value);
  }

  /// <summary>
  /// 종속성 속성 등록
  /// - 기본값: false
  /// - 변경 시 OnIsColumnHiddenChanged 콜백 호출
  /// </summary>
  public static readonly DependencyProperty IsColumnHiddenProperty =
      DependencyProperty.Register(
          nameof(IsColumnHidden),
          typeof(bool),
          typeof(HeimdallrGridViewColumnHeader),
          new PropertyMetadata(false, OnIsColumnHiddenChanged)
      );

  /// <summary>
  /// IsColumnHidden 변경 시 호출되는 콜백
  /// 실제 GridViewColumn.Width를 0 또는 Auto(NaN)로 변경
  /// </summary>
  private static void OnIsColumnHiddenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is not HeimdallrGridViewColumnHeader header) return;

    if (header.Column != null)
    {
      // 숨김이면 Width = 0, 표시이면 Width = Auto (NaN)
      header.Column.Width = (bool)e.NewValue ? 0 : Double.NaN;
    }
  }

  #endregion

  #region 정렬 기능 (Optional, 확장용)

  /// <summary>
  /// 향후 확장: 컬럼 클릭 시 정렬 아이콘 표시
  /// - SortDirection(Ascending/Descending) 상태 추적 가능
  /// - UI 템플릿(ControlTemplate)에서 Arrow 표시
  /// </summary>
  public ListSortDirection? SortDirection
  {
    get => (ListSortDirection?)GetValue(SortDirectionProperty);
    set => SetValue(SortDirectionProperty, value);
  }

  /// <summary>
  /// 종속성 등록: 정렬 방향
  /// </summary>
  public static readonly DependencyProperty SortDirectionProperty =
      DependencyProperty.Register(
          nameof(SortDirection),
          typeof(ListSortDirection?),
          typeof(HeimdallrGridViewColumnHeader),
          new PropertyMetadata(null)
      );

  #endregion

  /// <summary>
  /// 헤더 종속성 속성 기본값 null
  /// </summary>
  public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(object), typeof(HeimdallrGridViewColumnHeader), new PropertyMetadata(null));

  /// <summary>
  /// Hearder 속성
  /// </summary>
  public object Header
  {
    get => GetValue(HeaderProperty);
    set => SetValue(HeaderProperty, value);
  }
}


