TRUNCATE TABLE  dbo.Certificate;

CREATE TABLE [dbo].[ProgramDGs] (
    [id]                 INT             NOT NULL,
    [name]               NVARCHAR (1200) NOT NULL,
    [dateNumberApproved] NVARCHAR (100)  NOT NULL,
    [typeId]             INT             NOT NULL,
    CONSTRAINT [PK_dbo.ProgramDGs] PRIMARY KEY CLUSTERED ([name] ASC),
    FOREIGN KEY ([typeId]) REFERENCES [dbo].[TypeDocument] ([Id])
);

