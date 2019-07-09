TRUNCATE TABLE  dbo.Certificate;

CREATE TABLE [dbo].[ProgramDGs] (
    [id]                 INT             NOT NULL,
    [name]               NVARCHAR (1200) NOT NULL,
    [dateNumberApproved] NVARCHAR (100)  NOT NULL,
    [typeId]             INT             NOT NULL,
    CONSTRAINT [PK_dbo.ProgramDGs] PRIMARY KEY CLUSTERED ([name] ASC),
    FOREIGN KEY ([typeId]) REFERENCES [dbo].[TypeDocument] ([Id])
);

INSERT INTO dbo.Lesson(name)
VALUES(N'Раздел I. Правовая подготовка.' + CHAR(13) +
N'Раздел II. Пожарно-тактическая подготовка.' + CHAR(13) +
N'Раздел III. Аварийно-спасательная подготовка.' + CHAR(13) +
N'Раздел IV. Пожарная и аварийно - спасательная техника.' + CHAR(13) +
N'Раздел V. Пожарно-строевая подготовка.' + CHAR(13) +
N'Раздел VI. Психологическая подготовка.' + CHAR(13) +
N'Раздел VII. Пожарно-профилактическая подготовка.' + CHAR(13) +
N'Раздел VIII. Газодымозащитная подготовка.' + CHAR(13) +
N'Раздел IX. Медицинская подготовка'
);