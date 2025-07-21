using System.Security.Cryptography;
using System.Text;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// Salt를 사용하여 비밀번호 해시 - 비밀번호와 Salt를 결합하여 해시 처리 (복호화 불가)
/// </summary>
public static class PasswordHasherWithSalt
{
  // SHA256 알고리즘 객체를 재사용하기 위해 정적 변수로 선언
  private static readonly SHA256 sha256 = SHA256.Create();

  // 16진수로 변환할 때 사용할 포맷 정의
  private const string HexFormat = "x2";

  /// <summary>
  /// 비밀번호와 Salt를 결합하여 SHA-256 해시 방식으로 해시하는 메서드
  /// </summary>
  /// <param name="password">사용자가 입력한 비밀번호</param>
  /// <param name="salt">고유 Salt 값</param>
  /// <returns>Salt가 결합된 해시된 비밀번호 (16진수 문자열)</returns>
  /// <exception cref="ArgumentException">비밀번호가 null 또는 빈 값인 경우</exception>
  public static string HashPasswordWithSalt(string password, string salt)
  {
    // 비밀번호가 null이거나 빈 값일 경우 예외를 던짐
    if (string.IsNullOrEmpty(password))
    {
      throw new ArgumentException("비밀번호는 null이거나 빈 값일 수 없습니다.", nameof(password));
    }

    // Salt가 빈 값이라면, 새로 Salt를 생성
    if (string.IsNullOrEmpty(salt))
    {
      salt = GenerateSalt(16);  // 예시로 16바이트 길이로 랜덤 Salt 생성
    }

    // Salt와 비밀번호를 결합하여 해시
    byte[] saltBytes = Encoding.UTF8.GetBytes(salt);  // Salt를 바이트 배열로 변환
    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);  // 비밀번호를 바이트 배열로 변환
    byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();  // Salt와 비밀번호를 결합

    // SHA256 해시를 계산하여 바이트 배열로 반환
    byte[] hash = sha256.ComputeHash(combinedBytes);

    // 해시된 값을 16진수 문자열로 변환하여 반환
    return ConvertToHex(hash);  // 16진수 문자열로 변환
  }

  /// <summary>
  /// 주어진 길이만큼 랜덤 Salt를 생성
  /// </summary>
  /// <param name="length">Salt의 길이 (바이트 단위)</param>
  /// <returns>랜덤 Salt 값</returns>
  private static string GenerateSalt(int length)
  {
    byte[] saltBytes = new byte[length];

    // .NET 6 이상에서 권장되는 방식: RandomNumberGenerator를 사용하여 랜덤 Salt 생성
    using (var rng = RandomNumberGenerator.Create())
    {
      rng.GetBytes(saltBytes);  // 랜덤 Salt 생성
    }

    // Base64로 인코딩하여 반환 (Base64로 반환하는 이유: 바이트 배열을 안전하게 저장하기 위해)
    return Convert.ToBase64String(saltBytes);
  }

  /// <summary>
  /// 바이트 배열을 16진수 문자열로 변환하는 보조 메서드
  /// </summary>
  /// <param name="hash">SHA256 해시 값</param>
  /// <returns>16진수 문자열로 변환된 해시 값</returns>
  private static string ConvertToHex(byte[] hash)
  {
    // 문자열을 결합할 StringBuilder 객체 생성
    StringBuilder builder = new StringBuilder();

    // 해시값 배열을 하나씩 반복하면서 각 바이트를 16진수로 변환하여 추가
    foreach (var b in hash)
    {
      // 바이트를 "x2" 포맷을 사용하여 두 자릿수 16진수로 변환
      builder.Append(b.ToString(HexFormat));
    }

    // 최종적으로 16진수 문자열로 결합된 값을 반환
    return builder.ToString();
  }

  /// <summary>
  /// 사용자가 입력한 비밀번호와 저장된 해시값을 비교하여 인증을 검증하는 메서드 (Salt 사용)
  /// </summary>
  /// <param name="enteredPassword">사용자가 입력한 비밀번호</param>
  /// <param name="storedHash">저장된 해시값</param>
  /// <param name="salt">사용한 Salt 값</param>
  /// <returns>비밀번호가 일치하면 true, 그렇지 않으면 false</returns>
  public static bool VerifyPasswordWithSalt(string enteredPassword, string storedHash, string salt)
  {
    // 입력된 비밀번호를 해시하여, 저장된 해시값과 비교하기 위한 준비
    string hashOfEnteredPassword = HashPasswordWithSalt(enteredPassword, salt);

    // 입력한 비밀번호의 해시와 저장된 해시를 비교하여 일치하면 true 반환
    return StringComparer.OrdinalIgnoreCase.Compare(hashOfEnteredPassword, storedHash) == 0;
  }
}