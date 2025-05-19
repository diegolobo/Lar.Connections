CREATE TABLE [People] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Document] NVARCHAR(20) NOT NULL,
    [BirthDate] DATETIME2 NOT NULL,
    [Active] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_People_Document ON [People]([Document]);
CREATE INDEX IX_People_Active ON [People]([Active]);