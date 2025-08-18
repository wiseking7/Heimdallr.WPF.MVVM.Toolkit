using System.Globalization;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// DataGridRowIndexConverter
/// DataGridRow 객체를 받아 해당 행(row)의 0부터 시작하는 인덱스에 1을 더해
/// 사용자에게 보여줄 행 번호(1부터 시작)를 반환하는 값 변환기(ValueConverter)입니다.
///
/// 기존 GetIndex() 방식은 가상화(Virtualization)로 인해 인덱스가 꼬이는 문제가 있어,
/// 이 구현은 DataGrid의 ItemContainerGenerator를 사용하여 실제 행 인덱스를 안정적으로 조회합니다.
/// </summary>
public class DataGridRowIndexConverter : BaseValueConverter<DataGridRowIndexConverter>
{
  /// <summary>
  /// Convert 메서드
  /// DataGridRow 객체를 받아서, 행 번호를 반환합니다.
  /// DataGrid에서 행 인덱스는 0부터 시작하므로, 사용자에게 친숙한 1부터 시작하는 번호로 변환합니다.
  /// </summary>
  /// <param name="value">Binding 대상에서 전달된 값 (예: DataGridRow)</param>
  /// <param name="targetType">변환 대상 타입 (보통 string 또는 int)</param>
  /// <param name="parameter">변환에 추가적인 파라미터가 필요할 때 사용 (여기서는 미사용)</param>
  /// <param name="culture">문화권 정보 (숫자, 날짜 형식 변환에 사용됨)</param>
  /// <returns>
  /// 해당 행 번호 (1부터 시작), 
  /// value가 DataGridRow가 아니거나 DataGrid를 찾지 못하면 빈 문자열 반환
  /// </returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is DataGridRow row)
    {
      // DataGridRow를 포함하는 DataGrid 컨트롤을 찾음
      var dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;
      if (dataGrid != null)
      {
        // ItemContainerGenerator를 통해 실제 인덱스 조회 (가상화 환경에서 더 안정적)
        int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);
        return (index + 1).ToString(); // 1부터 시작하는 번호 반환
      }
    }
    return string.Empty;
  }

  /// <summary>
  /// ConvertBack 메서드
  /// 일반적으로 단방향 바인딩에서 사용되며, 역변환은 지원하지 않으므로 예외 발생.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotSupportedException();
}
