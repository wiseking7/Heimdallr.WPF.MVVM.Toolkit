using CommunityToolkit.Mvvm.ComponentModel;
using Heimdallr.ToolKit.Attributes;
using Heimdallr.ToolKit.Helpers;
using System.Reflection;

namespace Heimdallr.ToolKit.Dialogs.Base;

/// <summary>
/// 다이얼로그 ViewModel의 베이스 클래스.
/// Prism의 IDialogAware를 구현하여 다이얼로그 제어를 담당.
/// UseDimmingAttribute를 기반으로 다이얼로그가 열릴 때 배경 디밍 효과를 자동 적용. 
/// </summary>
public class ObservableDialog : ObservableObject, IDialogAware
{
  private readonly DimmingManager _dimmingManager;

  /// <summary>
  /// 다이얼로그 제목
  /// </summary>
  public string? Title { get; set; }

  /// <summary>
  /// Prism 프레임워크가 내부적으로 사용하는 RequestClose 델리게이트.
  /// 이 속성은 인터페이스 명시적 구현이므로 외부에서는 접근되지 않음.
  /// 실질적으로 사용하지 않으므로 null 반환.
  /// </summary>
  DialogCloseListener IDialogAware.RequestClose => throw new NotImplementedException();

  /// <summary>
  /// 다이얼로그 닫기 요청할 때 호출되는 이벤트, IDialogResult 인터페이스의 일환으로 사용
  /// </summary>
  public event Action<IDialogResult>? RequestClose;

  /// <summary>
  /// 기본생성자 내부에서 디밍 메니저를 초기화
  /// </summary>
  public ObservableDialog()
  {
    _dimmingManager = new DimmingManager();
  }

  /// <summary>
  /// 다이얼로그를 닫을 수 있는지 여부를 반환, 기본 구현은 항상 True
  /// </summary>
  /// <returns>다이얼로그를 닫을 수 있는지 여부</returns>
  public virtual bool CanCloseDialog()
  {
    return true;
  }

  private void CloseDialog()
  {
    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
  }


  /// <summary>
  /// 다이얼로그가 닫힐 때 호출됨, UseDimming 특성이 설정된 경우 디밍 효과를 제거
  /// </summary>
  public virtual void OnDialogClosed()
  {
    var useDimmingAttribute = GetType().GetCustomAttribute<UseDimmingAttribute>();
    if (useDimmingAttribute != null && useDimmingAttribute.UseDimming)
    {
      _dimmingManager.Dimming(false);
    }
  }

  /// <summary>
  /// 다이얼로그가 열릴 때 호출됨, UseDimming 특성이 설정된 경우 디밍 효과를 적용
  /// </summary>
  /// <param name="parameters">다이얼로그 열기 시 전달된 파라미터</param>
  public virtual void OnDialogOpened(IDialogParameters parameters)
  {
    var useDimmingAttribute = GetType().GetCustomAttribute<UseDimmingAttribute>();
    if (useDimmingAttribute != null && useDimmingAttribute.UseDimming)
    {
      _dimmingManager.Dimming(true);
    }
  }
}
