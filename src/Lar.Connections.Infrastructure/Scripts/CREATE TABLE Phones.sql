CREATE TABLE [Phones] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Number] NVARCHAR(20) NOT NULL,
    [Type] INT NOT NULL,
    [PersonId] BIGINT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Phone_People FOREIGN KEY ([PersonId]) REFERENCES [People]([Id])
);

CREATE INDEX IX_Phone_PersonId ON [Phones]([PersonId]);