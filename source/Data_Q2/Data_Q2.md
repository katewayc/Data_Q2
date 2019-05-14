# 資料處理與呈現

建立一個 ASP.NET MVC 專案，顯示資料計算結果


## 基本

- 資料來源為 data1.json ~ data3.json

- 執行 http:{host}/Dashboard，隨機呈現 data1 ~ data3 其中一個資料統計結果

- 呈現統計資料欄位

    - Ticket - Ticket 數量總和
    - AvgResponseMinutes - 平均回應時間(分鐘), 取到整數再依符合條件轉換成以下格式
    
        - Day - 天，取最小整數 ex: 25 hr -> 1 day 
        - Hour - 小時，需要包含小數點 ex: 90 min = 1.5 hr
        - Minute - 分鐘

- UI 樣式不拘，可以自己設計
    

## 改良

- 無