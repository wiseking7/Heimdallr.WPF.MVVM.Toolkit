using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;


/// <summary>
/// MarkupExtension 은 XAML에서 사용할 수 있도록 확장 기능을 제공하는 기본 클래스입니다
/// </summary>
/// <typeparam name="T">class, new()</typeparam>
public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new()
{

  // Converter 는 정적 필드로, 생성된 변환기 인스턴스를 저장합니다. 
  // 클래스가 처음 호출될 때 인스턴스가 생성되고 이후에는 같은 인스턴스를 재사용합니다.
  // T?: C# 8.0부터 지원되는 null 허용 타입을 사용하여, 이 필드는 null일 수도 있습니다
  private static T? Converter = null;

  /// <summary>
  /// XAML에서 변환기 인스턴스를 제공하는 메서드입니다
  /// </summary>
  /// <param name="serviceProvider">XAML의 서비스 제공자를 통해 필요한 서비스를 받을 수 있습니다</param>
  /// <returns></returns>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    // Converter가 null일 경우, 새로운 T 인스턴스를 생성하여 Converter에 할당하고 반환합니다.
    // 그렇지 않으면 기존의 Converter를 반환합니다
    return Converter ?? (Converter = new T());
  }

  /// <summary>
  /// 이 메서드는 반드시 구현해야 하는 추상 메서드입니다.
  /// </summary>
  /// <param name="value">변환된 입력 값 입니다</param>
  /// <param name="targetType">변환할 목표 타입입니다</param>
  /// <param name="parameter">변환에 사용할 추가 매개변수 입니다</param>
  /// <param name="culture">현재 문화권 정보입니다</param>
  /// <returns></returns>
  public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

  /// <summary>
  /// 이 메서드는 반드시 구현해야 하는 추상 메서드입니다.
  /// </summary>
  /// <param name="value">변환된 입력 값 입니다</param>
  /// <param name="targetType">변환할 목표 타입입니다</param>
  /// <param name="parameter">변환에 사용할 추가 매개변수 입니다</param>
  /// <param name="culture">현재 문화권 정보입니다</param>
  /// <returns></returns>
  public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}

