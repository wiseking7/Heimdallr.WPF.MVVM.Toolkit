using System.ComponentModel;

namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// PathGeometry 기반의 아이콘 타입을 나타내는 열거형(Enum)입니다.
/// 각 값은 특정 UI 아이콘 또는 기능을 표현하는 Path 아이콘을 의미합니다.
/// UI에서 벡터 형태의 아이콘으로 사용하기 적합합니다.
/// </summary>
public enum PathIconType
{
  /// <summary>사용자 계정(Account) 아이콘</summary>
  [Description("사용자 계정 아이콘")]
  Account,

  /// <summary>활동</summary>
  [Description("활동")]
  Active,

  /// <summary>활동</summary>
  [Description("활동A")]
  ActiveA,

  /// <summary>활동</summary>
  [Description("활동B")]
  ActiveB,

  /// <summary>주소(Address) 아이콘</summary>
  [Description("주소")]
  Address,

  /// <summary>조정</summary>
  [Description("조정(수정)")]
  Adjustment,

  /// <summary>분석</summary>
  [Description("분석")]
  Analytics,

  /// <summary>승인</summary>
  [Description("승인")]
  Approval,

  /// <summary>아래방향</summary>
  [Description("아래방향")]
  ArrowDown,

  /// <summary>왼쪽방향</summary>
  [Description("왼쪽방향")]
  ArrowLeft,

  /// <summary>오른쪽방향</summary>
  [Description("오른쪽방향")]
  ArrowRight,

  /// <summary>위방향</summary>
  [Description("위 방향")]
  ArrowUp,

  /// <summary>위방향</summary>
  [Description("왼쪽방향")]
  Arrow_Triangle_Left,

  /// <summary>위방향</summary>
  [Description("왼쪽방향")]
  Arrow_Triangle_Right,

  /// <summary>위방향</summary>
  [Description("위 방향")]
  Arrow_Triangle_Up,

  /// <summary>위방향</summary>
  [Description("아래 방향")]
  Arrow_Triangle_Down,

  /// <summary>아래 방향 화살표 타원(Chevron Down Ellipse) 아이콘</summary>
  [Description("원형 아래방향")]
  Arrow_Ellipse_Down,

  /// <summary>왼쪽 방향 화살표 타원(Chevron Left Ellipse) 아이콘</summary>
  [Description("원형 왼쪽방향")]
  Arrow_Ellipse_Left,

  /// <summary>오른쪽 방향 화살표 타원(Chevron Right Ellipse) 아이콘</summary>
  [Description("원형 오른쪽 방향")]
  Arrow_Ellipse_Rigth,

  /// <summary>위쪽 방향 화살표 타원(Chevron Up Ellipse) 아이콘</summary>
  [Description("원형 위 방향")]
  Arrow_Ellipse_Up,

  /// <summary>아래 방향 화살표 타원(Chevron Down Ellipse) 아이콘</summary>
  [Description("원형 아래방향")]
  Arrow_Ellipse_DownA,

  /// <summary>왼쪽 방향 화살표 타원(Chevron Left Ellipse) 아이콘</summary>
  [Description("원형 왼쪽방향")]
  Arrow_Ellipse_LeftB,

  /// <summary>오른쪽 방향 화살표 타원(Chevron Right Ellipse) 아이콘</summary>
  [Description("원형 오른쪽 방향")]
  Arrow_Ellipse_RigthC,

  /// <summary>위쪽 방향 화살표 타원(Chevron Up Ellipse) 아이콘</summary>
  [Description("원형 위 방향")]
  Arrow_Ellipse_UpD,

  /// <summary>권한</summary>
  [Description("권한")]
  Authority,

  /// <summary>뒤로 가기(Back) 아이콘</summary>
  [Description("뒤로가기")]
  Back,

  /// <summary>백업(Backup) 아이콘</summary>
  [Description("Back up(저장)")]
  Backup,

  /// <summary>은행</summary>
  [Description("은행")]
  Bank,

  /// <summary>바코드(Barcode) 아이콘</summary>
  [Description("바코드")]
  Barcode,

  /// <summary>바코드(Barcode) 아이콘</summary>
  [Description("바코드 A")]
  BarcodeA,

  /// <summary>바코드(Barcode) 아이콘</summary>
  [Description("바코드 B")]
  BarcodeB,

