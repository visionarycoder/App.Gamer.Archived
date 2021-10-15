CREATE TABLE [dbo].[GameDefinition]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL DEFAULT '', 
    [Description] NTEXT NOT NULL DEFAULT '', 
    [TurnPrompt] NTEXT NOT NULL DEFAULT '', 
    [MaxNumberOfPlayers] INT NOT NULL DEFAULT 0, 
    [MinNumberOfPlayers] INT NOT NULL DEFAULT 0
)
