using System.Globalization;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// DataGridRowIndexConverter
/// DataGridRow 객체를 받아 해당 행(row)의 0부터 시작하는 인덱스에 1을 더해
/// 사용자에게 보여줄 행 번호(1부터 시작)를 반환하는 값 변환기(ValueConverter)입니다.
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
  /// <returns>해당 행 번호 (1부터 시작), value가 DataGridRow가 아니면 null 반환</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 DataGridRow 타입인지 안전하게 캐스팅 시도
    var row = value as DataGridRow;

    // row가 null이 아니면 인덱스 + 1 반환, null이면 빈 문자열 반환
    // (null 반환 시 바인딩 에러 또는 UI에 "null"로 표시될 수 있기 때문)
    if (row != null)
    {
      // GetIndex()는 0부터 시작하는 행 번호를 반환
      int index = row.GetIndex();
      return index + 1; // 사용자에게 보여줄 1부터 시작하는 번호
    }
    else
    {
      return string.Empty; // 혹은 null 대신 빈 문자열 반환으로 안전하게 처리
    }
  }

  /// <summary>
  /// ConvertBack 메서드
  /// 일반적으로 단방향 바인딩에서 사용되며, 역변환은 지원하지 않으므로 예외 발생.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotSupportedException();
}
