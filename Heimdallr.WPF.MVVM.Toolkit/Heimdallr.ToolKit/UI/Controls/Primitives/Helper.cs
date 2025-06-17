using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 내부 헬퍼 클래스 - 다양한 WPF 관련 보조 기능을 제공
/// </summary>
internal static class Helper
{
  /// <summary>
  /// 시스템에서 애니메이션이 활성화되어 있고,
  /// 렌더링 Tier가 0보다 크면 애니메이션 사용 가능
  /// </summary>
  public static bool IsAnimationsEnabled =>
      SystemParameters.ClientAreaAnimation &&    // 시스템 설정에서 애니메이션이 켜져 있는가
      RenderCapability.Tier > 0;                // 그래픽 렌더링 Tier (0: 없음, 1~2: GPU 사용 가능)

  /// <summary>
  /// 시각적 요소(Visual)의 픽셀 변환 행렬(디바이스 좌표 변환)을 구함
  /// </summary>
  public static bool TryGetTransformToDevice(Visual visual, out Matrix value)
  {
    // visual로부터 PresentationSource 가져오기
    var presentationSource = PresentationSource.FromVisual(visual);
    if (presentationSource != null)
    {
      // CompositionTarget.TransformToDevice는 장치 DPI에 따라 논리 단위 → 장치 단위 변환 행렬
      value = presentationSource.CompositionTarget.TransformToDevice;
      return true;
    }

    // null일 경우 default 행렬 반환
    value = default;
    return false;
  }

  /// <summary>
  /// 두 UI 요소 간의 지정된 기준점(InterestPoint)을 기준으로 오프셋(거리 벡터) 계산
  /// </summary>
  public static Vector GetOffset(
      UIElement element1,              // 기준 요소 1
      InterestPoint interestPoint1,    // 기준점 위치 (예: TopLeft, Center 등)
      UIElement element2,              // 상대 요소 2
      InterestPoint interestPoint2,    // 상대 기준점
      Rect element2Bounds              // 요소 2의 렌더 영역 (필요 시 직접 지정 가능)
  )
  {
    // element1의 기준점을 element2 좌표계로 변환
    Point point = element1.TranslatePoint(GetPoint(element1, interestPoint1), element2);

    if (element2Bounds.IsEmpty)
    {
      // bounds가 없으면, element2 자체 기준점 사용
      return point - GetPoint(element2, interestPoint2);
    }
    else
    {
      // bounds가 있으면, bounds 안에서의 기준점을 사용
      return point - GetPoint(element2Bounds, interestPoint2);
    }
  }

  /// <summary>
  /// UIElement에서 기준점 위치 계산
  /// </summary>
  private static Point GetPoint(UIElement element, InterestPoint interestPoint)
  {
    // RenderSize를 기준으로 Rect 생성하여 넘김
    return GetPoint(new Rect(element.RenderSize), interestPoint);
  }

  /// <summary>
  /// Rect 사각형에서 지정된 InterestPoint 위치 계산
  /// </summary>
  private static Point GetPoint(Rect rect, InterestPoint interestPoint)
  {
    // 사각형의 위치를 기준으로 Point 반환
    switch (interestPoint)
    {
      case InterestPoint.TopLeft:
        return rect.TopLeft;
      case InterestPoint.TopRight:
        return rect.TopRight;
      case InterestPoint.BottomLeft:
        return rect.BottomLeft;
      case InterestPoint.BottomRight:
        return rect.BottomRight;
      case InterestPoint.Center:
        return new Point(rect.Left + rect.Width / 2,
                         rect.Top + rect.Height / 2);
      default:
        // 정의되지 않은 enum일 경우 예외 발생
        throw new ArgumentOutOfRangeException(nameof(interestPoint));
    }
  }

  /// <summary>
  /// DependencyProperty의 값이 기본값인지 확인 (스타일, 로컬 값 등 없음)
  /// </summary>
  public static bool HasDefaultValue(this DependencyObject d, DependencyProperty dp)
  {
    // BaseValueSource가 Default인 경우 (로컬값, 스타일, 상속 등 미사용)
    return DependencyPropertyHelper.GetValueSource(d, dp).BaseValueSource == BaseValueSource.Default;
  }

  /// <summary>
  /// DependencyProperty에 로컬값 또는 스타일이 지정되어 있는지 확인
  /// </summary>
  public static bool HasNonDefaultValue(this DependencyObject d, DependencyProperty dp)
  {
    return !HasDefaultValue(d, dp);
  }

  /// <summary>
  /// DependencyProperty에 로컬 값이 설정되어 있는지 확인
  /// </summary>
  public static bool HasLocalValue(this DependencyObject d, DependencyProperty dp)
  {
    // ReadLocalValue는 로컬 값이 없으면 UnsetValue 반환
    return d.ReadLocalValue(dp) != DependencyProperty.UnsetValue;
  }

  /// <summary>
  /// 지정된 Window의 DPI 스케일 값을 반환
  /// </summary>
  public static DpiScale2 GetDpi(this Window window)
  {
    if (window is null)
    {
      throw new ArgumentNullException(nameof(window));
    }

#if NET462_OR_NEWER
        // WPF 4.6.2 이상에서는 VisualTreeHelper.GetDpi() 지원
        return new DpiScale2(VisualTreeHelper.GetDpi(window));
#else
    // 하위 호환: Win32 핸들로부터 CompositionTarget.TransformToDevice 사용
    var hwnd = new WindowInteropHelper(window).Handle;
    var hwndSource = HwndSource.FromHwnd(hwnd);
    if (hwndSource != null)
    {
      Matrix transformToDevice = hwndSource.CompositionTarget.TransformToDevice;
      return new DpiScale2(transformToDevice.M11, transformToDevice.M22);
    }
    else
    {
      Debug.Fail("여기에 도달해서는 안 됩니다");
      return new DpiScale2(1, 1); // fallback to 96 DPI
    }
#endif
  }
}

/// <summary>
/// 사각형 또는 UI 요소의 기준점 위치를 정의하는 열거형
/// </summary>
internal enum InterestPoint
{
  TopLeft = 0,
  TopRight = 1,
  BottomLeft = 2,
  BottomRight = 3,
  Center = 4,
}

/// <summary>
/// DPI 스케일 값을 표현하는 구조체
/// WPF 내장 DpiScale과 유사한 사용자 정의 버전
/// </summary>
internal readonly record struct DpiScale2
{
  public DpiScale2(double dpiScaleX, double dpiScaleY)
  {
    DpiScaleX = dpiScaleX;
    DpiScaleY = dpiScaleY;
  }

#if NET462_OR_NEWER
    public DpiScale2(DpiScale dpiScale)
        : this(dpiScale.DpiScaleX, dpiScale.DpiScaleY)
    {
    }
#endif

  public double DpiScaleX { get; } // X축 DPI 배율
  public double DpiScaleY { get; } // Y축 DPI 배율
}