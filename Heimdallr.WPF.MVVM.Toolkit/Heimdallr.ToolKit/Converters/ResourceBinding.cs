using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// XAML에서 동적으로 리소스를 바인딩할 수 있도록 도와주는 커스텀 마크업 확장(MarkupExtension) 입니다.
/// 사용시점은 -> "리소스를 동적으로 바인딩하고 싶을 때
/// </summary>
public class ResourceBinding : MarkupExtension
{
  #region Helper Properties
  public static object GetResourceBindingKeyHelper(DependencyObject obj)
  {
    return obj.GetValue(ResourceBindingKeyHelperProperty);
  }

  public static void SetResourceBindingKeyHelper(DependencyObject obj, object value)
  {
    obj.SetValue(ResourceBindingKeyHelperProperty, value);
  }

  /// <summary>
  /// DependencyProperty로 정의된 첨부 속성
  /// XAML에서 ResourceBinding을 적용할 때, 리소스 키와 대상 속성을 연결하는 역할
  /// 동작은 -> 속성 값이 변경되면 ResourceKeyChanged 콜백 메서드가 호출되어, 해당 리소스를 대상 속성에 설정합니다.
  /// </summary>
  public static readonly DependencyProperty ResourceBindingKeyHelperProperty =
      DependencyProperty.RegisterAttached("ResourceBindingKeyHelper",
        typeof(object), typeof(ResourceBinding),
        new PropertyMetadata(null, ResourceKeyChanged));

  /// <summary>
  /// 리소스 키가 변경되었을 때 호출되어, 해당 리소스를 대상 속성에 설정합니다.
  /// 동작 -> 리소스 키가 null이면, 대상 속성에 기본값을 설정합니다.
  /// 그렇지 않으면, SetResourceReference를 사용하여 리소스를 설정합니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void ResourceKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 바인딩 대상이 FrameworkElement가 아니거나, 값이 튜플이 아니면 무시
    if (!(d is FrameworkElement target) || !(e.NewValue is Tuple<object, DependencyProperty> newVal))
    {
      return;
    }

    // 바인딩할 DependencyProperty 추출
    DependencyProperty dp = newVal.Item2;

    // 리소스 키가 null이면 기본값으로 설정
    if (newVal.Item1 == null)
    {
      target.SetValue(dp, dp.GetMetadata(target).DefaultValue);
      return;
    }

