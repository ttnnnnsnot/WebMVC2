測試及學習專案
--.NET Core 8.0 MVC專案
--資料庫MSSQL 2019利用Docker架設
(ps.頁面流程及資料處理方式只是為了測試及學習)

1、使用者登入利用不透過 ASP.NET Core Identity 使用 cookie 驗證
  1-1、設定數據保護的密鑰環持久化
  1-2、AddAuthentication可針對登入者的相關設定
  1-3、可使用AddPolicy針對角色權限進行群組化設定

2、表單驗証利用validator.unobtrusive，來達到前後端驗証
  2-1、ValidationAttribute建立擴充驗証機制來達到自訂驗証
  2-2、前端再加入$.validator.addMethod來達到即時自訂驗証
  2-3、待加入AntiforgeryOptions「防止 ASP.NET 核心中的跨網站偽造要求 (XSRF/CSRF) 攻擊」

3、多檔案上傳利用ValidationAttribute驗証機制自訂需要限制的格式及檔案大小
  3-1、FormOptions可限制表單提交的資料大小限制及提交表單超過多少後由記憶體轉至使用暫存檔案機制
  
4、頁數、loading、alert msg、使用partial來達到共用目的，日後若有複雜邏輯的共用頁面時，可使用「ViewComponent」的方式

5、使用Serilog來記錄網站的錯誤訊息方便除錯，記錄在檔案方便查詢，並設定記錄7天

6、頁面「加入購物車」利用寫入cookie來達到不需進入資料庫而暫存的訊息
  6-1、結帳列表，設定需登入
  6-2、頁面操作上利用ajax來進行新增、數量修改、刪除
  6-3、結帳時利用資料庫的「使用者資料定義型別」來達到stored procedure可進行批次寫入的方式

版面設置使用bootstrap來建置


