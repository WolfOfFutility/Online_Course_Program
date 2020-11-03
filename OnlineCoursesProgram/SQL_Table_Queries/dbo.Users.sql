CREATE TABLE [dbo].[Users] (
    [UserID]      INT          NOT NULL IDENTITY,
    [Username]    VARCHAR (50) NOT NULL,
    [Password]    VARCHAR (50) NULL,
    [DisplayName] VARCHAR (50) NULL,
    [Role]        VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