  /// <summary>생일</summary>
  [Description("생일")]
  Birthday,

  /// <summary>브랜드</summary>
  [Description("Brand")]
  Brand,

  /// <summary>브랜드</summary>
  [Description("Brand A")]
  BrandA,

  /// <summary>BrandText</summary>
  [Description("Brand Logo")]
  BrandLogo,

  /// <summary>예산문자</summary>
  [Description("예산")]
  BudgetString,

  /// <summary>구매</summary>
  [Description("구매")]
  Buy,

  /// <summary>구매</summary>
  [Description("구매A")]
  BuyA,

  /// <summary>구매</summary>
  [Description("구매B")]
  BuyB,

  /// <summary>업종</summary>
  [Description("업종")]
  BusinessItem,

  /// <summary>업태</summary>
  [Description("업태")]
  BusinessType,

  /// <summary>캐시(Cache) 아이콘</summary>
  [Description("Cache")]
  Cache,

  /// <summary>계산(Calculation) 아이콘</summary>
  [Description("계산")]
  Calculation,

  /// <summary>달력</summary>
  [Description("달력")]
  Calender,

  /// <summary>취소(Cancel) 아이콘</summary>
  [Description("취소")]
  Cancel,

  /// <summary>장바구니(Cart) 아이콘</summary>
  [Description("장바구니")]
  Cart,

  /// <summary>현금</summary>
  [Description("현금")]
  Cash,

  /// <summary>카테고리(Category) 아이콘</summary>
  [Description("카테고리")]
  Category,

  /// <summary>3D 카테고리(Category 3D) 아이콘</summary>
  [Description("3D 카테고리")]
  Category3D,

  /// <summary>차트(Chart) 아이콘</summary>
  [Description("차트")]
  Chart,

  /// <summary>차트A </summary>
  [Description("차트A")]
  ChartA,

  /// <summary>파이 차트(Pie) 아이콘</summary>
  [Description("차트파이")]
  ChartPie,

  /// <summary>채팅</summary>
  [Description("채팅")]
  Chat,

  /// <summary>도시</summary>
  [Description("도시")]
  City,

  /// <summary>클라이언트 타원(Client Ellipse) 아이콘</summary>
  [Description("클라이언트 원형")]
  ClientEllipse,

  /// <summary>코드</summary>
  [Description("코드")]
  Code,

  /// <summary>코드</summary>
  [Description("코드")]
  CodeA,

  /// <summary>코드</summary>
  [Description("코드")]
  CodeB,

  /// <summary>코드</summary>
  [Description("코드")]
  CodeC,

  /// <summary>코드</summary>
  [Description("코드")]
  CodeD,

  /// <summary>커밋(Commit) 아이콘</summary>
  [Description("커밋")]
  Commit,

  /// <summary>이미지 빌딩</summary>
  [Description("회사")]
  Company,

  /// <summary>계약 타원(Contract Ellipse) 아이콘</summary>
  [Description("게약 원형(문자열)")]
  ContractEllipse,

  /// <summary>계약 타원(Contract Ellipse) 아이콘</summary>
  [Description("게약 원형(문자열)")]
  Contact,

  /// <summary>나라</summary>
  [Description("국가 원형")]
  Country,

  /// <summary>쿠폰</summary>
  [Description("쿠폰")]
  Coupon,

  /// <summary>생성(Create) 아이콘</summary>
  [Description("생성 원형(문자열)")]
  Create,

  /// <summary>생성하기</summary>
  [Description("생성하기")]
  CreateA,

  /// <summary>생성하기</summary>
  [Description("생성하기")]
  CreateB,

  /// <summary>생성하기</summary>
  [Description("생성하기")]
  CreateC,

  /// <summary>생성하기</summary>
  [Description("생성하기")]
  CreateD,

  /// <summary>생성 1(Create1) 아이콘</summary>
  [Description("생성 사각(+)")]
  CreateSquarePlus,

  /// <summary>신용카드</summary>
  [Description("신용카드")]
  CreditCard,

  /// <summary>신용카드</summary>
  [Description("신용카드A")]
  CreditCardA,

  /// <summary>원화표시</summary>
  [Description("원화표시")]
  CurrencyWon,

  /// <summary>고객(Customer) 아이콘</summary>
  [Description("고객")]
  Customer,

  /// <summary>고객 1(Customer1) 아이콘</summary>
  [Description("고객A")]
  CustomerA,

