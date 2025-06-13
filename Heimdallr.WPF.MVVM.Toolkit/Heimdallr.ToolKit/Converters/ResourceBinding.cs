using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// XAML에서 동적으로 리소스를 바인딩할 수 있도록 도와주는 커스텀 마크업 확장(MarkupExtension)입니다.
/// 주로 리소스 키를 동적으로 바인딩하고, 해당 리소스를 대상 DependencyProperty에 연결할 때 사용합니다.
/// </summary>
public class ResourceBinding : MarkupExtension
{
  #region Helper Properties

  /// <summary>
  /// 첨부 속성 getter
  /// </summary>
  public static object GetResourceBindingKeyHelper(DependencyObject obj)
  {
    return obj.GetValue(ResourceBindingKeyHelperProperty);
  }

  /// <summary>
  /// 첨부 속성 setter
  /// </summary>
  public static void SetResourceBindingKeyHelper(DependencyObject obj, object value)
  {
    obj.SetValue(ResourceBindingKeyHelperProperty, value);
  }

  /// <summary>
  /// 첨부 속성으로 등록된 ResourceBindingKeyHelperProperty
  /// 이 속성에 리소스 키와 대상 DependencyProperty를 튜플로 바인딩하여 리소스 변경시 자동 적용을 구현
  /// </summary>
  public static readonly DependencyProperty ResourceBindingKeyHelperProperty =
      DependencyProperty.RegisterAttached(
          "ResourceBindingKeyHelper",
          typeof(object),
          typeof(ResourceBinding),
          new PropertyMetadata(null, ResourceKeyChanged));

  /// <summary>
  /// ResourceBindingKeyHelperProperty 값이 변경될 때 호출되는 콜백 메서드
  /// 리소스 키가 바뀌면 대상 DependencyProperty에 SetResourceReference로 리소스를 다시 설정함
  /// </summary>
  private static void ResourceKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 바인딩 대상이 FrameworkElement가 아니거나, 새 값이 (object 리소스키, DependencyProperty) 튜플이 아니면 무시
    if (!(d is FrameworkElement target) || !(e.NewValue is Tuple<object, DependencyProperty> newVal))
    {
      return;
    }

    DependencyProperty dp = newVal.Item2;

    // 리소스 키가 null이면 대상 속성에 기본값을 설정
    if (newVal.Item1 == null)
    {
      target.SetValue(dp, dp.GetMetadata(target).DefaultValue);
      return;
    }

    // 리소스 키가 유효하면 SetResourceReference로 리소스를 동적으로 설정
    target.SetResourceReference(dp, newVal.Item1);
  }

  #endregion

  /// <summary>
  /// 기본 생성자
  /// </summary>
  public ResourceBinding() { }

  /// <summary>
  /// 경로 문자열을 받아서 PropertyPath로 변환해 설정하는 생성자
  /// </summary>
  /// <param name="path"></param>
  public ResourceBinding(string path)
  {
    Path = new PropertyPath(path);
  }

  /// <summary>
  /// XAML 파서가 이 마크업 확장을 만났을 때 호출되는 메서드.
  /// IProvideValueTarget를 통해 바인딩 대상 객체와 속성을 얻어내고,
  /// MultiBinding을 사용해 리소스 키와 대상 속성을 연결하는 바인딩을 설정함.
  /// </summary>
  /// <param name="serviceProvider">서비스 제공자</param>
  /// <returns>바인딩 결과 (실제 값이 아니라 DependencyProperty.UnsetValue)</returns>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    // 바인딩 대상 객체 및 속성 정보 가져오기
    IProvideValueTarget? provideValueTargetService =
        serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

    // 바인딩 대상 정보를 얻지 못하면 아무것도 설정하지 않음 (기본값 유지)
    if (provideValueTargetService == null)
      return DependencyProperty.UnsetValue;

    // 디자인 타임 특수 처리 (디자인 시 SharedDp 객체 무시)
    if (provideValueTargetService.TargetObject != null &&
        provideValueTargetService.TargetObject.GetType().FullName == "System.Windows.SharedDp")
    {
      return this;
    }

    // 대상이 FrameworkElement가 아니거나 대상 속성이 DependencyProperty가 아니면 중단
    if (provideValueTargetService.TargetObject is not FrameworkElement frameworkElement ||
        provideValueTargetService.TargetProperty is not DependencyProperty dependencyProperty)
    {
      return DependencyProperty.UnsetValue;
    }

    // 단일 Binding 객체 생성 및 속성 설정
    Binding binding = new Binding
    {
      Path = Path,
      XPath = XPath,
      Mode = Mode,
      UpdateSourceTrigger = UpdateSourceTrigger,
      Converter = Converter,
      ConverterParameter = ConverterParameter,
      ConverterCulture = ConverterCulture
    };

    if (RelativeSource != null)
      binding.RelativeSource = RelativeSource;

    if (ElementName != null)
      binding.ElementName = ElementName;

    if (Source != null)
      binding.Source = Source;

    binding.FallbackValue = FallbackValue;

    // MultiBinding 생성: 튜플(Tuple<object 리소스키, DependencyProperty 대상속성>)을 전달하는 헬퍼 컨버터 설정
    MultiBinding multiBinding = new MultiBinding
    {
      Converter = HelperConverter.Current,
      ConverterParameter = dependencyProperty,
      NotifyOnSourceUpdated = true
    };

    // 리소스 키 바인딩 추가
    multiBinding.Bindings.Add(binding);

    // 첨부 속성에 MultiBinding 설정하여 리소스키와 대상 속성을 함께 바인딩함
    _ = frameworkElement.SetBinding(ResourceBindingKeyHelperProperty, multiBinding);

    // ProvideValue는 실제 바인딩 값을 반환하지 않고, 기본값을 유지하도록 함
    return DependencyProperty.UnsetValue;
  }

  #region Binding Members (Binding과 유사한 속성들)

  /// <summary>바인딩 소스</summary>
  public object? Source { get; set; }

  /// <summary>바인딩 경로</summary>
  public PropertyPath? Path { get; set; }

  /// <summary>XPath (XML 바인딩용)</summary>
  [DefaultValue(null)]
  public string? XPath { get; set; }

  /// <summary>바인딩 모드</summary>
  [DefaultValue(BindingMode.Default)]
  public BindingMode Mode { get; set; }

  /// <summary>UpdateSourceTrigger 속성</summary>
  [DefaultValue(UpdateSourceTrigger.Default)]
  public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

  /// <summary>변환기</summary>
  [DefaultValue(null)]
  public IValueConverter? Converter { get; set; }

  /// <summary>변환기 파라미터</summary>
  [DefaultValue(null)]
  public object? ConverterParameter { get; set; }

  /// <summary>변환기 문화권</summary>
  [DefaultValue(null)]
  [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
  public CultureInfo? ConverterCulture { get; set; }

  /// <summary>RelativeSource</summary>
  [DefaultValue(null)]
  public RelativeSource? RelativeSource { get; set; }

  /// <summary>ElementName</summary>
  [DefaultValue(null)]
  public string? ElementName { get; set; }

  #endregion

  #region BindingBase Members

  /// <summary>
  /// 바인딩 실패 시 사용할 대체값
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