using System.ComponentModel;

namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// 다양한 종류의 아이콘을 나타내는 열거형(Enum)입니다.
/// 각 아이콘은 Geometry 타입으로 표현되는 벡터 그래픽 형태로, UI 요소에 시각적 아이콘으로 사용됩니다.
/// </summary>
public enum IconType
{
  /// <summary>
  /// 사용자 계정 아이콘
  /// </summary>
  [Description("")]
  Account,

  /// <summary>
  /// 박스 형태의 계정 아이콘
  /// </summary>
  [Description("박스형태계정")] AccountBox,

  /// <summary>
  /// 원형 테두리의 계정 아이콘
  /// </summary>
  [Description("운형형태계정")] AccountCircle,

  /// <summary>
  /// 그룹 사용자 아이콘
  /// </summary>
  [Description("그룹계정")] AccountGroup,

  /// <summary>
  /// 다중 사용자 윤곽선 아이콘
  /// </summary>
  [Description("다중계정윤곽선")] AccountMultipleOutline,

  /// <summary>
  /// 애플 로고 아이콘
  /// </summary>
  [Description("애플")] Apple,

  /// <summary>
  /// 사방 화살표 아이콘
  /// </summary>
  [Description("사방화살표")] ArrowAll,

  /// <summary>
  /// 박스 안 아래 화살표 아이콘
  /// </summary>
  [Description("박스안아래화살표")] ArrowDownBox,

  /// <summary>
  /// 왼쪽 화살표 아이콘
  /// </summary>
  [Description("왼쪽화살표")] ArrowLeft,

  /// <summary>
  /// 굵은 왼쪽 화살표 아이콘
  /// </summary>
  [Description("굵은왼쪽화살표")] ArrowLeftBold,

  /// <summary>
  /// 얇은 왼쪽 화살표 아이콘
  /// </summary>
  [Description("얇은왼쪽화살표")] ArrowLeftThin,

  /// <summary>
  /// 오른쪽 화살표 아이콘
  /// </summary>
  [Description("오른쪽화살표")] ArrowRight,

  /// <summary>
  /// 굵은 오른쪽 화살표 아이콘
  /// </summary>
  [Description("굵은오른쪽화살표")] ArrowRightBold,

  /// <summary>
  /// 얇은 오른쪽 화살표 아이콘
  /// </summary>
  [Description("얇은오른쪽화살표")] ArrowRightThin,

  /// <summary>
  /// 굵은 위쪽 화살표 아이콘
  /// </summary>
  [Description("굵은위쪽화살표")] ArrowUpBold,

  /// <summary>
  /// 알림 벨 윤곽선 아이콘
  /// </summary>
  [Description("알림벨윤곽선")] ellOutline,

  /// <summary>
  /// 방송 아이콘
  /// </summary>
  [Description("방송아이콘")] Broadcast,

  /// <summary>
  /// 벌레(버그) 아이콘
  /// </summary>
  [Description("벌레")] Bug,

  /// <summary>
  /// 버튼 커서 아이콘
  /// </summary>
  [Description("버튼커서")] ButtonCursor,

  /// <summary>
  /// 빈 달력 윤곽선 아이콘
  /// </summary>
  [Description("빈달력윤곽선")] CalendarBlankOutline,

  /// <summary>
  /// 달력 월 아이콘
  /// </summary>
  [Description("달력 월")] CalendarMonth,

  /// <summary>
  /// 다중 카드 아이콘
  /// </summary>
  [Description("다중카드")] CardMultiple,

  /// <summary>
  /// 다중 카드 윤곽선 아이콘
  /// </summary>
  [Description("다중카드윤곽선")] CardMultipleOutline,

  /// <summary>
  /// 여러 카드 모양의 윤곽선 아이콘
  /// </summary>
  [Description("여러카드모양")] Cardsplayingspademultipleoutline,

  /// <summary>
  /// 카드 무늬 클럽(♣) 아이콘
  /// </summary>
  [Description("클로바카드")] CardSuitClub,