  /// <summary>대시보드(Dashboard) 아이콘</summary>
  [Description("대쉬보드")]
  Dashboard,

  /// <summary>대시보드(Dashboard) 아이콘</summary>
  [Description("대쉬보드A")]
  DashboardA,

  /// <summary>날짜</summary>
  [Description("날짜")]
  Date,

  /// <summary>일</summary>
  [Description("일")]
  Day,

  /// <summary>낮 시간 타원(Day Ellipse) 아이콘</summary>
  [Description("일 원형(문자열)")]
  DayEllipse,

  /// <summary>삭제(Delete) 아이콘</summary>
  [Description("삭제")]
  Delete,

  /// <summary>배송시간</summary>
  [Description("배송")]
  Delivery,

  /// <summary>부서</summary>
  [Description("부서")]
  Department,

  /// <summary>입금</summary>
  [Description("입금")]
  Deposit,

  /// <summary>설명</summary>
  [Description("설명")]
  Description,

  /// <summary>설명</summary>
  [Description("설명A")]
  DescriptionA,

  /// <summary>설명</summary>
  [Description("설명B")]
  DescriptionB,

  /// <summary>세부사항</summary>
  [Description("세부사항")]
  Detail,

  /// <summary>세부사항</summary>
  [Description("세부사항A")]
  DetailA,

  /// <summary>세부사항</summary>
  [Description("세부상항B")]
  DetailB,

  /// <summary>세부사항</summary>
  [Description("세부사항C")]
  DetailC,

  /// <summary>세부사항</summary>
  [Description("세부사항D")]
  DetailD,

  /// <summary>세부사항</summary>
  [Description("세부사항E")]
  DetailE,

  /// <summary>할인(Discount) 아이콘</summary>
  [Description("할인")]
  Discount,

  /// <summary>할인 1(Discount1) 아이콘</summary>
  [Description("할인A")]
  DiscountA,

  /// <summary>할인 1(Discount1) 아이콘</summary>
  [Description("할인B")]
  DiscountB,

  /// <summary>아래방향</summary>
  [Description("아래방향")]
  Down,

  /// <summary>편집</summary>
  [Description("편집")]
  Edit,

  /// <summary>편집(Edit) 아이콘</summary>
  [Description("편집A")]
  EditA,

  /// <summary>편집(Edit) 연필</summary>
  [Description("편집연필")]
  EditB,

  /// <summary>이메일(Email) 아이콘</summary>
  [Description("이메일")]
  Email,

  /// <summary>직원(Employee) 아이콘</summary>
  [Description("직원")]
  Employee,

  /// <summary>직원(Employee) 아이콘</summary>
  [Description("직원A")]
  EmployeeA,

  /// <summary>환경</summary>
  [Description("환경")]
  Environment,

  /// <summary>오류</summary>
  [Description("오류")]
  Error,

  /// <summary>오류</summary>
  [Description("오류")]
  ErrorA,

  /// <summary>엑셀</summary>
  [Description("엑셀")]
  Excel,

  /// <summary>교환(Exchange) 아이콘</summary>
  [Description("환전")]
  Exchange,

  /// <summary>빨리</summary>
  [Description("빨리")]
  Fast,

  /// <summary>즐겨찾기(Favorite) 아이콘</summary>
  [Description("즐겨찾기")]
  Favorite,

  /// <summary>팩스</summary>
  [Description("팩스")]
  FAX,

  /// <summary>파일(File) 아이콘</summary>
  [Description("파일")]
  File,

  /// <summary>자금</summary>
  [Description("자금")]
  Finance,

  /// <summary>폴더(Folder) 아이콘</summary>
  [Description("폴더")]
  Folder,

  /// <summary>남여구분</summary>
  [Description("남여구분")]
  Gender,

  /// <summary>유령(Ghost) 아이콘</summary>
  [Description("유령")]
  Ghost,

  /// <summary>유령 1(Ghost1) 아이콘</summary>
  [Description("유령1")]
  Ghost1,

  /// <summary>선물(Gift) 아이콘</summary>
  [Description("선물")]
  Gift,

  /// <summary>하임달르 로고(Heimdallr Logo) 아이콘</summary>
  [Description("헤일달")]
  HeimdallrLogo,

  /// <summary>HEIMDALLR 문자열(Heimdallr String) 아이콘</summary>
  [Description("헤임달(문자열)")]
  HEIMDALLR_STRING,

