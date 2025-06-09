using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Indicators;

public class LoadingIndicator : Control
{
  // IndicatorType 속성
  // 이 속성은 Indicator 의 형식을 지정합니다.
  // 예를 들어, 'Twist', 'Spin' 등이 될 수 있습니다.
  public LoadingSpinnerType SpinnerType
  {
    get => (LoadingSpinnerType)GetValue(SpinnerTypeTypeProperty);
    set => SetValue(SpinnerTypeTypeProperty, value);
  }

  // IndicatorType 속성에 대한 종속성 속성 정의
  // 이 종속성 속성은 IndicatorType의 값이 변경될 때 UI에 반영되도록 합니다.
  // 기본값 -> IndicatorType.Twist
  public static readonly DependencyProperty SpinnerTypeTypeProperty =
      DependencyProperty.Register(nameof(SpinnerType), typeof(LoadingSpinnerType),
        typeof(LoadingIndicator),
          new PropertyMetadata(LoadingSpinnerType.None));

  // 정적 생성자: DefaultStyleKey를 오버라이드하여 기본 스타일을 정의합니다.
  // 'Indicator' 컨트롤의 스타일은 기본적으로 이 클래스를 사용하게 됩니다.
  static LoadingIndicator()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingIndicator),
        new FrameworkPropertyMetadata(typeof(LoadingIndicator)));
  }


  // OnApplyTemplate 메서드: 템플릿이 적용되었을 때 호출됩니다.
  // 템플릿이 적용되면 컨트롤의 시각적 상태를 업데이트합니다.
  public override void OnApplyTemplate()
  {
    UpdateVisualState();
  }

  // OnPropertyChanged 메서드: 속성 값이 변경될 때 호출됩니다.
  // 속성 값이 변경되면 해당 속성에 맞춰 시각적 상태를 업데이트합니다.
  protected override void OnPropertyChanged
    (DependencyPropertyChangedEventArgs e)
  {
    base.OnPropertyChanged(e);

    // IsVisible 속성이 변경되었을 때 시각적 상태를 업데이트합니다.
    if (e.Property == IsVisibleProperty)
    {
      UpdateVisualState();
    }
  }

  // UpdateVisualState 메서드: 컨트롤의 시각적 상태를 변경합니다.
  private void UpdateVisualState()
  {
    // 템플릿이 적용되지 않으면 업데이트를 건너뜁니다.
    if (Template == null)
    {
      return;
    }

    // 템플릿에서 'MainGrid'라는 이름의 자식을 찾습니다.
    var MainGrid = (FrameworkElement)GetTemplateChild("MainGrid");

    if (MainGrid == null)
    {
      return;
    }

    // IsVisible 속성에 따라 'Active' 또는 'Inactive' 상태를 설정합니다.
    var TargetState = IsVisible ? "Active" : "Inactive";

    // 'MainGrid'의 상태를 변경하여 시각적 상태를 반영합니다.
    VisualStateManager.GoToElementState(MainGrid, TargetState, true);
  }
}

