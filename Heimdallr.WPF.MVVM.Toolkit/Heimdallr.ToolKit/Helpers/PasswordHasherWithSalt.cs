using System.Security.Cryptography;
using System.Text;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// Salt를 사용하여 비밀번호를 해시 처리하는 클래스 구현체
/// IPasswordHasher 인터페이스를 구현합니다.
/// </summary>
public class PasswordHasherWithSalt : IPasswordHasher
{
  // SHA256 해시 알고리즘 객체 생성 (인스턴스 변수로 재사용)
  private readonly SHA256 _sha256 = SHA256.Create();

  // 16진수 변환 시 사용할 포맷 문자열
  private const string HexFormat = "x2";

  /// <summary>
  /// 지정한 길이만큼 랜덤 Salt를 생성합니다.
  /// </summary>
  /// <param name="length">생성할 Salt 길이 (기본값 16바이트)</param>
  /// <returns>Base64 인코딩된 Salt 문자열</returns>
  public string GenerateSalt(int length = 16)
  {
    // length 크기의 바이트 배열을 생성
    byte[] saltBytes = new byte[length];

    // .NET 권장 랜덤 생성기 사용
    using var rng = RandomNumberGenerator.Create();

    // 바이트 배열에 랜덤 값 채우기
    rng.GetBytes(saltBytes);

    // 바이트 배열을 Base64 문자열로 변환 후 반환
    return Convert.ToBase64String(saltBytes);
  }

  /// <summary>
  /// 비밀번호와 Salt를 결합하여 SHA256 해시를 계산합니다.
  /// </summary>
  /// <param name="password">사용자가 입력한 비밀번호</param>
  /// <param name="salt">해시에 사용할 Salt 값</param>
  /// <returns>16진수 문자열로 표현된 해시값</returns>
  /// <exception cref="ArgumentException">비밀번호가 null 또는 빈 값인 경우 발생</exception>
  public string HashPasswordWithSalt(string password, string salt)
  {
    // 비밀번호가 없으면 예외 처리
    if (string.IsNullOrEmpty(password))
      throw new ArgumentException("비밀번호는 null이거나 빈 값일 수 없습니다.", nameof(password));

    // Salt가 없으면 새로 생성
    if (string.IsNullOrEmpty(salt))
      salt = GenerateSalt();

    // Salt와 비밀번호를 UTF8 바이트 배열로 변환
    byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

    // Salt와 비밀번호 바이트 배열을 순서대로 합침
    byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();

    // SHA256 해시 계산
    byte[] hash = _sha256.ComputeHash(combinedBytes);

    // 바이트 배열 해시를 16진수 문자열로 변환하여 반환
    return ConvertToHex(hash);
  }

  /// <summary>
  /// 입력된 비밀번호가 저장된 해시값과 일치하는지 검증합니다.
  /// </summary>
  /// <param name="enteredPassword">사용자가 입력한 비밀번호</param>
  /// <param name="storedHash">데이터베이스 등에 저장된 해시된 비밀번호</param>
  /// <param name="salt">해시 생성 시 사용된 Salt</param>
  /// <returns>비밀번호가 일치하면 true, 그렇지 않으면 false</returns>
  public bool VerifyPasswordWithSalt(string enteredPassword, string storedHash, string salt)
  {
    // 입력된 비밀번호를 같은 방식으로 해시 처리
    var hashOfEnteredPassword = HashPasswordWithSalt(enteredPassword, salt);

    // 해시값을 대소문자 구분 없이 비교해서 결과 반환
    return StringComparer.OrdinalIgnoreCase.Compare(hashOfEnteredPassword, storedHash) == 0;
  }

  /// <summary>
  /// 바이트 배열 해시 값을 16진수 문자열로 변환하는 헬퍼 메서드
  /// </summary>
  /// <param name="hash">해시 바이트 배열</param>
  /// <returns>16진수 문자열</returns>
  private string ConvertToHex(byte[] hash)
  {
    // 결과를 효율적으로 문자열로 결합할 StringBuilder 생성
    var builder = new StringBuilder();

    // 해시 배열의 각 바이트를 16진수 형식으로 변환해 추가
    foreach (var b in hash)
      builder.Append(b.ToString(HexFormat));

    // 최종 16진수 문자열 반환
    return builder.ToString();
  }
}