  /// <summary>이력</summary>
  [Description("이력")]
  History,

  /// <summary>홈(Home) 아이콘</summary>
  [Description("홈")]
  Home,

  /// <summary>홈(Home) 아이콘</summary>
  [Description("홈A")]
  HomeA,

  /// <summary>가져오기</summary>
  [Description("가져오기")]
  Import,

  /// <summary>수익</summary>
  [Description("수익")]
  Income,

  /// <summary>정보(Information) 아이콘</summary>
  [Description("정보")]
  Information,

  /// <summary>재고(Inventory) 아이콘</summary>
  [Description("재고")]
  Inventory,

  /// <summary>창고형재고</summary>
  [Description("창고형재고")]
  InventoryA,

  /// <summary>청구서(Invoice) 아이콘</summary>
  [Description("청구서")]
  Invoice,

  /// <summary>청구서 리스트</summary>
  [Description("청구서A")]
  InvoiceA,

  /// <summary>청구서 리스트</summary>
  [Description("청구서B")]
  InvoiceB,

  /// <summary>그림</summary>
  [Description("이미지")]
  Image,

  /// <summary>항목피라미드</summary>
  [Description("아이템")]
  Item,

  /// <summary>항목사이트</summary>
  [Description("아이템시트")]
  ItemSite,

  /// <summary>(Key) 아이콘</summary>
  [Description("키")]
  Key,

  /// <summary>라벨(Lable) 아이콘 </summary>
  [Description("라벨")]
  Label,

  /// <summary>성</summary>
  [Description("이름성")]
  LastName,

  /// <summary>왼쪽방향</summary>
  [Description("왼쪽방향")]
  Left,

  /// <summary>목록</summary>
  [Description("목록")]
  List,

  /// <summary>목록</summary>
  [Description("목록A")]
  ListA,

  /// <summary>목록</summary>
  [Description("목록B")]
  ListB,

  /// <summary>목록</summary>
  [Description("목록C")]
  ListC,

  /// <summary>목록</summary>
  [Description("목록D")]
  ListD,

  /// <summary>목록</summary>
  [Description("목록E")]
  ListE,

  /// <summary>목록</summary>
  [Description("목록F")]
  ListF,

  /// <summary>목록</summary>
  [Description("목록G")]
  ListG,

  /// <summary>목록</summary>
  [Description("목록H")]
  ListH,

  /// <summary>목록</summary>
  [Description("목록I")]
  ListI,

  /// <summary>목록</summary>
  [Description("목록J")]
  ListJ,

  /// <summary>잠금</summary>
  [Description("잠금")]
  Lock,

  /// <summary>로그(Log) 아이콘</summary>
  [Description("로그")]
  Log,

  /// <summary>로그인(Login) 아이콘</summary>
  [Description("로그인")]
  Login,

  /// <summary>로그인(Login) 아이콘</summary>
  [Description("로그인")]
  LoginA,

  /// <summary>로그기록</summary>
  [Description("로그기록")]
  LogRecord,

  /// <summary>로그아웃(Logout) 아이콘</summary>
  [Description("로그아웃")]
  Logout,

  /// <summary>로그아웃(Logout) 아이콘</summary>
  [Description("로그아웃")]
  LogoutA,

  /// <summary>로그아웃(Logout) 아이콘</summary>
  [Description("로그아웃")]
  LogoutB,

  /// <summary>관리</summary>
  [Description("관리")]
  Management,

  /// <summary>관리</summary>
  [Description("관리A")]
  ManagementA,

  /// <summary>관리</summary>
  [Description("관리A")]
  ManagementB,

  /// <summary>관리</summary>
  [Description("관리A")]
  ManagementC,

  /// <summary>관리</summary>
  [Description("관리자")]
  Manager,

  /// <summary>메뉴(Menu) 아이콘</summary>
  [Description("메뉴")]
  Menu,

  /// <summary>빼기</summary>
  [Description("마이너스")]
  Minus,

  /// <summary>이동전화</summary>
  [Description("이동전화")]
  MobilePhone,

  /// <summary>돈(Money) 아이콘</summary>
  [Description("돈")]
  Money,

  /// <summary>월별</summary>
  [Description("월")]
  Month,

  /// <summary>월 요약</summary>
  [Description("월요약")]
  MonthSummary,

