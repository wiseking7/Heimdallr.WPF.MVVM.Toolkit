namespace Heimdallr.ToolKit.Animations.EasingFunctions;

/// <summary>
/// 이징 함수(easing function) 의 다양한 모드를 정의하는데 사용
/// 이징 함수는 애니메이션의 진행 속도를 조절하는 수학적 함수로, 
/// 애니메이션이 어떻게 시작하고 끝나는지, 또는 애니메이션이 중간에 어떻게 진행되는지를 결정하는 중요한 역할을 합니다
/// </summary>
public enum EasingFunctionBaseMode
{
  BackEaseIn,
  BackEaseOut,
  BackEaseInOut,
  BounceEaseIn,
  BounceEaseOut,
  BounceEaseInOut,
  CircleEaseIn,
  CircleEaseOut,
  CircleEaseInOut,
  CubicEaseIn,
  CubicEaseOut,
  CubicEaseInOut,
  ElasticEaseIn,
  ElasticEaseOut,
  ElasticEaseInOut,
  ExponentialEaseIn,
  ExponentialEaseOut,
  ExponentialEaseInOut,
  PowerEaseIn,
  PowerEaseOut,
  PowerEaseInOut,
  QuadraticEaseIn,
  QuadraticEaseOut,
  QuadraticEaseInOut,
  QuarticEaseIn,
  QuarticEaseOut,
  QuarticEaseInOut,
  QuinticEaseIn,
  QuinticEaseOut,
  QuinticEaseInOut,
  SineEaseIn,
  SineEaseOut,
  SineEaseInOut
}
/* 
1. Easing Function이란?
   WPF 애니메이션에서 이징 함수는 애니메이션의 진행 방식, 즉 애니메이션의 속도를 어떻게 변화시킬지 제어합니다. 
   기본적으로 애니메이션은 시작과 끝이 일정한 속도로 진행되지만, 이징 함수를 사용하면 더 자연스럽고, 
   부드러운 애니메이션 효과를 만들 수 있습니다.

예를 들어:

  EaseIn: 애니메이션이 처음에 천천히 시작하고 점점 빨라지는 효과를 제공합니다.
  EaseOut: 애니메이션이 빠르게 시작하고 점점 느려지는 효과를 제공합니다.
  EaseInOut: 애니메이션이 처음과 끝은 천천히 시작하고, 중간은 빠르게 진행되는 효과를 제공합니다.

이러한 이징 함수들은 애니메이션의 시각적 효과를 자연스럽게 보이도록 만드는 중요한 도구입니다.

2. EasingFunctionBaseMode 열거형이 필요한 이유
   EasingFunctionBaseMode 열거형은 이징 함수의 다양한 모드를 정의하기 위해 사용됩니다. 
   열거형을 사용하는 이유는 코드의 가독성과 유연성을 높이고, 잘못된 값을 방지할 수 있도록 돕기 위함입니다.

장점 1: 코드 가독성 향상
        EasingFunctionBaseMode 열거형을 사용하면 코드에서 이징 함수 모드를 명확하게 참조할 수 있습니다. 
        예를 들어, EasingFunctionBaseMode.CubicEaseInOut은 이징 함수 모드를 명확하게 지정하므로, 
        이 값이 어떤 것을 의미하는지 직관적으로 알 수 있습니다.

만약 문자열을 사용한다면, 예를 들어 "CubicEaseInOut"이라고 설정하는 것보다 
EasingFunctionBaseMode.CubicEaseInOut이라고 설정하는 것이 더 명확하고, 실수할 확률도 적습니다.

장점 2: 타입 안정성(Type Safety)
        열거형을 사용하면 값이 제한적이고 미리 정의되어 있기 때문에, 
        코드에서 유효하지 않은 값을 사용하려 할 때 컴파일 타임에 오류가 발생합니다. 
        예를 들어, 문자열을 사용했다면 오타나 잘못된 값이 들어갈 수 있지만, 열거형을 사용하면 그런 실수를 방지할 수 있습니다.

EasingFunctionBaseMode.CubicEaseInOut과 같은 값만 허용되며, 다른 값은 컴파일 오류를 발생시킵니다. 이는 애니메이션에 적용할 수 없는 잘못된 값을 방지하는 데 유용합니다.

장점 3: 확장성
        이징 함수 모드를 열거형으로 정의함으로써 새로운 이징 함수가 추가될 때, 
        열거형에 새로운 항목을 추가하면 됩니다. 예를 들어, 
        새로운 이징 함수 모드가 필요하면 EasingFunctionBaseMode 열거형에 NewEasingFunction 같은 값을 추가할 수 있습니다.

이를 통해 코드 수정 없이 새로운 기능을 손쉽게 추가할 수 있습니다.

장점 4: 자동 완성 및 디버깅 지원
        Visual Studio와 같은 IDE에서 열거형을 사용하면 코드 자동 완성 기능이 제공되며, 
        이를 통해 개발자가 실수 없이 정확한 값을 입력할 수 있습니다. 또한, 디버깅 시 이징 함수의 이름을 바로 확인할 수 있습니다.

3. EasingFunctionBaseMode 열거형 값 설명
   EasingFunctionBaseMode 열거형은 다양한 이징 함수 모드를 정의합니다. 
   이 모드들은 EasingFunctionBase 클래스를 기반으로 한 여러 가지 애니메이션의 동작 방식을 나타냅니다.

주요 이징 함수 모드:
BackEaseIn: 애니메이션이 처음에 느리게 시작하고, 끝에서 "뒤로 밀리는" 효과를 줍니다.
BackEaseOut: 애니메이션이 처음에 빠르게 시작하고 끝에서 뒤로 밀리는 효과를 제공합니다.
BackEaseInOut: 애니메이션이 천천히 시작하고 끝에서 뒤로 밀리는 효과를 제공합니다.
BounceEaseIn: 애니메이션이 처음에 빠르게 시작하지만, 끝에서 바운스 효과가 발생합니다.
BounceEaseOut: 애니메이션이 시작에서 천천히 시작하고, 끝에서 바운스 효과가 나타납니다.
CubicEaseIn: 애니메이션이 처음에 천천히 시작하고, 중간부터 빨라지는 효과입니다.
CubicEaseOut: 애니메이션이 빠르게 시작하고, 끝에서 천천히 마무리되는 효과입니다.
CubicEaseInOut: 애니메이션이 시작과 끝에서 천천히, 중간에서 빠르게 진행됩니다.
ElasticEaseIn: 애니메이션이 처음에 느리게 시작하고, 끝에서 늘어나는 효과를 줍니다.
ElasticEaseOut: 애니메이션이 시작에서 빠르게 진행되고, 끝에서 늘어나는 효과를 줍니다.
ElasticEaseInOut: 애니메이션이 천천히 시작하고, 중간에서 빨라지고, 끝에서 늘어나는 효과입니다.

이와 같이 EasingFunctionBaseMode 열거형은 다양한 이징 함수의 종류를 포함하고 있어, 
개발자는 필요한 이징 함수 모드를 쉽게 설정하고 사용할 수 있습니다.
 
 */