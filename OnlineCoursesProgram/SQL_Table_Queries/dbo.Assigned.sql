CREATE TABLE [dbo].[Assigned] (
    [AssignedID] INT IDENTITY (1, 1) NOT NULL,
    [CourseID]   INT NOT NULL,
    [UserID]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AssignedID] ASC)
);