  /// <summary>탐색</summary>
  [Description("탐색")]
  Navigation,

  /// <summary>탐색</summary>
  [Description("탐색아웃라인")]
  NavigationOutLine,

  /// <summary>아이콘 없음(None)</summary>
  [Description("없음")]
  None,

  /// <summary>알림(Notification) 아이콘</summary>
  [Description("알림")]
  Notification,

  /// <summary>사무실</summary>
  [Description("사무실")]
  Office,

  /// <summary>주문(Order) 아이콘</summary>
  [Description("주문")]
  Order,

  /// <summary>대표</summary>
  [Description("대표")]
  Owner,

  /// <summary>포장단위</summary>
  [Description("포장단위")]
  Packaging,

  /// <summary>포장단위</summary>
  [Description("포장단위A")]
  PackagingA,

  /// <summary>결제완료</summary>
  [Description("결재완료")]
  Paid,

  /// <summary>비밀번호</summary>
  [Description("비밀번호")]
  PasswordLock,

  /// <summary>비밀번호</summary>
  [Description("비밀번호")]
  PasswordKey,

  /// <summary>결제(Payment) 아이콘</summary>
  [Description("결제")]
  Payment,

  /// <summary>PDF</summary>
  [Description("PDF")]
  PDF,

  /// <summary>전화</summary>
  [Description("전화")]
  Phone,

  /// <summary>사진(Photo) 아이콘</summary>
  [Description("사진")]
  Photo,

  /// <summary>Pin</summary>
  [Description("핀")]
  Pin,

  /// <summary>더하기</summary>
  [Description("플러스")]
  Plus,

  /// <summary>점수</summary>
  [Description("포인트")]
  Point,

  /// <summary>포지션</summary>
  [Description("포지션")]
  Position,

  /// <summary>포지션</summary>
  [Description("포지션")]
  PositionA,

  /// <summary>포지션</summary>
  [Description("포지션")]
  PositionB,

  /// <summary>우편번호</summary>
  [Description("우편번호")]
  Postal,

  /// <summary>우편번호</summary>
  [Description("우편번호A")]
  PostalA,

  /// <summary>전원(Power) 아이콘</summary>
  [Description("전원")]
  Power,

  /// <summary>이전(Previous) 아이콘</summary>
  [Description("이전")]
  Previous,

  /// <summary>가격(Price) 아이콘</summary>
  [Description("가격")]
  Price,

  /// <summary>가격 1(Price1) 아이콘</summary>
  [Description("가격1")]
  PriceA,

  /// <summary> 타원(Prince Ellipse) 아이콘</summary>
  [Description("가격 원형")]
  PriceEllipse,

  /// <summary>인쇄(Print) 아이콘</summary>
  [Description("인쇄")]
  Print,

  /// <summary>진행</summary>
  [Description("진행")]
  Processing,

  /// <summary>제품(Product) 아이콘</summary>
  [Description("제품")]
  Product,

  /// <summary>제품 A(Product1) 아이콘</summary>
  [Description("제품A")]
  ProductA,

  /// <summary>제품 B</summary>
  [Description("제품B")]
  ProductB,

  /// <summary>제품 C</summary>
  [Description("제품C")]
  ProductC,

  /// <summary>제품공장</summary>
  [Description("제품공장")]
  ProductFactory,

  /// <summary>제품 생성(Product Create) 아이콘</summary>
  [Description("제품생성")]
  ProductCreate,

  /// <summary>제품 타원(Product Ellipse) 아이콘</summary>
  [Description("제품 원형")]
  ProductEllipse,

  /// <summary>제품 반품(Product Return) 아이콘</summary>
  [Description("제품 반품")]
  ProductReturn,

  /// <summary>제품 반품 타원(Product Return Ellipse) 아이콘</summary>
  [Description("제품반품 원형")]
  ProductReturnEllipse,

  /// <summary>제품 텍스트 타원(Product Text Ellipse) 아이콘</summary>
  [Description("제품 원형(문자열)")]
  ProductTextEllipse,

  /// <summary>제품 경고(Product Warning) 아이콘</summary>
  [Description("제품 경고")]
  ProductWarning,

  /// <summary>구매(Purchase) 아이콘</summary>
  [Description("구매")]
  Purchase,

  /// <summary>구매(Purchase) 아이콘</summary>
  [Description("구매")]
  Purchase_Buy,

