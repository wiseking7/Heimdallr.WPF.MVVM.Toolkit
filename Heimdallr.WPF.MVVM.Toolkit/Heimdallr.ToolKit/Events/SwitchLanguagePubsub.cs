namespace Heimdallr.ToolKit.Events;

/// <summary>
/// SwitchLanguagePubsub 클래스는 PubSubEvent<string>을 상속받아 특정 이벤트를 발행하고 구독하는 기능을 제공합니다. 
/// 이 클래스는 문자열을 전달하는 이벤트를 발행할 수 있도록 합니다.
/// string : 변경 언어
/// </summary>
public class SwitchLanguagePubsub : PubSubEvent<string> { }

/* 예제 
1. 변경언어 이벤트 발생하기 
public class LanguageSwitcher
{
  private readonly IEventAggregator _eventAggregator;

  public LanguageSwitcher(IEventAggregator eventAggregator)
  {
      _eventAggregator = eventAggregator;
  }

  public void SwitchLanguage(string language)
  {
      // 언어 변경 이벤트 발행
      _eventAggregator.GetEvent<SwitchLanguagePubsub>().Publish(language);
  }
}

2. 언어 변경 이벤트 구독하기 
public class LanguageObserver
{
  private readonly IEventAggregator _eventAggregator;

  public LanguageObserver(IEventAggregator eventAggregator)
  {
      _eventAggregator = eventAggregator;

      // 언어 변경 이벤트 구독
      _eventAggregator.GetEvent<SwitchLanguagePubsub>().Subscribe(OnLanguageChanged);
  }

  private void OnLanguageChanged(string newLanguage)
  {
      // 언어가 변경되었을 때 처리할 로직
      Console.WriteLine($"언어가 변경되었습니다: {newLanguage}");
  }
}

3. 클래스 사용 예제
class Program
{
  static void Main(string[] args)
  {
      // Prism의 EventAggregator 초기화
      var eventAggregator = new EventAggregator();

      // LanguageObserver는 언어 변경을 구독
      var languageObserver = new LanguageObserver(eventAggregator);

      // LanguageSwitcher는 언어 변경을 발행
      var languageSwitcher = new LanguageSwitcher(eventAggregator);

      // 언어 변경 발행 (예: 영어로 변경)
      languageSwitcher.SwitchLanguage("English");

      // 언어 변경 발행 (예: 한국어로 변경)
      languageSwitcher.SwitchLanguage("Korean");
  }
}

 */