  /// <summary>
  /// 카드 무늬 다이아몬드(♦) 아이콘
  /// </summary>
  [Description("다이아몬드카드")] CardSuitDiamond,

  /// <summary>
  /// 카드 무늬 하트(♥) 아이콘
  /// </summary>
  [Description("하트카드")] CardSuitHeart,

  /// <summary>
  /// 카드 무늬 스페이드(♠) 아이콘
  /// </summary>
  [Description("스페이드카드")] CardSuitSpade,

  /// <summary>
  /// 현금 아이콘
  /// </summary>
  [Description("현금")] Cash,

  /// <summary>
  /// 100달러 현금 아이콘
  /// </summary>
  [Description("백달러현금")] Cash100,

  /// <summary>
  /// 여러 현금 아이콘
  /// </summary>
  [Description("현금여러")] CashMulti,

  /// <summary>
  /// 버블 차트 아이콘
  /// </summary>
  [Description("챠트거품")] ChartBubble,

  /// <summary>
  /// 파이 차트 아이콘
  /// </summary>
  [Description("챠트파이")] ChartPie,

  /// <summary>
  /// 체크 표시 아이콘
  /// </summary>
  [Description("체쿠")] Check,

  /// <summary>
  /// 굵은 체크 표시 아이콘
  /// </summary>
  [Description("체크굵은")] Checkbold,

  /// <summary>
  /// 원형 체크 아이콘
  /// </summary>
  [Description("체크원형")] CheckCircle,

  /// <summary>
  /// 데카그램(10각형) 체크 아이콘
  /// </summary>
  [Description("체크")] CheckDecagram,

  /// <summary>
  /// 아래쪽 꺾쇠 아이콘
  /// </summary>
  [Description("아래쪽꺽쇠")] ChevronDown,

  /// <summary>
  /// 오른쪽 꺾쇠 아이콘
  /// </summary>
  [Description("오른쪽꺽쇠")] ChevronRight,

  /// <summary>
  /// 클립보드 체크 아이콘
  /// </summary>
  [Description("클립보드와체크")] ClipboardCheck,

  /// <summary>
  /// 클립보드와 시계 아이콘
  /// </summary>
  [Description("클립보드와시계")] ClipboardTextClock,

  /// <summary>
  /// 닫기(X) 아이콘
  /// </summary>
  [Description("닫기")] Close,

  /// <summary>
  /// 구름 아이콘
  /// </summary>
  [Description("구름")] Cloud,

  /// <summary>
  /// 톱니바퀴 아이콘
  /// </summary>
  [Description("톱니바퀴")] Cog,

  /// <summary>
  /// 톱니바퀴 윤곽선 아이콘
  /// </summary>
  [Description("톱니바퀴 윤곽선")] CogOutline,

  /// <summary>
  /// 새로고침 톱니바퀴 윤곽선 아이콘
  /// </summary>
  [Description("새로고침 톱니바퀴")] CogRefreshOutline,

  /// <summary>
  /// 댓글 아이콘
  /// </summary>
  [Description("댓글")] Comment,

  /// <summary>
  /// 댓글 윤곽선 아이콘
  /// </summary>
  [Description("댓글윤곽선")] CommentOutline,

  /// <summary>
  /// 콘솔 라인 아이콘
  /// </summary>
  [Description("콘솔라인")] ConsoleLine,

  /// <summary>
  /// 클립보드 붙여넣기 아이콘
  /// </summary>
  [Description("클립보드븉여넣기")] Contentpaste,

  /// <summary>
  /// 신용카드 칩 아이콘
  /// </summary>
  [Description("신용카드")] Creditcardchip,

  /// <summary>
  /// 신용카드 칩 윤곽선 아이콘
  /// </summary>
  [Description("신용카드윤곽선")] CreditcardchipOutline,

  /// <summary>
  /// 자르기 아이콘
  /// </summary>
  [Description("자르기")] Crop,

  /// <summary>
  /// 커서 포인터 아이콘
  /// </summary>
  [Description("커서포인트")] CursorPointer,

  /// <summary>
  /// 데이터베이스 아이콘
  /// </summary>
  [Description("데이터플러스")] Database,