  /// <summary>구매(Purchase) 아이콘</summary>
  [Description("구매")]
  Purchase_Ticket,

  /// <summary>Qr코드</summary>
  [Description("QR 코드")]
  QrCode,

  /// <summary>수량</summary>
  [Description("수량")]
  Quantity,

  /// <summary>질문</summary>
  [Description("질문")]
  Question,

  /// <summary>빠른 </summary>
  [Description("빠른")]
  Quick,

  /// <summary>읽기(Read) 아이콘</summary>
  [Description("읽기")]
  Read,

  /// <summary>읽기(Read) 아이콘</summary>
  [Description("읽기")]
  ReadA,

  /// <summary>읽기 글자</summary>
  [Description("Read 글자")]
  ReadText,

  /// <summary>눈 읽기(Read Eyes) 아이콘</summary>
  [Description("읽기 눈모양")]
  ReadEyes,

  /// <summary>영수증(Receipt) 아이콘</summary>
  [Description("영수증")]
  Receipt,

  /// <summary>기록</summary>
  [Description("기록")]
  Record,

  /// <summary>환불</summary>
  [Description("환불")]
  Refund,

  /// <summary>지역(Region) 아이콘</summary>
  [Description("지역")]
  Region,

  /// <summary>등록</summary>
  [Description("등록")]
  Registration,

  /// <summary>요청</summary>
  [Description("요청")]
  Request,

  /// <summary>보고서(Report) 아이콘</summary>
  [Description("보고서")]
  Report,

  /// <summary>보고서 A</summary>
  [Description("보고서A")]
  ReportA,

  /// <summary>반환</summary>
  [Description("반환")]
  Return,

  /// <summary>반품입고</summary>
  [Description("반품입고")]
  ReturnedWarehousing,

  /// <summary>오른쪽방향</summary>
  [Description("오른쪽 방향")]
  Right,

  /// <summary>역할</summary>
  [Description("역할")]
  Role,

  /// <summary>판매(Sale) 아이콘</summary>
  [Description("판매")]
  Sale,

  /// <summary>판매(Sale) 아이콘</summary>
  [Description("판매A")]
  SaleA,

  /// <summary>판매(Sale) 아이콘</summary>
  [Description("판매B")]
  SaleB,

  /// <summary>매출실적</summary>
  [Description("판매실적")]
  SaleRevenue,

  /// <summary>저장(Save) 아이콘</summary>
  [Description("저장")]
  Save,

  /// <summary>검색(Search) 아이콘</summary>
  [Description("검색")]
  Search,

  /// <summary>검색 1(Search1) 아이콘</summary>
  [Description("검색A")]
  SearchA,

  /// <summary>데이터베이스 검색</summary>
  [Description("데이터베이스 검색")]
  SearchDatabase,

  /// <summary>Sell</summary>
  [Description("판매")]
  Sell,

  /// <summary>Sell</summary>
  [Description("판매A")]
  SellA,

  /// <summary>설정(Setting) 아이콘</summary>
  [Description("설정")]
  Setting,

  /// <summary>배송트럭</summary>
  [Description("배송트럭")]
  Shipped,

  /// <summary>배송(Shipping) 아이콘</summary>
  [Description("배송")]
  Shipping,

  /// <summary>구/군/읍/면</summary>
  [Description("구/군")]
  State,

  /// <summary>구/군/읍/면</summary>
  [Description("구/군")]
  StateA,

  /// <summary>통계</summary>
  [Description("통계")]
  Stats,

  /// <summary>상황</summary>
  [Description("상황")]
  Status,

  /// <summary>스태프</summary>
  [Description("스태프")]
  Staff,

  /// <summary>재고</summary>
  [Description("재고")]
  Stock,

  /// <summary>매장(Store) 아이콘</summary>
  [Description("매장")]
  Store,

  /// <summary>매장</summary>
  [Description("매장A")]
  StoreA,

  /// <summary>거리</summary>
  [Description("주소거리")]
  Street,

  /// <summary>제출(저장)</summary>
  [Description("제출")]
  Submit,

  /// <summary>제출(저장)</summary>
  [Description("제출")]
  SubmitA,

  /// <summary>제출(저장)</summary>
  [Description("제출")]
  SubmitB,

  /// <summary>합계</summary>
  [Description("합계")]
  Sum,

