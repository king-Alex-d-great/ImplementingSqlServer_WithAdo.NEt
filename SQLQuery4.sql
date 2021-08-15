Use [BEZAOPay]
Go
CREATE PROCEDURE CreateAccount
(
@Id int ,
@UserId Int ,
@Account_Number Int,
@Balance decimal(38,2) 
)

AS
BEGIN
INSERT INTO Accounts VALUES (@Id, @UserId, @Account_Number, @Balance)
SELECT * FROM Accounts
END