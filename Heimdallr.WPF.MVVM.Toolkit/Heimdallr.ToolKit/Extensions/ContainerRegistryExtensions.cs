namespace Heimdallr.ToolKit.Extensions;
/// <summary>
/// 
/// </summary>
public static class ContainerRegistryExtensions
{
  /// <summary>
  /// Prism 의 IContainerRegistry 확장메서드
  /// </summary>
  /// <param name="containerRegistry"></param>
  /// <param name="views"></param>
  public static void RegisterViewsForNavigation(this IContainerRegistry containerRegistry,
      params (Type view, Type viewModel)[] views)
  {
    foreach (var (view, viewModel) in views)
    {
      // 문자열로 이름을 자동 생성하거나 명시해야 함
      var viewName = view.Name;

      // 이건 Prism에 정의된 유효한 메서드
      containerRegistry.RegisterForNavigation(view, viewName);
    }
  }
}