  /// <summary>
  /// 데이터베이스 플러스 아이콘
  /// </summary>
  [Description("데이터베이스플러스")] DatabasePlus,

  /// <summary>
  /// 삭제 아이콘
  /// </summary>
  [Description("삭제")] Delete,

  /// <summary>
  /// 빈 삭제 아이콘
  /// </summary>
  [Description("빈삭제")] DeleteEmpty,

  /// <summary>
  /// 클래식 데스크탑 아이콘
  /// </summary>
  [Description("클래식 데크스탑")] DesktopClassic,

  /// <summary>
  /// 도메인 아이콘
  /// </summary>
  [Description("도메인")] Domain,

  /// <summary>
  /// 할인 아이콘
  /// </summary>
  [Description("할인")] Discount,

  /// <summary>
  /// 수평 점 3개 아이콘
  /// </summary>
  [Description("수평점 3개")] DotsHorizontal,

  /// <summary>
  /// 원 안의 수평 점 3개 아이콘
  /// </summary>
  [Description("원 안의 수평점 3개")] DotsHorizontalCircle,

  /// <summary>
  /// 이메일 아이콘
  /// </summary>
  [Description("이메일")] Email,

  /// <summary>
  /// 이메일 윤곽선 아이콘
  /// </summary>
  [Description("이메일윤곽선")] EmailOutline,

  /// <summary>
  /// 내보내기 아이콘
  /// </summary>
  [Description("내보내기")] Export,

  /// <summary>
  /// 눈 모양 원 아이콘
  /// </summary>
  [Description("눈모양")] EyeCircle,

  /// <summary>
  /// 스포이드 아이콘
  /// </summary>
  [Description("스포이드")] EyedropperVariant,

  /// <summary>
  /// 페이스북 로고 아이콘
  /// </summary>
  [Description("페이스북")] Facebook,

  /// <summary>
  /// 파일 아이콘
  /// </summary>
  [Description("파일")] File,

  /// <summary>
  /// 체크된 파일 아이콘
  /// </summary>
  [Description("파일체크")] FileCheck,

  /// <summary>
  /// 이미지 파일 아이콘
  /// </summary>
  [Description("파일이미지")] FileImage,

  /// <summary>
  /// PDF 파일 아이콘
  /// </summary>
  [Description("PDF 파일")] FilePdf,

  /// <summary>
  /// 워드 파일 아이콘
  /// </summary>
  [Description("워드파일")] FileWord,

  /// <summary>
  /// 압축 파일 아이콘
  /// </summary>
  [Description("압축파일")] FileZip,

  /// <summary>
  /// 필터 아이콘
  /// </summary>
  [Description("필터변형")] FilterVariant,

  /// <summary>
  /// 폴더 아이콘
  /// </summary>
  [Description("폴더")] Folder,

  /// <summary>
  /// 열린 폴더 아이콘
  /// </summary>
  [Description("폴더열린")] FolderOpen,

  /// <summary>
  /// 열린 폴더 윤곽선 아이콘
  /// </summary>
  [Description("폴더열린윤곽선")] FolderOpenOutline,

  /// <summary>
  /// 폴더 윤곽선 아이콘
  /// </summary>
  [Description("폴더윤곽선")] FolderOutline,

  /// <summary>
  /// (오타로 보임, 폴더 관련 아이콘으로 추정)
  /// </summary>
  [Description("폴더")] FolderRable,

  /// <summary>
  /// 글머리 기호 목록 아이콘
  /// </summary>
  [Description("글머리기호목록")] FormatListBulleted,

  /// <summary>
  /// 구글 로고 아이콘
  /// </summary>
  [Description("구글")] Google,

  /// <summary>
  /// 구글 번역 아이콘
  /// </summary>
  [Description("구글번역")] GoogleTranslate,

  /// <summary>
  /// 그리드 아이콘
  /// </summary>
  [Description("그리드")] Grid,

  /// <summary>
  /// 하드디스크 아이콘
  /// </summary>
  [Description("하드디스크")] Harddisk,

  /// <summary>
  /// 하트 아이콘
  /// </summary>
  [Description("하트")] Heart,

