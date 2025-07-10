using System.Security.Cryptography;
using System.Text;

namespace Heimdallr.ToolKit.Helpers;
/// <summary>
/// 비밀번호 해시(암호) 복호화 불가능
/// </summary>
public static class PasswordHasher
{
  // 16진수로 변환할 때 사용할 포맷 정의
  // "x2"는 두 자리 16진수로 표현하겠다는 의미
  private const string HexFormat = "x2";

  // SHA256 알고리즘 객체를 재사용하기 위해 정적 변수로 선언
  // SHA256.Create()를 호출하여 SHA256 객체를 생성하고 이를 재사용
  private static readonly SHA256 sha256 = SHA256.Create();

  /// <summary>
  /// 비밀번호를 SHA-256 해시 방식으로 해시하는 메서드
  /// </summary>
  /// <param name="password"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static string HashPassword(string password)
  {
    // 비밀번호가 null이거나 빈 값일 경우 예외를 던짐
    // 예외 처리를 통해 빈 비밀번호를 처리하고 코드의 안전성을 높임
    if (string.IsNullOrEmpty(password))
    {
      throw new ArgumentException("비밀번호는 null이거나 빈 값일 수 없습니다.", nameof(password));
    }

    // 비밀번호 문자열을 UTF-8 바이트 배열로 변환
    // 비밀번호는 문자열이므로 해시 알고리즘에 입력하기 위해 바이트 배열로 변환해야 함
    byte[] bytes = Encoding.UTF8.GetBytes(password);

    // SHA256 해시를 계산하여 바이트 배열로 반환
    // SHA256.ComputeHash()는 입력된 바이트 배열에 대해 SHA256 해시를 계산함
    byte[] hash = sha256.ComputeHash(bytes);

    // 해시된 값을 16진수 문자열로 변환하여 반환
    // ConvertToHex() 메서드는 바이트 배열을 16진수 문자열로 변환하는 역할
    return ConvertToHex(hash);
  }

  /// <summary>
  /// 16진수 문자열로 변환하는 보조 메서드, 바이트 배열로 되어 있는 해시 값을 16진수 문자열로 변환하는 역할
  /// </summary>
  /// <param name="hash"></param>
  /// <returns></returns>
  private static string ConvertToHex(byte[] hash)
  {
    // 16진수 문자열을 구성할 때 사용할 StringBuilder 객체 생성
    // StringBuilder는 문자열을 효율적으로 결합할 수 있도록 해 주는 클래스
    StringBuilder builder = new StringBuilder();

    // 해시값 배열을 하나씩 반복하면서 각 바이트를 16진수로 변환하여 추가
    foreach (var b in hash)
    {
      // 각 바이트를 16진수로 변환하고, 두 자릿수로 포맷하여 builder에 추가
      // "x2"는 두 자리 16진수로 변환하겠다는 의미
      builder.Append(b.ToString(HexFormat));
    }

    // 최종적으로 16진수 문자열로 결합된 값을 반환
    return builder.ToString();
  }

  /// <summary>
  /// 사용자가 입력한 비밀번호와 저장된 해시값을 비교하여 인증을 검증하는 메서드
  /// </summary>
  /// <param name="enteredPassword"></param>
  /// <param name="storedHash"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static bool VerifyPassword(string enteredPassword, string storedHash)
  {
    // 입력된 비밀번호가 null이거나 빈 값인 경우 예외를 던짐
    // 예외 처리를 통해 빈 비밀번호를 처리하고 코드의 안전성을 높임
    if (string.IsNullOrEmpty(enteredPassword))
    {
      throw new ArgumentException("입력된 비밀번호는 null 이거나 빈 값일 수 없습니다.", nameof(enteredPassword));
    }

    // 입력된 비밀번호를 해시하여, 저장된 해시값과 비교하기 위한 준비
    // HashPassword 메서드를 호출하여 입력된 비밀번호를 해시값으로 변환
    string hashOfEnteredPassword = HashPassword(enteredPassword);

    // 입력한 비밀번호의 해시와 저장된 해시를 비교
    // StringComparer.OrdinalIgnoreCase는 대소문자 구분 없이 문자열을 비교하는 비교자
    // 두 해시값이 같으면 0을 반환하므로, 비교 결과가 0이면 비밀번호가 일치한 것
    return StringComparer.OrdinalIgnoreCase.Compare(hashOfEnteredPassword, storedHash) == 0;
  }
}

