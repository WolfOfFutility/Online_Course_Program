CREATE TABLE [dbo].[Posts] (
    [PostID]     INT           NOT NULL,
    [Author]     VARCHAR (50)  NOT NULL,
    [DatePosted] VARCHAR (50)  NOT NULL,
    [Content]    VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([PostID] ASC)
);

