using Heimdallr.ToolKit.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrGridView는 GridView를 상속한 커스텀 뷰입니다.
/// 주요 기능:
/// 1. 자동 넘버링(#) 컬럼을 첫 번째 열에 삽입
/// 2. 기존 컬럼 보호
/// 3. 컬럼 헤더 스타일링과 컬럼 너비는 ListView에서 적용하도록 설계
/// 
/// 사용 방법:
/// - ListView.View에 HeimdallrGridView를 설정
/// - ListView 초기화 직후 또는 Loaded 이벤트에서 EnsureNumberingColumn() 호출
/// </summary>
public class HeimdallrGridView : GridView
{
  // 넘버링 컬럼이 이미 삽입되었는지 상태 저장
  private bool _numberingColumnInserted;

  /// <summary>
  /// 넘버링 컬럼을 자동으로 추가합니다.
  /// 호출 시점:
  /// - ListView.View에 HeimdallrGridView를 설정한 후
  /// - ListView 초기화 직후 또는 ListView.Loaded 이벤트에서 호출 권장
  /// </summary>
  public void EnsureNumberingColumn()
  {
    if (_numberingColumnInserted && !base.Columns.Any((GridViewColumn c) => c.Header?.ToString() == "#"))
    {
      base.Columns.Insert(0, CreateNumberingColumn());
      _numberingColumnInserted = true;
    }
  }

  /// <summary>
  /// 넘버링용 GridViewColumn 생성
  /// - 각 행의 인덱스를 1부터 표시
  /// - ListViewItem의 RelativeSource를 이용하여 인덱스 바인딩
  /// - IndexToNumberConverter 사용 (0-based → 1-based 변환)
  /// - 컬럼 너비는 파라미터로 지정 가능
  /// </summary>
  /// <param >컬럼 너비 (기본값: 너비 40)</param>
  /// <returns>자동 생성된 넘버링 컬럼</returns>
  private HeimdallrGridViewColumn CreateNumberingColumn()
  {
    FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(TextBlock));
    frameworkElementFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
    frameworkElementFactory.SetBinding(TextBlock.TextProperty, new Binding
    {
      RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(HeimdallrListViewItem), 1),
      Converter = new IndexToNumberConverter()
    });
    return new HeimdallrGridViewColumn
    {
      Header = "#",
      Width = 40.0,
      CellTemplate = new DataTemplate
      {
        VisualTree = frameworkElementFactory
      }
    };
  }
}