  /// <summary>세금(Tax) 아이콘</summary>
  [Description("세금")]
  Tax,

  /// <summary>세금계산서</summary>
  [Description("세금계산서")]
  TaxInvoice,

  /// <summary>세금계산서</summary>
  [Description("세금계산서A")]
  TaxInvoiceA,

  /// <summary>타이틀</summary>
  [Description("타이틀")]
  Title,

  /// <summary>거래</summary>
  [Description("거래")]
  Transaction,

  /// <summary>형식</summary>
  [Description("형식")]
  Type,

  /// <summary>단위</summary>
  [Description("단위")]
  Unit,

  /// <summary>단위</summary>
  [Description("단위A")]
  UnitA,

  /// <summary>위 방향</summary>
  [Description("위 방향")]
  Up,

  /// <summary>업데이트(Update) 아이콘</summary>
  [Description("업데이트")]
  Update,

  /// <summary>업데이트(Update) 아이콘</summary>
  [Description("업데이트A")]
  UpdateA,

  /// <summary>업데이트(Update) 아이콘</summary>
  [Description("업데이트B")]
  UpdateB,

  /// <summary>사용자(User) 아이콘</summary>
  [Description("사용자")]
  User,

  /// <summary>중간판매자</summary>
  [Description("중간판매자")]
  Vendor,

  /// <summary>보기창</summary>
  [Description("View")]
  Views,

  /// <summary>창고(Warehouse) 아이콘</summary>
  [Description("창고")]
  Warehouse,

  /// <summary>경고 아이콘</summary>
  [Description("경고")]
  Warning,

  /// <summary>창 최대화(Window Maximize) 아이콘</summary>
  [Description("윈도우창 최대화")]
  WindowMaximize,

  /// <summary>창 최소화(Window Minimize) 아이콘</summary>
  [Description("윈도우창 최소화")]
  WindowMinimize,

  /// <summary>년</summary>
  [Description("년")]
  Year
}

/*
 1. Revenue (수익)
비즈니스에서 수익을 나타내는 아이콘을 추가할 수 있습니다.

2. Profit (이익)
이익을 나타내는 아이콘도 비즈니스 상 중요합니다.

3. Contract (계약)
계약서, 협정서 등을 상징하는 아이콘이 있을 수 있습니다.

4. Investment (투자)
투자와 관련된 아이콘(예: 주식, 자산, 투자 금액 등)을 추가할 수 있습니다.

5. Expense (비용)
비용을 추적하는 아이콘도 중요합니다.

6. Tax (세금)
세금 관련 아이콘은 비즈니스 운영에 중요한 요소입니다.

7. Loan (대출)
대출 관련 아이콘도 자주 사용됩니다.

8. Budget (예산)
예산을 관리하는 아이콘은 비즈니스 운영에서 핵심적인 역할을 합니다.

9. Audit (감사)
감사를 나타내는 아이콘은 재정 검토 및 회계에서 중요합니다.

10. Transaction (거래)
거래와 관련된 아이콘(예: 송금, 거래 내역)을 추가할 수 있습니다.

11. Meeting (회의)
회의 관련 아이콘은 팀 미팅, 클라이언트 미팅 등을 나타낼 수 있습니다.

12. Report (보고서)
보고서 아이콘은 성과 분석, 전략 보고서 등을 나타냅니다.

13. Strategy (전략)
비즈니스 전략을 나타내는 아이콘도 중요합니다.

14. Risk (위험)
위험 관리 관련 아이콘이 있을 수 있습니다.

15. Client (클라이언트)
클라이언트 관리 관련 아이콘이 추가될 수 있습니다.

16. Partnership (파트너십)
파트너십이나 협력 관계를 나타내는 아이콘도 비즈니스에서 중요합니다.

17. Growth (성장)
비즈니스 성장을 나타내는 아이콘도 추가할 수 있습니다.

18. Supplier (공급업체)
공급망 관리 아이콘도 비즈니스 관련 아이콘에서 중요한 부분을 차지합니다.

19. Stock (주식)
주식 관련 아이콘이 누락된 경우가 있을 수 있습니다.

20. Market (시장)
시장 분석, 마케팅 관련 아이콘이 있을 수 있습니다.

21. Delivery (배송)
물류, 배송과 관련된 아이콘도 비즈니스에서는 흔히 사용됩니다.
 */