  /// <summary>
  /// 하트 윤곽선 아이콘
  /// </summary>
  [Description("하트 윤곽선")]
  HeartOutline,

  /// <summary>
  /// 히스토리 아이콘
  /// </summary>
  [Description("역사")]
  History,

  /// <summary>
  /// 집 아이콘
  /// </summary>
  [Description("Home")] Home,

  /// <summary>
  /// 원형 테두리 집 아이콘
  /// </summary>
  [Description("Home 운형")] HomeCircle,

  /// <summary>
  /// 원형 윤곽선 집 아이콘
  /// </summary>
  [Description("Home 원형 윤곽선")] HomeCircleOutline,

  /// <summary>
  /// 집 윤곽선 아이콘
  /// </summary>
  [Description("Home 윤곽선")] HomeOutline,

  /// <summary>
  /// 이미지 아이콘
  /// </summary>
  [Description("이미지")] Image,

  /// <summary>
  /// 가져오기 아이콘
  /// </summary>
  [Description("가져오기")] Import,

  /// <summary>
  /// 정보 아이콘
  /// </summary>
  [Description("정보")] Information,

  /// <summary>
  /// 정보 윤곽선 아이콘
  /// </summary>
  [Description("정보윤곽선")] InformationOutline,

  /// <summary>
  /// 인스타그램 로고 아이콘
  /// </summary>
  [Description("인스타그램")]
  Instagram,

  /// <summary>
  /// 백스페이스 키보드 아이콘
  /// </summary>
  [Description("백스페이스")]
  KeyboardBackspace,

  /// <summary>
  /// 링크 아이콘
  /// </summary>
  [Description("Link")]
  Link,

  /// <summary>
  /// 박스 안 링크 아이콘
  /// </summary>
  [Description("Link Box")]
  LinkBox,

  /// <summary>
  /// 링크드인 로고 아이콘
  /// </summary>
  [Description("Link In")]
  Linkedin,

  /// <summary>
  /// 링크 변형 아이콘
  /// </summary>
  [Description("Link 변형")]
  LinkVariant,

  /// <summary>
  /// 확대경 아이콘
  /// </summary>
  [Description("확대경")]
  Magnify,

  /// <summary>
  /// 지도 제작자 아이콘
  /// </summary>
  [Description("지도제작")]
  MapMaker,

  /// <summary>
  /// 지도 마커 윤곽선 아이콘
  /// </summary>
  [Description("지도마커윤곽선")]
  MapMarkerOutline,

  /// <summary>
  /// 최대화 아이콘
  /// </summary>
  [Description("최대화")]
  Maximize,

  /// <summary>
  /// 메모리 아이콘
  /// </summary>
  [Description("메모리")]
  Memory,

  /// <summary>
  /// 메뉴 바 아이콘
  /// </summary>
  [Description("메뉴 BAR")]
  MenuBar,

  /// <summary>
  /// 메뉴 아래쪽 화살표 아이콘
  /// </summary>
  [Description("메뉴아래쪽화살표")]
  MenuDown,

  /// <summary>
  /// 메뉴 위쪽 화살표 아이콘
  /// </summary>
  [Description("메뉴왼쪽화살표")]
  MenuUp,

  /// <summary>
  /// 마이크로소프트 로고 아이콘
  /// </summary>
  [Description("마이크로소프트")]
  Microsoft,

  /// <summary>
  /// 마이크로소프트 비주얼 스튜디오 아이콘
  /// </summary>
  [Description("마이크로소프트비주얼스튜디오")]
  MicrosoftVisualStudio,

  /// <summary>
  /// 마이크로소프트 윈도우 아이콘
  /// </summary>
  [Description("마이크로소프트윈도우")]
  MicrosoftWindows,

  /// <summary>
  /// 최소화 아이콘
  /// </summary>
  [Description("최소화")]
  Minimize,

  /// <summary>
  /// 모니터 반짝임 아이콘
  /// </summary>
  [Description("반짝이는 모니터")]
  MonitorShimmer,

