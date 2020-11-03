CREATE TABLE [dbo].[Classes] (
    [ClassID]   INT             IDENTITY (1, 1) NOT NULL,
    [ClassName] VARCHAR (50)    NOT NULL,
    [Video]     VARBINARY (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([ClassID] ASC)
);

