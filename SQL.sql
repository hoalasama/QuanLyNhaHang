CREATE DATABASE RESTAURANT;
USE RESTAURANT;

CREATE TABLE CATEGORY(
	cate_id INT IDENTITY primary key,
	cate_name NVARCHAR(100) NOT NULL
);

CREATE TABLE MENU(
	menu_id INT IDENTITY primary key,
	menu_name NVARCHAR(100) NOT NULL,
	/*menu_img VARCHAR(100) NOT NULL,*/
	menu_price float DEFAULT(0) NOT NULL,
	cate_id INT references CATEGORY(cate_id)
);

CREATE TABLE TABLES(
	table_id INT IDENTITY primary key,
	table_name NVARCHAR(100) NOT NULL,
	table_status NVARCHAR(50) NOT NULL DEFAULT N'Trống'
);

CREATE TABLE USERS(
	user_id INT IDENTITY primary key,
	user_name NVARCHAR(50) NOT NULL,
	user_password NVARCHAR(10) NOT NULL DEFAULT 0,
	user_displayname NVARCHAR(50) NOT NULL,
	user_fullname NVARCHAR(100) NOT NULL,
	user_phone VARCHAR(10) NOT NULL,
	user_role INT NOT NULL DEFAULT 0
);

CREATE TABLE BILL(
	bill_id INT IDENTITY primary key,
	bill_checkin_date datetime NOT NULL,
	bill_checkout_date datetime,
	bill_status int DEFAULT(0),
	bill_total float,
	table_id INT references TABLES(table_id),
);

CREATE TABLE BILL_DETAIL(
	bd_id INT IDENTITY primary key,
	bill_id INT NOT NULL references BILL(bill_id),
	menu_id INT NOT NULL references MENU(menu_id),
	quantity INT DEFAULT 0
);

DROP TABLE BILL_DETAIL;
DROP TABLE BILL;
DROP TABLE USERS;
DROP TABLE TABLES;
DROP TABLE MENU;
DROP TABLE CATEGORY;

DELETE BILL_DETAIL;
DELETE BILL;
DELETE USERS;
DELETE TABLES;
DELETE MENU;
DELETE CATEGORY;

SELECT *FROM BILL_DETAIL;
SELECT *FROM BILL;
SELECT *FROM USERS;
SELECT *FROM TABLES;
SELECT *FROM MENU;
SELECT *FROM CATEGORY;

INSERT INTO CATEGORY (cate_name) VALUES (N'Món khai vị');
INSERT INTO CATEGORY (cate_name) VALUES (N'Soup');
INSERT INTO CATEGORY (cate_name) VALUES (N'Món chính');
INSERT INTO CATEGORY (cate_name) VALUES (N'Rau xanh');
INSERT INTO CATEGORY (cate_name) VALUES (N'Nấm');
INSERT INTO CATEGORY (cate_name) VALUES (N'Hải sản');
INSERT INTO CATEGORY (cate_name) VALUES (N'Thịt gia súc');
INSERT INTO CATEGORY (cate_name) VALUES (N'Cơm và bún');
INSERT INTO CATEGORY (cate_name) VALUES (N'Tráng miệng');
INSERT INTO CATEGORY (cate_name) VALUES (N'Đồ uống');

INSERT INTO MENU (menu_name, menu_price, cate_id)
VALUES (N'Gỏi Cuốn', '50000', 1);

INSERT INTO MENU (menu_name, menu_price, cate_id)
VALUES (N'Phở Bò', '80000', 2);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Bún Tôm', 'bun-tom.jpg', '70000', 3);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Rau Cải Xào Tỏi', 'rau-cai-xao-toi.jpg', '30000', 4);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Nấm Kim Châm Trứng', 'nam-kim-cham-trung.jpg', '40000', 5);

INSERT INTO MENU (menu_name, menu_price, cate_id)
VALUES (N'Tôm Hùm Nướng', 'tom-hum-nuong.jpg', '250000', 6);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Bò Lụi', 'bo-lui.jpg', '600000', 7);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Cơm Tấm', 'com-tam.jpg', '300000', 8);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Chè', 'che.jpg', '250000', 9);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Trà Đá', 'tra-da.jpg', '150000', 10);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Nem Rán', 'nem-ran.jpg', '40000', 1);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Bánh Xèo', 'banh-xeo.jpg', '50000', 1);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Há Cảo', 'ha-cao.jpg', '35000', 1);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Kim Chi', 'kim-chi.jpg', '20000', 1);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Súp Bí Đỏ', 'sup-bi-do.jpg', '30000', 2); 

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Cá Kho Tộ', 'ca-kho-to.jpg', '100000', 3);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Gà Rán Mật Ong', 'ga-ran-mat-ong.jpg', '80000', 3);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Mì Quảng', 'mi-quang.jpg', '60000', 3);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Lẩu Cá Hồi', 'lau-ca-hoi.jpg', '200000', 3);

INSERT INTO MENU (menu_name, menu_img, menu_price, cate_id)
VALUES (N'Bún Chả', 'bun-cha.jpg', '70000', 3);

DECLARE @i INT = 1

WHILE @i <= 10
BEGIN 
	INSERT TABLES (table_name) VALUES (N'Bàn ' + CAST (@i AS nvarchar(100)))
	SET @i = @i+ 1
END