  /// <summary>
  /// 열린 플레이 아이콘
  /// </summary>
  [Description("열린 플레이")]
  MoveOpenPlay,

  /// <summary>
  /// 넷플릭스 로고 아이콘
  /// </summary>
  [Description("넷플릿스")]
  Netflix,

  /// <summary>
  /// 아이콘 없음
  /// </summary>
  [Description("없음")]
  None,

  /// <summary>
  /// 팔레트 아이콘
  /// </summary>
  [Description("팔렛트")]
  Palette,

  /// <summary>
  /// 핀 아이콘
  /// </summary>
  [Description("핀")]
  Pin,

  /// <summary>
  /// 더하기(플러스) 아이콘
  /// </summary>
  [Description("더하기")]
  Plus,

  /// <summary>
  /// 박스 안 플러스 아이콘
  /// </summary>
  [Description("플러스 박스")]
  PlusBox,

  /// <summary>
  /// 포커 칩 아이콘
  /// </summary>
  [Description("포커 칩")]
  PokerChip,

  /// <summary>
  /// 전원 아이콘
  /// </summary>
  [Description("전원")]
  Power,

  /// <summary>
  /// 크기 조절 아이콘
  /// </summary>
  [Description("크기조절")]
  Resize,

  /// <summary>
  /// 복원 아이콘
  /// </summary>
  [Description("복원")]
  Restore,

  /// <summary>
  /// 자 아이콘
  /// </summary>
  [Description("자")]
  Ruler,

  /// <summary>
  /// 보안 아이콘
  /// </summary>
  [Description("보안")]
  Security,

  /// <summary>
  /// 모두 선택 아이콘
  /// </summary>
  [Description("모두선택")]
  SelectAll,

  /// <summary>
  /// 자물쇠가 잠긴 방패 아이콘
  /// </summary>
  [Description("자물쇠가 잠긴 방패")]
  ShieldLock,

  /// <summary>
  /// 다음 스킵 아이콘
  /// </summary>
  [Description("다음 Skip")]
  SkipNext,

  /// <summary>
  /// 이전 스킵 아이콘
  /// </summary>
  [Description("이전 Skip")]
  SkipPrevious,

  /// <summary>
  /// 별 아이콘
  /// </summary>
  [Description("별")]
  Star,

  /// <summary>
  /// 수평 교체 아이콘
  /// </summary>
  [Description("수평교체")]
  SwapHorizontal,

  /// <summary>
  /// 텍스트 박스 아이콘
  /// </summary>
  [Description("Text Box")]
  TextBox,

  /// <summary>
  /// 시간표 아이콘
  /// </summary>
  [Description("시간표")]
  Timetable,

  /// <summary>
  /// 휴지통 아이콘
  /// </summary>
  [Description("휴지통")]
  Trash,

  /// <summary>
  /// 휴지통 윤곽선 아이콘
  /// </summary>
  [Description("윤곽선 휴지통")]
  TrashOutline,

  /// <summary>
  /// 트위터 로고 아이콘
  /// </summary>
  [Description("트위터")]
  Twitter,

  /// <summary>
  /// 일정 보기 아이콘
  /// </summary>
  [Description("보기 Agenda")]
  ViewAgenda,

  /// <summary>
  /// 열 보기 아이콘
  /// </summary>
  [Description("열 보기")]
  ViewColumn,

  /// <summary>
  /// 컴팩트 보기 아이콘
  /// </summary>
  [Description("치밀한 View")]
  ViewCompact,

  /// <summary>
  /// 그리드 보기 아이콘
  /// </summary>
  [Description("Gird 보기")]
  ViewGrid,

  /// <summary>
  /// 야간 날씨 아이콘
  /// </summary>
  [Description("야간날씨")]
  WeatherNight,

  /// <summary>
  /// 웹 아이콘
  /// </summary>
  [Description("웹")]
  Web,

  /// <summary>
  /// 햇빛 아이콘
  /// </summary>
  [Description("햇빛")]
  WhiteBalanceSunny,

  /// <summary>
  /// 유튜브 로고 아이콘
  /// </summary>
  [Description("YouTube")]
  YouTube
}