    // 리소스 키가 유효하면 해당 리소스를 설정
    target.SetResourceReference(dp, newVal.Item1);
  }
  #endregion

  // 생성자 오버로드: Path를 문자열로 받는 버전
  public ResourceBinding() { }

  public ResourceBinding(string path)
  {
    Path = new PropertyPath(path);
  }

  /// <summary>
  /// XAML에서 ResourceBinding이 사용될 때 호출되며, 실제 바인딩을 설정하는 역할을 합니다.
  /// 동작 -> IProvideValueTarget을 통해 바인딩 대상 객체와 속성을 확인합니다.
  /// 대상이 FrameworkElement이고, 속성이 DependencyProperty인 경우에만 바인딩을 설정
  /// MultiBinding을 사용하여, 리소스 키와 대상 속성을 함께 바인딩합니다
  /// </summary>
  /// <param name="serviceProvider"></param>
  /// <returns></returns>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    // 타겟 정보(IProvideValueTarget) 획득
    IProvideValueTarget? provideValueTargetService = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

    if (provideValueTargetService == null)
    {
      // 바인딩 정보를 얻을 수 없으면 아무 값도 설정하지 않음
      // WPF 내부에서 사용하는 특별한 값으로,
      // "이 속성에 값을 설정하지 않았다" 또는 바인딩 실패 시 기본값을 사용하겠다는 의도를 나타냅니다
      // ProvideValue 리턴할 때 세 가지의 주요 시나리오
      // 바인딩 성공 반환값(바인딩 결과(object) 의미(정상동작)
      // 바인딩 조건 미충족(DependencyProperty.UnsetValue) 의미(설정하지 마라, 기본값 유지)
      // 바인딩 오류(예외 또는 null) 의미(런타임 오류 가능)
      // 왜 return null; 아닌가요
      // return null;을 사용하면 바인딩 오류로 처리되어 XAML 런타임 예외가 발생할 수 있습니다.
      // 반면 return DependencyProperty.UnsetValue;는 **"아무것도 하지 마라"**는 안전한 신호로,
      // WPF는 기본값을 유지하며 에러 없이 동작합니다.
      return DependencyProperty.UnsetValue;  // return null
    }

    // WPF 디자인 타임 특수 객체 처리: SharedDp는 무시 (XAML 디자인 시 오류 방지용)
    if (provideValueTargetService.TargetObject != null &&
        provideValueTargetService.TargetObject.GetType().FullName == "System.Windows.SharedDp")
    {
      return this;
    }

    // is not 연산자는 "해당 타입이 아니라면" 또는 "null" 이거나 해당 타입이 아니면 이라는 "패턴 매칭 조건"입니다.(null 검사도 포함)
    /* 예
       if (obj is not FrameworkElement fe)
       
      이 조건은 다음과 같습니다:

      obj가 FrameworkElement 타입이 아니거나 null이면 true
      그렇지 않고, FrameworkElement 타입이면 false
      (그리고 타입이 맞으면 fe라는 변수에 캐스팅된 인스턴스가 바인딩됨)
     */

    if (provideValueTargetService.TargetObject is not FrameworkElement frameworkElement ||
        provideValueTargetService.TargetProperty is not DependencyProperty dependencyProperty)
    {
      // 바인딩 대상 정보가 없거나 잘못된 경우, 기본값 유지
      return DependencyProperty.UnsetValue;
    }

    // 단일 Binding 설정
    Binding binding = new Binding();

    #region binding
    binding.Path = Path;
    binding.XPath = XPath;
    binding.Mode = Mode;
    binding.UpdateSourceTrigger = UpdateSourceTrigger;
    binding.Converter = Converter;
    binding.ConverterParameter = ConverterParameter;
    binding.ConverterCulture = ConverterCulture;

    // RelativeSource나 ElementName, Source 중 하나를 설정
    if (RelativeSource != null)
    {
      binding.RelativeSource = RelativeSource;
    }

    if (ElementName != null)
    {
      binding.ElementName = ElementName;
    }

    if (Source != null)
    {
      binding.Source = Source;
    }

    binding.FallbackValue = FallbackValue;

    // MultiBinding 설정: 리소스 키 + 대상 속성을 하나의 객체로 전달
    MultiBinding multiBinding = new MultiBinding()
    {
      // 내부적으로 Tuple<리소스키, 속성> 생성
      Converter = HelperConverter.Current,
      ConverterParameter = dependencyProperty
    };

    // ViewModel에서 키만 전달해도 됨
    multiBinding.Bindings.Add(binding);

    multiBinding.NotifyOnSourceUpdated = true;

    // 리소스 키와 대상 속성을 ResourceBindingKeyHelperProperty에 바인딩
    _ = frameworkElement.SetBinding(ResourceBindingKeyHelperProperty, multiBinding);

    // 시각적으로는 바인딩 없음. 내부 처리만
    return DependencyProperty.UnsetValue;

    #endregion
  }

  #region Binding Members
  /// <summary>
  /// 소스 경로(CLR 바인딩용)
  /// </summary>
  public object? Source { get; set; }

  /// <summary>
  /// 소스 경로(CLR 바인딩용)
  /// </summary>
  public PropertyPath? Path { get; set; }

  /// <summary>
  /// XPath 경로(XML 바인딩용)
  /// </summary>
  [DefaultValue(null)]
  public string? XPath { get; set; }

  /// <summary>
  /// Binding mode
  /// </summary>
  [DefaultValue(BindingMode.Default)]
  public BindingMode Mode { get; set; }

  /// <summary>
  /// Update type
  /// </summary>
  [DefaultValue(UpdateSourceTrigger.Default)]
  public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

  /// <summary>
  /// 적용할 컨버터
  /// </summary>
  [DefaultValue(null)]
  public IValueConverter? Converter { get; set; }

  /// <summary>
  /// 변환기에 전달할 매개변수.
  /// </summary>
  [DefaultValue(null)]
  public object? ConverterParameter { get; set; }

  /// <summary>
  /// 컨버터를 평가하는 문화
  /// </summary>
  [DefaultValue(null)]
  [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
  public CultureInfo? ConverterCulture { get; set; }

  /// <summary>
  /// 대상 요소를 기준으로 소스로 사용할 개체에 대한 설명.
  /// </summary>
  [DefaultValue(null)]
  public RelativeSource? RelativeSource { get; set; }

  /// <summary>
  /// 소스로 사용할 요소의 이름
  /// </summary>
  [DefaultValue(null)]
  public string? ElementName { get; set; }
  #endregion

  #region BindingBase Members
  /// <summary>
  /// 소스가 값을 제공할 수 없는 경우 사용할 값
  /// <Remarks>DependencyProperty.UnsetValue로 초기화되었습니다.
  /// FallbackValue 가 설정되지 않은 경우 BindingExpression 은 Binding 이 실제 값을 얻을 수 없을 때 대상 속성의 기본값을 반환합니다.</Remarks>
  /// </summary>
  public object? FallbackValue { get; set; }
  #endregion
}
/* 사용예제
 어떻게 사용하는가?
<!-- ViewModel에 있는 ThemeKey 속성의 값으로 리소스를 바인딩 -->
<Border Background="{local:ResourceBinding Path=ThemeKey}" />

* ViewModel
public class MainViewModel
{
  // ThemeKey가 변경되면 Background 색상이 바뀜
  public string ThemeKey { get; set; } = "PrimaryBackgroundBrush";
}

* XAML 정의된 소스
<Application.Resources>
  <SolidColorBrush x:Key="PrimaryBackgroundBrush" Color="#FFCCE5FF" />
  <SolidColorBrush x:Key="SecondaryBackgroundBrush" Color="#FFE5E5E5" />
</Application.Resources>

* 언제 쓰나?
상황	                                                                             기존 방식	                           문제	                ResourceBinding으로 해결
리소스를 ViewModel의 값으로 동적으로 바꾸고 싶을 때	                    {StaticResource}, {DynamicResource}       	Binding 사용 불가	             Binding 가능
테마, 컬러, 스타일 등 XAML 리소스를 뷰모델 값으로 지정하고 싶을 때	                     불가                            MVVM에 어긋남	         ViewModel 속성 값으로 리소스 키 지정 가능

*/