INSERT INTO USERS (user_name, user_password, user_displayname, user_fullname, user_phone)
VALUES ('anh2000', '456', N'Vân Anh', N'Nguyễn Vân Anh', '0901234567'),
       ('binh2001', '123', N'Bình', N'Trần Thị Bình', '0378901234'),
       ('cang2002', '135', N'Cang', N'Lê Minh Cang', '0856789012'),
       ('dung1991', '246', N'Dung', N'Phạm Thu Dung', '0945678901'),
       ('em1992', '789', N'Ngọc Em', N'Hồ Ngọc Em', '0321903456')

CREATE PROC USP_GetAccountByUserName
@userName NVARCHAR(50)
AS
BEGIN 
	SELECT *FROM USERS WHERE user_name = @userName
END
GO

DROP PROC USP_GetAccountByUserName;

CREATE PROC USP_Login
@userName NVARCHAR(50), @passWord NVARCHAR(50)
AS
BEGIN
	SELECT * FROM USERS WHERE user_name = @userName AND user_password = @passWord
END
GO

DROP PROC USP_Login;

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT BILL (bill_checkin_date, bill_checkout_date, table_id)
		VALUES (GETDATE(), NULL, @idTable);
END
GO

DROP PROC USP_InsertBill;

CREATE PROC USP_InsertBillInfo 
@idBill INT,
@idFood INT,
@quantity INT
AS
BEGIN
  DECLARE @isExitsBillInfo INT
  DECLARE @foodCount INT = 1

  SELECT @isExitsBillInfo = bd_id, @foodCount= bd.quantity 
  FROM BILL_DETAIL AS bd  
  WHERE bill_id = @idBill AND menu_id = @idFood

  IF (@isExitsBillInfo > 0)
  BEGIN  
    DECLARE @newCount INT = @foodCount + @quantity
    IF (@newCount > 0)
      UPDATE BILL_DETAIL SET quantity = @foodCount + @quantity WHERE menu_id = @idFood
    ELSE  
      DELETE BILL_DETAIL WHERE bill_id = @idBill AND menu_id = @idFood
  END
  ELSE
    BEGIN
      INSERT BILL_DETAIL (bill_id, menu_id, quantity)
        VALUES (@idBill, @idFood, @quantity);
    END
END
GO

DROP PROC USP_InsertBillInfo;

CREATE TRIGGER UTG_UpdateBillInfo
ON BILL_DETAIL FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = bill_id FROM inserted

	DECLARE @idTable INT
	SELECT @idTable = table_id FROM BILL WHERE bill_id = @idBill AND bill_status = 0

	UPDATE TABLES SET table_status = N'Có người' WHERE table_id = @idTable
END
GO

DROP TRIGGER UTG_UpdateBillInfo;

CREATE TRIGGER UTG_UpdateBill
ON BILL FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = bill_id FROM Inserted
	
	DECLARE @idTable INT
	SELECT @idTable = table_id FROM BILL WHERE bill_id = @idBill

	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM BILL WHERE table_id = @idTable AND bill_status = 0

	IF (@count = 0)
		UPDATE TABLES SET table_status = N'Trống' WHERE @idTable = table_id
END
GO

DROP TRIGGER UTG_UpdateBill;

CREATE PROC USP_GetListBillByDate
@checkIn date, @checkOut date
AS
BEGIN
	SELECT t.table_name AS [Tên bàn], b.bill_total AS [Tổng tiền], CAST(bill_checkin_date AS DATE) AS [Ngày vào], CAST(bill_checkout_date AS DATE) AS [Ngày ra] 
	FROM BILL AS b
    INNER JOIN TABLES AS t ON b.table_id = t.table_id
	WHERE CAST(bill_checkin_date AS DATE) >= @checkIn AND CAST(bill_checkout_date AS DATE) <= @checkOut  
		AND b.bill_status = 1
END
GO

DROP PROC USP_GetListBillByDate;


CREATE PROC USP_UpdateAccount
@userName NVARCHAR(50), @displayName NVARCHAR(50), @fullName NVARCHAR(50), @password NVARCHAR(10), @newPassword NVARCHAR(10)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM USERS WHERE user_name =  @userName AND user_password = @passWord

	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL or @newPassWord = '')
		BEGIN
			UPDATE USERS SET user_displayname = @displayName, user_fullname = @fullName WHERE user_name = @userName
		END
		ELSE
			UPDATE USERS SET user_displayname = @displayName, user_fullname = @fullName, user_password = @newPassword WHERE user_name = @userName
	END
END
GO

DROP PROC USP_UpdateAccount;

CREATE TRIGGER UTG_DeleteBillInfo
ON BILL_DETAIL FOR DELETE
AS
BEGIN
	  DECLARE @idBillInfo INT;
	  DECLARE @idBill INT;
	  SELECT @idBillInfo = bd_id, @idBill = Deleted.bill_id FROM Deleted;

	  DECLARE @idTable INT;
	  SELECT @idTable = table_id FROM BILL WHERE bill_id = @idBill;

	  DECLARE @count INT = 0;
	  SELECT @count = COUNT(*) FROM BILL_DETAIL AS bd
	  WHERE bd.bill_id = @idBill;

	  IF (@count = 0)
		UPDATE TABLES SET table_status = N'Trống' WHERE table_id = @idTable;
END;
GO

DROP TRIGGER UTG_DeleteBillInfo;