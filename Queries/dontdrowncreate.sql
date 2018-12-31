CREATE TABLE [dbo].[Accounts] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (30) NOT NULL,
    [password] VARCHAR (30) NOT NULL,
    [rol_id]   INT          DEFAULT ((1)) NOT NULL,
    [save_id]  INT          NOT NULL,
    [klas]     VARCHAR (50) NULL,
    CONSTRAINT [Accounts_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [Accounts_Rollen] FOREIGN KEY ([rol_id]) REFERENCES [dbo].[Rollen] ([id]) ON DELETE SET DEFAULT,
    CONSTRAINT [Accounts_Saves] FOREIGN KEY ([save_id]) REFERENCES [dbo].[Saves] ([id])
);

CREATE TABLE [dbo].[Antwoorden] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [vraag_id]    INT          NOT NULL,
    [text]        VARCHAR (30) NOT NULL,
    [correctness] INT          NOT NULL,
    CONSTRAINT [Antwoorden_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [Antwoorden_Vragen] FOREIGN KEY ([vraag_id]) REFERENCES [dbo].[Vragen] ([id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Levels] (
    [id]       INT IDENTITY (1, 1) NOT NULL,
    [vraag_id] INT NOT NULL,
    [minlvl]   INT NOT NULL,
    [maxlvl]   INT NULL,
    CONSTRAINT [Levels_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [Levels_Vragen] FOREIGN KEY ([vraag_id]) REFERENCES [dbo].[Vragen] ([id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Rollen] (
    [id]   INT          IDENTITY (1, 1) NOT NULL,
    [naam] VARCHAR (30) NOT NULL,
    CONSTRAINT [Rollen_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Saves] (
    [id]   INT            IDENTITY (1, 1) NOT NULL,
    [data] NVARCHAR (MAX) DEFAULT ('{ "Level": 1, "LevelUp": "false" }') NOT NULL,
    CONSTRAINT [Saves_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Type_vragen] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [naam] VARCHAR (255) NOT NULL,
    CONSTRAINT [Type_vragen_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Vragen] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [type_id]  INT           NOT NULL,
    [vraag]    VARCHAR (255) NOT NULL,
    [hint]     VARCHAR (400) NULL,
    [minlevel] INT           NULL,
    [maxlevel] INT           NULL,
    CONSTRAINT [Vragen_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [Vragen_Type_vragen] FOREIGN KEY ([type_id]) REFERENCES [dbo].[Type_vragen] ([id])
);

