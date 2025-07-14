using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// BaseMultiValueConverter는 XAML에서 사용 가능한 멀티 바인딩(MultiBinding) 전용 추상 변환기 기본 클래스입니다.
/// </summary>
/// <typeparam name="T">변환기 타입으로, 매개 변수 없는 생성자를 가진 클래스여야 합니다.</typeparam>
public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : class, new()
{
  // 정적 인스턴스를 재사용하여 성능을 최적화합니다.
  private static T? _converter = null;

  /// <summary>
  /// XAML에서 변환기 인스턴스를 반환하는 메서드입니다.
  /// </summary>
  /// <param name="serviceProvider">XAML 서비스 제공자입니다.</param>
  /// <returns>싱글턴 형태로 재사용되는 변환기 인스턴스입니다.</returns>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return _converter ??= new T();
  }

  /// <summary>
  /// 여러 입력 값을 단일 출력 값으로 변환할 때 사용됩니다.
  /// 반드시 오버라이드하여 구현해야 합니다.
  /// </summary>
  /// <param name="values">여러 개의 바인딩 입력 값입니다.</param>
  /// <param name="targetType">변환될 대상 타입입니다.</param>
  /// <param name="parameter">변환에 필요한 선택적 매개변수입니다.</param>
  /// <param name="culture">문화권 정보입니다.</param>
  /// <returns>변환된 단일 출력 값입니다.</returns>
  public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

  /// <summary>
  /// 단일 값을 여러 값으로 다시 변환할 때 사용됩니다.
  /// 반드시 오버라이드하여 구현해야 합니다.
  /// </summary>
  /// <param name="value">변환된 단일 값입니다.</param>
  /// <param name="targetTypes">각각의 바인딩 대상 타입 배열입니다.</param>
  /// <param name="parameter">변환에 필요한 선택적 매개변수입니다.</param>
  /// <param name="culture">문화권 정보입니다.</param>
  /// <returns>여러 개의 값으로 변환된 결과입니다.</returns>
  public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
}

