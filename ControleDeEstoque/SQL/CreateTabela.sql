CREATE TABLE [dbo].[Produtos] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Nome]       NVARCHAR (MAX) NOT NULL,
    [Codigo]     FLOAT (53)     NOT NULL,
    [Quantidade] FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED ([Id] ASC)
);