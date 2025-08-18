namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// 비밀번호 해시 처리 관련 기능을 제공하는 인터페이스
/// </summary>
public interface IPasswordHasher
{
  /// <summary>
  /// 지정된 길이만큼 랜덤 Salt 값을 생성합니다.
  /// </summary>
  /// <param name="length">생성할 Salt의 바이트 길이 (기본값: 16)</param>
  /// <returns>Base64 인코딩된 랜덤 Salt 문자열</returns>
  string GenerateSalt(int length = 16);

  /// <summary>
  /// 비밀번호와 Salt를 결합하여 해시된 비밀번호를 생성합니다.
  /// </summary>
  /// <param name="password">사용자 입력 비밀번호</param>
  /// <param name="salt">비밀번호 해시에 사용할 Salt 값</param>
  /// <returns>16진수 문자열 형태의 해시된 비밀번호</returns>
  string HashPasswordWithSalt(string password, string salt);

  /// <summary>
  /// 입력된 비밀번호와 저장된 해시값, Salt를 비교하여 일치 여부를 검증합니다.
  /// </summary>
  /// <param name="enteredPassword">사용자가 입력한 비밀번호</param>
  /// <param name="storedHash">저장된 해시된 비밀번호</param>
  /// <param name="salt">저장된 Salt 값</param>
  /// <returns>비밀번호가 일치하면 true, 그렇지 않으면 false</returns>
  bool VerifyPasswordWithSalt(string enteredPassword, string storedHash, string salt);
}
