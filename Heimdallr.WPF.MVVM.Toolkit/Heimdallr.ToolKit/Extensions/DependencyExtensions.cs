using System.Windows;

namespace Heimdallr.ToolKit.Extensions;

/// <summary>
/// WPF (Windows Presentation Foundation) 개발에서 자주 쓰이는 DependencyProperty의 기본값 설정 여부를 확인하고 
/// 값을 설정하는 유틸리티 확장 메서드를 제공하는 매우 실용적인 도우미 클래스입니다
/// </summary>
public static class DependencyExtensions
{
  /// <summary>
  /// 지정된 <see cref="DependencyProperty"/>가 현재 기본값(Default) 상태일 경우,
  /// 해당 속성에 사용자가 전달한 <paramref name="value"/> 값을 설정합니다.
  /// 
  /// 일반적으로 컨트롤에 기본값을 설정할 때,
  /// 외부에서 명시적으로 값을 설정하지 않았을 경우에만 내부에서 값을 지정하고 싶을 때 유용하게 사용됩니다.
  /// </summary>
  /// <typeparam name="T">설정하려는 값의 타입입니다. (예: string, Brush, bool 등)</typeparam>
  /// <param name="o">확장 메서드를 호출하는 대상</param>
  /// <param name="property">기본값인지 확인하고, 필요시 값을 설정할</param>
  /// <param name="value">기본값 상태일 때 설정할 값입니다</param>
  /// <returns>
  /// 속성이 현재 기본값 상태인 경우 값을 설정하고 <c>true</c>를 반환합니다.
  /// 그렇지 않으면 값을 설정하지 않고 <c>false</c>를 반환합니다.</returns>
  public static bool SetIfDefault<T>(this DependencyObject o, DependencyProperty property, T value)
  {
    if (DependencyPropertyHelper.GetValueSource(o, property).BaseValueSource == BaseValueSource.Default)
    {
      o.SetValue(property, value);

      return true;
    }

    return false;
  }
}
