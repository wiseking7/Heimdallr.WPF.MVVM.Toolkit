
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Enum 값을 문자열로 변환합니다. DescriptionAttribute 가 있으면 그 값을 사용하고, 없으면 ToString()을 사용합니다.
/// </summary>
public class EnumToTextConverter : BaseValueConverter<EnumToTextConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Enum enumValue)
    {
      var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
      var description = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
      return description?.Description ?? enumValue.ToString();
    }

    return string.Empty;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string str && targetType.IsEnum)
    {
      foreach (var field in targetType.GetFields())
      {
        var desc = field.GetCustomAttribute<DescriptionAttribute>()?.Description;
        if (desc == str || field.Name == str)
          return Enum.Parse(targetType, field.Name);
      }
    }

    return Binding.DoNothing;
  }
}

/* 사용예제
public enum Status
{
  [Description("사용 중")]
  Active,
  [Description("사용 안 함")]
  Inactive
} */
