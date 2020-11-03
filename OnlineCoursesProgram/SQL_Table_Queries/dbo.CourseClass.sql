CREATE TABLE [dbo].[CourseClass] (
    [CourseClassID] INT IDENTITY (1, 1) NOT NULL,
    [CourseID]      INT NOT NULL,
    [ClassID]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CourseClassID] ASC)
);

