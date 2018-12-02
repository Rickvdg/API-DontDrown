--INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel)
--VALUES (1, 'Hoe lang is een chinees?', 'Vraag aan je opa', 1, 20)

--BEGIN TRANSACTION
--   DECLARE @DataID int;
--   INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel)
--   VALUES (1, 'Hoe lang is een chinees?', 'Vraag aan je opa', 1, 20);
--   SELECT @DataID = scope_identity();
--   INSERT INTO Antwoorden (vraag_id, text, correctness) VALUES (@DataID, 'Lang is zijn naam', 1);
--   INSERT INTO Antwoorden (vraag_id, text, correctness) VALUES (@DataID, 'Zeer lang', 2);
--COMMIT

--SELECT * FROM Vragen, Antwoorden WHERE Vragen.id = Antwoorden.vraag_id ORDER BY Vragen.id, Antwoorden.correctness;

--INSERT INTO Accounts (username, password, rol_id, save_id)
--VALUES ('Rick', '123', 1, 1);

SELECT * FROM Accounts;