-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-11-18 12:36:55.498

-- tables
-- Table: Accounts
CREATE TABLE Accounts (
    id int  NOT NULL IDENTITY(1,1),
    username varchar(30)  NOT NULL,
    password varchar(30)  NOT NULL,
    rol_id int  NOT NULL DEFAULT 1,
    save_id int  NOT NULL,
    CONSTRAINT Accounts_pk PRIMARY KEY  (id)
);

-- Table: Antwoorden
CREATE TABLE Antwoorden (
    id int  NOT NULL IDENTITY(1,1),
    vraag_id int  NOT NULL,
    text varchar(30)  NOT NULL,
    CONSTRAINT Antwoorden_pk PRIMARY KEY  (id)
);

-- Table: Levels
CREATE TABLE Levels (
    id int  NOT NULL IDENTITY(1,1),
    vraag_id int  NOT NULL,
    minlvl int  NOT NULL,
    maxlvl int  NULL,
    CONSTRAINT Levels_pk PRIMARY KEY  (id)
);

-- Table: Rollen
CREATE TABLE Rollen (
    id int  NOT NULL IDENTITY(1,1),
    naam varchar(30)  NOT NULL,
    CONSTRAINT Rollen_pk PRIMARY KEY  (id)
);

-- Table: Saves
CREATE TABLE Saves (
    id int  NOT NULL IDENTITY(1,1),
    data nvarchar(max)  NOT NULL,
    CONSTRAINT Saves_pk PRIMARY KEY  (id)
);

-- Table: Type_vragen
CREATE TABLE Type_vragen (
    id int  NOT NULL IDENTITY(1,1),
    naam varchar(255)  NOT NULL,
    CONSTRAINT Type_vragen_pk PRIMARY KEY  (id)
);

-- Table: Vragen
CREATE TABLE Vragen (
    id int  NOT NULL IDENTITY(1,1),
    type_id int  NOT NULL,
    vraag varchar(255)  NOT NULL,
    hint varchar(400)  NULL,
    CONSTRAINT Vragen_pk PRIMARY KEY  (id)
);

-- foreign keys
-- Reference: Accounts_Rollen (table: Accounts)
ALTER TABLE Accounts ADD CONSTRAINT Accounts_Rollen
    FOREIGN KEY (rol_id)
    REFERENCES Rollen (id)
    ON DELETE  SET DEFAULT;

-- Reference: Accounts_Saves (table: Accounts)
ALTER TABLE Accounts ADD CONSTRAINT Accounts_Saves
    FOREIGN KEY (save_id)
    REFERENCES Saves (id);

-- Reference: Antwoorden_Vragen (table: Antwoorden)
ALTER TABLE Antwoorden ADD CONSTRAINT Antwoorden_Vragen
    FOREIGN KEY (vraag_id)
    REFERENCES Vragen (id)
    ON DELETE  CASCADE;

-- Reference: Levels_Vragen (table: Levels)
ALTER TABLE Levels ADD CONSTRAINT Levels_Vragen
    FOREIGN KEY (vraag_id)
    REFERENCES Vragen (id)
    ON DELETE  CASCADE;

-- Reference: Vragen_Type_vragen (table: Vragen)
ALTER TABLE Vragen ADD CONSTRAINT Vragen_Type_vragen
    FOREIGN KEY (type_id)
    REFERENCES Type_vragen (id);

-- End of file.

