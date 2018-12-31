SET IDENTITY_INSERT [dbo].[Accounts] ON
INSERT INTO [dbo].[Accounts] ([id], [username], [password], [rol_id], [save_id], [klas]) VALUES (1, N'Rick', N'123', 1, 1, N'H5P')
INSERT INTO [dbo].[Accounts] ([id], [username], [password], [rol_id], [save_id], [klas]) VALUES (3, N'Peter', N'123', 2, 2, NULL)
SET IDENTITY_INSERT [dbo].[Accounts] OFF

SET IDENTITY_INSERT [dbo].[Antwoorden] ON
INSERT INTO [dbo].[Antwoorden] ([id], [vraag_id], [text], [correctness]) VALUES (1, 2, N'1000', 2)
INSERT INTO [dbo].[Antwoorden] ([id], [vraag_id], [text], [correctness]) VALUES (2, 2, N'10000', 1)
INSERT INTO [dbo].[Antwoorden] ([id], [vraag_id], [text], [correctness]) VALUES (3, 2, N'100000', 3)
INSERT INTO [dbo].[Antwoorden] ([id], [vraag_id], [text], [correctness]) VALUES (4, 1003, N'Lang is zijn naam', 1)
INSERT INTO [dbo].[Antwoorden] ([id], [vraag_id], [text], [correctness]) VALUES (5, 1003, N'Zeer lang', 2)
SET IDENTITY_INSERT [dbo].[Antwoorden] OFF

SET IDENTITY_INSERT [dbo].[Rollen] ON
INSERT INTO [dbo].[Rollen] ([id], [naam]) VALUES (1, N'Admin')
INSERT INTO [dbo].[Rollen] ([id], [naam]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Rollen] OFF

SET IDENTITY_INSERT [dbo].[Saves] ON
INSERT INTO [dbo].[Saves] ([id], [data]) VALUES (1, N'{ "Level": 2, "LevelUp": "false" }')
INSERT INTO [dbo].[Saves] ([id], [data]) VALUES (2, N'{}')

SET IDENTITY_INSERT [dbo].[Type_vragen] ON
INSERT INTO [dbo].[Type_vragen] ([id], [naam]) VALUES (1, N'Meerkeuze')
SET IDENTITY_INSERT [dbo].[Type_vragen] OFF

SET IDENTITY_INSERT [dbo].[Vragen] ON
INSERT INTO [dbo].[Vragen] ([id], [type_id], [vraag], [hint], [minlevel], [maxlevel]) VALUES (2, 1, N'Hoeveel kilogram in een centigram', N'Geen', 1, 5)
INSERT INTO [dbo].[Vragen] ([id], [type_id], [vraag], [hint], [minlevel], [maxlevel]) VALUES (1003, 1, N'Hoe lang is een chinees?', N'Vraag aan je opa', 1, 20)
SET IDENTITY_INSERT [dbo].[Vragen] OFF

