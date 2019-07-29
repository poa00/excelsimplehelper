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
VALUES(N'I. Введение в профессию.' + CHAR(13) +
N'II. Виды СЛО и условия их образования.' + CHAR(13) +
N'III. Концепция чистого ВС.' + CHAR(13) +
N'IV. Авиационные события, связанные с наземным обследованием ВС.' + CHAR(13) +
N'V. Средства противообледенительной обработки ВС.' + CHAR(13) +
N'VI. Противообледенительные жидкости.' + CHAR(13) +
N'VII. Методы противообледенительной обработки ВС.' + CHAR(13) +
N'VIII. Процедуры контроля качества состояния поверхностей ВС.' + CHAR(13) +
N'IX. Обеспечение безопасности и регулярности полетов, профилактика авиационных событий.'
);

