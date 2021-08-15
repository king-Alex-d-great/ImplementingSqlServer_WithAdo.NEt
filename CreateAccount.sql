CREATE PROCEDURE CreateAccount
(
@Id int NOT NULL,
@UserId Int NOT NULL,
@Account_Number Int NOT NULL,
@Balance decimal(38,2) NOT NULL
)

AS
BEGIN
INSERT INTO Accounts VALUES (@Id, @UserId, @Account_Number, @Balance)
SELECT * FROM Accounts
END
