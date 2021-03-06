﻿--INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel)
--VALUES (1, 'Hoe lang is een chinees?', 'Vraag aan je opa', 1, 20)

--BEGIN TRANSACTION
--   DECLARE @DataID int;
--   INSERT INTO Vragen (type_id, vraag, hint, minlevel, maxlevel)
--   VALUES (1, 'Hoe lang is een chinees?', 'Vraag aan je opa', 1, 20);
--   SELECT @DataID = scope_identity();
--   INSERT INTO Antwoorden (vraag_id, text, correctness) VALUES (@DataID, 'Lang is zijn naam', 1);
--   INSERT INTO Antwoorden (vraag_id, text, correctness) VALUES (@DataID, 'Zeer lang', 2);
--COMMIT

BEGIN TRANSACTION
   DECLARE @DataID int;
   INSERT INTO Saves (data) VALUES (DEFAULT);
   SELECT @DataID = scope_identity();
   INSERT INTO Accounts (username, password, rol_id, klas, save_id) VALUES ('Henk', '123', 2, '1', @DataID);
COMMIT

--SELECT * FROM Vragen, Antwoorden WHERE Vragen.id = Antwoorden.vraag_id ORDER BY Vragen.id, Antwoorden.correctness;

--INSERT INTO Accounts (username, password, rol_id, save_id)
--VALUES ('Rick', '123', 1, 1);

--SELECT a.id, a.username, a.rol_id, r.naam, a.save_id, s.data, a.klas FROM Accounts a, Rollen r, Saves s WHERE a.rol_id = r.id AND a.save_id = s.id ORDER BY a.rol_id;

--SELECT * FROM Accounts WHERE username = 'Rick' AND password = '123';

--UPDATE Saves SET data = '{ "Level": 1, "LevelUp": true }' WHERE id = 1;
--SELECT data FROM Saves WHERE id = 1;

--UPDATE Saves SET data = JSON_MODIFY(data, '$.LevelUp', 'true') WHERE id = 1;
--UPDATE Saves SET data = JSON_MODIFY(JSON_MODIFY(data, '$.LevelUp', 'false'), '$.Level',JSON_VALUE(data, '$.Level') + 1) WHERE id = 1;
--UPDATE Saves SET data = '{ "Level": 2, "LevelUp": "false" }' WHERE id = 1 AND ISJSON('{ "Level": 2, "LevelUp": "false" }') > 0;
--SELECT data FROM saves WHERE id = 1 AND ISJSON(data) > 0;

--SELECT data FROM Saves, Accounts WHERE username = 'rick' AND accounts.id = saves.id;

--SELECT a.id, a.username, a.rol_id, r.naam, a.save_id, a.klas FROM Accounts a, Rollen r WHERE a.rol_id = r.id AND LOWER(a.username) = 'rick' AND a.password = '123'

--UPDATE Saves SET data = '{ "Level": 1, "LevelUp": "true", "Request": "true" }' WHERE id = 1

--UPDATE Saves SET data = JSON_MODIFY(JSON_MODIFY(data, '$.LevelUp', CAST(0 as BIT)), '$.Request', CAST(0 as BIT)) WHERE id = 2;

--UPDATE Saves 
--SET data = '{ "Level": 1, "LevelUp": false, "Request": true, "Inventory": {} }'
--WHERE id = (SELECT a.save_id FROM Accounts a, Saves s WHERE s.id = a.save_id AND a.id = 1)
--AND ISJSON('{ "Level": 1, "LevelUp": false, "Request": false, "Inventory": {} }') > 0;

SELECT a.id, data FROM saves s, Accounts a WHERE a.save_id = s.id AND JSON_VALUE(data, '$.Request') = CAST(1 as BIT);

--SELECT a.id, a.username, a.rol_id, r.naam, a.save_id, s.data, a.klas FROM Accounts a, Rollen r, Saves s WHERE a.rol_id = r.id AND a.save_id = s.id AND klas = 'H5P' ORDER BY a.rol_id