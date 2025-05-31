using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Thumb은 WPF에서 드래그 가능한 UI 요소를 만들 때 사용하는 기본 컨트롤입니다
/// </summary>
public class HeimdallrThumb : Thumb
{
  // 기본 스타일을 HeimdallrThumb에 연결
  // 기본적으로 손 모양 커서를 표시하고, 드래그을 제공하는데 사용됩니다.
  // 주로 크기 조정(크기 조절)위치 이동(드래긴Thumb이 드래그되는 동안 다른 UI 요소를 변경할 수 있는 인터페이스를 제공합니다.

  // 드래그 시작 시점
  private Point _dragStartPoint;
  private bool _isDragging = false;

  static HeimdallrThumb()
  {
    // 기본 스타일을 HeimdallrThumb에 연결
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrThumb),
        new FrameworkPropertyMetadata(typeof(HeimdallrThumb)));
  }

  public HeimdallrThumb()
  {
    // 마우스 왼쪽 버튼을 눌렀을 때 드래그를 시작
    PreviewMouseLeftButtonDown += HeimdallrThumb_PreviewMouseLeftButtonDown;
    PreviewMouseMove += HeimdallrThumb_PreviewMouseMove;
    PreviewMouseLeftButtonUp += HeimdallrThumb_PreviewMouseLeftButtonUp;
  }

  // 드래그 시작 시점
  private void HeimdallrThumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
  {
    _dragStartPoint = e.GetPosition(this); // 드래그 시작점 기록
    _isDragging = true;
    CaptureMouse(); // 마우스를 캡처하여 다른 컨트롤로 드래그를 방지
  }

  // 드래그 중인 위치 이동 처리
  private void HeimdallrThumb_PreviewMouseMove(object sender, MouseEventArgs e)
  {
    if (_isDragging)
    {
      var currentPoint = e.GetPosition(this); // 현재 마우스 위치
      var offsetX = currentPoint.X - _dragStartPoint.X;
      var offsetY = currentPoint.Y - _dragStartPoint.Y;

      // 드래그 이동을 적용
      var parentCanvas = VisualTreeHelper.GetParent(this) as Canvas;

      if (parentCanvas != null)
      {
        // Canvas에 드래그된 Thumb 위치 업데이트
        double newLeft = Canvas.GetLeft(this) + offsetX;
        double newTop = Canvas.GetTop(this) + offsetY;

        Canvas.SetLeft(this, newLeft);
        Canvas.SetTop(this, newTop);
      }

      _dragStartPoint = currentPoint; // 새로운 시작점 설정
    }
  }

  // 드래그 종료 처리
  private void HeimdallrThumb_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
  {
    _isDragging = false;
    ReleaseMouseCapture(); // 마우스 캡처 해제
  }
}
